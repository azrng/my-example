using AutoMapper;
using Common.EFCore;
using Common.EFCore.Extensions;
using Common.EFCore.Requests;
using Common.EFCore.Results;
using Common.Extension;
using Microsoft.EntityFrameworkCore;
using NetByDocker.Domain;
using NetByDocker.Model.UserManage;

namespace NetByDocker.Service.UserManage;

public class UserService : IScopedDependency, IUserService
{
    private readonly IMapper _mapper;
    private readonly IBaseRepository<User> _userRep;
    private readonly IBaseRepository<Group> _gradeRep;

    public UserService(IMapper mapper,
        IBaseRepository<User> userRep,
        IBaseRepository<Group> gradeRep)
    {
        _mapper = mapper;
        _userRep = userRep;
        _gradeRep = gradeRep;
    }

    ///<inheritdoc cref="IUserService.GetPageListAsync(GetUserPageRequest)"/>
    public async Task<GetQueryPageResult<GetUserPageInfoResponse>> GetPageListAsync(GetUserPageRequest request)
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
        var query = _userRep.EntitiesNoTacking
            .WhereIF(!string.IsNullOrWhiteSpace(request.Keyword),
                t => t.Name == request.Keyword || t.Account == request.Keyword)
            .OrderBy(t => t.CreateTime)
            .Select(u => new GetUserPageInfoResponse
            {
                Account = u.Account,
                Id = u.Id,
                Sex = u.Sex,
                Name = u.Name,
                Credit = u.Credit,
                ClassId = u.GroupId
            });
        //总条数
        var total = await query.CountAsync().ConfigureAwait(false);
        //用户集合
        var result = await query.PagedBy(new GetPageSortRequest(request.PageIndex, request.PageSize)).ToListAsync()
            .ConfigureAwait(false);
        //查询上面查询出来的所有用户的班级ID
        var classIds = result.Select(t => t.ClassId).ToList();
        //根据班级id查询班级集合
        var classEntityList = await _gradeRep.EntitiesNoTacking.Where(t => classIds.Contains(t.Id))
            .Select(t => new { ClassId = t.Id, t.Name }).ToListAsync().ConfigureAwait(false);
        //循环用户集合，将班级的名字赋值给班级名字字段返回
        result.ForEach(t =>
        {
            var classEntity = classEntityList.Find(c => c.ClassId == t.ClassId);
            t.Name = classEntity?.Name;
        });

        #endregion

        return new GetQueryPageResult<GetUserPageInfoResponse>(result,
            new PageResult(request.PageIndex, request.PageSize, total));
    }

    ///<inheritdoc cref="IUserService.GetDetailsAsync"/>
    public async Task<GetUserInfoResponse> GetDetailsAsync(long userId, CancellationToken cancellationToken)
    {
        var result = await _userRep.EntitiesNoTacking
            .Select(t => new GetUserInfoResponse
            {
                Id = t.Id,
                Account = t.Account,
                Name = t.Name,
                Sex = t.Sex,
            })
            .FirstOrDefaultAsync(t => t.Id == userId, cancellationToken).ConfigureAwait(false);
        return result;
    }

    ///<inheritdoc cref="IUserService.AddAsync(AddUserVm)"/>
    public async Task<long> AddAsync(AddUserVm dto)
    {
        var exist = await _userRep.EntitiesNoTacking.AnyAsync(t => !t.Deleted && t.Account == dto.Account)
            .ConfigureAwait(false);
        if (exist)
            throw new ParameterException("账号重复");

        //方案一：直接使用automapper映射
        //var user = _mapper.Map<User>(dto);

        //方案二：自己手动实例化对象
        var user = new User(dto.Account, dto.PassWord, dto.Name, dto.Sex, dto.Credit, dto.ClassId);

        await _userRep.AddAsync(user, true).ConfigureAwait(false);
        return user.Id;
    }

    ///<inheritdoc cref="IUserService.PatchUpdateAsync(long, UpdateUserVm)"/>
    public async Task<int> PatchUpdateAsync(long userId, UpdateUserVm dto)
    {
        if (userId == 0)
            throw new ParameterException("用户标识无效");

        var user = await _userRep.Entities.FirstOrDefaultAsync(t => t.Id == userId).ConfigureAwait(false);
        _mapper.Map(dto, user);
        return await _userRep.UpdateAsync(user, true).ConfigureAwait(false);
    }

    ///<inheritdoc cref="IUserService.UpdateAsync(long, UpdateUserVm)"/>
    public async Task<int> UpdateAsync(long userId, UpdateUserVm dto)
    {
        var entity = await _userRep.EntitiesNoTacking.FirstOrDefaultAsync(t => t.Id == userId).ConfigureAwait(false);
        ParameterException.ThrowIfNull(entity, "对象不存在");

        if (!string.IsNullOrWhiteSpace(dto.Account))
            entity.Account = dto.Account;

        if (!string.IsNullOrWhiteSpace(dto.PassWord))
            entity.PassWord = dto.PassWord;

        return await _userRep.UpdateAsync(entity, true).ConfigureAwait(false);
    }

    ///<inheritdoc cref="IUserService.DeleteAsync(long)"/>
    public async Task<int> DeleteAsync(long userId)
    {
        var entity = await _userRep.Entities.FirstOrDefaultAsync(t => t.Id == userId).ConfigureAwait(false);
        if (entity is null)
            return 0;

        entity.SetDelete("system");
        //逻辑删除
        return await _userRep.UpdateAsync(entity).ConfigureAwait(false);
    }
}