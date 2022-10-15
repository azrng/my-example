using Common.EFCore.Results;
using Microsoft.EntityFrameworkCore;
using NetByDocker.Domain;
using NetByDocker.Model.TestManage;

namespace NetByDocker.Service.TestManage
{
    /// <summary>
    /// 通过最基础的OpenDbContext实现基本的EFCore使用
    /// </summary>
    public class TestService : IScopedDependency, ITestService
    {
        private readonly OpenDbContext _openDbContext;

        public TestService(OpenDbContext openDbContext)
        {
            _openDbContext = openDbContext;
        }

        ///<inheritdoc cref="ITestService.GetBaseInfo(string, string)"/>
        public async Task GetBaseInfo(string account, string name)
        {
            //查询集合：返回List<User>
            var userList = await _openDbContext.Users.ToListAsync();


            throw new NotImplementedException();
        }

        ///<inheritdoc cref="ITestService.GetGroupUserListAsync"/>
        public async Task<List<GetGroupUserInfoResponseTest>> GetGroupUserListAsync()
        {
            var result = await _openDbContext.Users.AsNoTracking()
                .Join(_openDbContext.Grades.AsNoTracking(), ur => ur.GroupId, gr => gr.Id, (ur, gr) => new { ur, gr })
                .Join(_openDbContext.Scores.AsNoTracking(), urg => urg.ur.Id, sc => sc.UserId, (urg, sc) =>
                    new GetGroupUserInfoResponseTest
                    {
                        GroupId = urg.gr.Id,
                        GradeName = urg.gr.Name,
                        UserId = urg.ur.Id,
                        Account = urg.ur.Account,
                        Name = urg.ur.Name,
                        CourseName = sc.CourseName,
                        Grade = sc.Grade,
                    }).ToListAsync().ConfigureAwait(false);
            return result;
        }

        ///<inheritdoc cref="ITestService.GetPageListAsync(GetUserPageRequestTest)"/>
        public async Task<GetQueryPageResult<GetUserPageInfoResponseTest>> GetPageListAsync(
            GetUserPageRequestTest request)
        {
            /*
             当前方法需要返回学生信息以及学生所属的班级名字，涉及到两个表的查询
            方案一：通过两个表的关联实现
            方案二：查询出来数据后循环赋值实现
             */

            #region 方案一

            //var query = _userRep.EntitiesNoTacking
            //    .WhereIF(!string.IsNullOrWhiteSpace(request.Keyword), t => t.Name == request.Keyword || t.Account == request.Keyword)
            //    .OrderBy(t => t.CreateTime)
            //    .Join(_gradeRep.EntitiesNoTacking, u => u.ClassId, g => g.Id, (u, g) =>
            //        new GetUserPageInfoResponse
            //        {
            //            Account = u.Account,
            //            Id = u.Id,
            //            Sex = u.Sex,
            //            Name = u.Name,
            //            Credit = u.Credit,
            //            ClassName = g.Name
            //        })
            //    .PagedBy(new GetQueryPageRequest
            //    {
            //        PageIndex = request.PageIndex,
            //        PageSize = request.PageSize,
            //    });
            //var total = await query.CountAsync();

            #endregion

            #region 方案二

            //先查询用户表
            var query = _openDbContext.Users.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                query = query.Where(t => t.Name == request.Keyword || t.Account == request.Keyword);
            }

            //总条数
            var total = await query.CountAsync();
            //分页集合
            var result = await query.OrderBy(t => t.CreateTime)
                .Select(u => new GetUserPageInfoResponseTest
                {
                    Account = u.Account,
                    Id = u.Id,
                    Sex = u.Sex,
                    Name = u.Name,
                    Credit = u.Credit,
                    GroupId = u.GroupId
                })
                .Skip(request.PageSize * (request.PageIndex - 1))
                .Take(request.PageSize)
                .ToListAsync();
            //查询上面查询出来的所有用户的班级ID
            var classIds = result.Select(t => t.GroupId).ToList();
            //根据班级id查询班级集合
            var classEntityList = await _openDbContext.Grades.AsNoTracking()
                .Where(t => classIds.Contains(t.Id)).Select(t => new { GroupId = t.Id, t.Name }).ToListAsync();
            //循环用户集合，将班级的名字赋值给班级名字字段返回
            result.ForEach(t =>
            {
                var classEntity = classEntityList.Find(c => c.GroupId == t.GroupId);
                t.Name = classEntity?.Name;
            });

            #endregion

            return new GetQueryPageResult<GetUserPageInfoResponseTest>(result,
                new PageResult(request.PageIndex, request.PageSize, total));
        }

        ///<inheritdoc cref="ITestService.GetDetailsAsync"/>
        public async Task<GetUserInfoResponseTest> GetDetailsAsync(long userId)
        {
            var result = await _openDbContext.Users.AsNoTracking()
                .Select(t => new GetUserInfoResponseTest
                {
                    Id = t.Id,
                    Account = t.Account,
                    Name = t.Name,
                    Sex = t.Sex,
                })
                .FirstOrDefaultAsync(t => t.Id == userId).ConfigureAwait(false);
            return result;
        }

        ///<inheritdoc cref="ITestService.AddAsync(AddUserVmTest)"/>
        public async Task<long> AddAsync(AddUserVmTest dto)
        {
            var exist = await _openDbContext.Users.AsNoTracking().AnyAsync(t => !t.Deleted && t.Account == dto.Account);
            if (exist)
                throw new AggregateException("账号重复");

            //方案一：直接使用automapper映射
            //var user = _mapper.Map<User>(dto);

            //方案二：自己手动实例化对象
            var user = new User(dto.Account, dto.PassWord, dto.Name, dto.Sex, dto.Credit, dto.ClassId);

            await _openDbContext.AddAsync(user);
            await _openDbContext.SaveChangesAsync();
            return user.Id;
        }

        ///<inheritdoc cref="ITestService.UpdateAsync(long, UpdateUserVmTest)"/>
        public async Task<int> UpdateAsync(long userId, UpdateUserVmTest dto)
        {
            var entity = await _openDbContext.Users.FirstOrDefaultAsync(t => t.Id == userId).ConfigureAwait(false);
            if (entity == null)
                throw new AggregateException("对象不存在");

            if (!string.IsNullOrWhiteSpace(dto.Account))
                entity.Account = dto.Account;

            if (!string.IsNullOrWhiteSpace(dto.PassWord))
                entity.PassWord = dto.PassWord;

            _openDbContext.Update(entity);
            return await _openDbContext.SaveChangesAsync();
        }

        ///<inheritdoc cref="ITestService.DeleteAsync(long)"/>
        public async Task<int> DeleteAsync(long userId)
        {
            var entity = await _openDbContext.Users.FirstOrDefaultAsync(t => t.Id == userId).ConfigureAwait(false);
            if (entity is null)
                return 0;
            entity.Deleted = true;
            entity.Modifyer = "system";
            entity.ModifyTime = DateTime.Now;

            //逻辑删除,将是否删除字段修改为已删除
            _openDbContext.Update(entity);
            return await _openDbContext.SaveChangesAsync();
        }
    }
}