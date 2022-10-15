using Common.EFCore;
using Microsoft.EntityFrameworkCore;
using NetByDocker.Domain;
using NetByDocker.Model.Enum;
using NetByDocker.Model.GradeMange;

namespace NetByDocker.Service.GradeManage;

/// <summary>
/// 班级服务
/// </summary>
public class GroupService : IScopedDependency, IGroupService
{
    private readonly IBaseRepository<Group> _gradeRepository;
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<Score> _scoreRepository;

    public GroupService(IBaseRepository<Group> gradeRepository,
        IBaseRepository<User> userRepository,
        IBaseRepository<Score> scoreRepository)
    {
        _gradeRepository = gradeRepository;
        _userRepository = userRepository;
        _scoreRepository = scoreRepository;
    }

    ///<inheritdoc cref="IGroupService.GetGroupListAsync"/>
    public async Task<IEnumerable<GetGroupListResponse>> GetGroupListAsync()
    {
        var result = await _gradeRepository.EntitiesNoTacking
            .Select(t => new GetGroupListResponse
            {
                GradeId = t.Id,
                GradeName = t.Name,
                CreateTime = t.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")
            }).ToListAsync().ConfigureAwait(false);
        return result;
    }

    ///<inheritdoc cref="IGroupService.InitAsync"/>
    public async Task<bool> InitAsync()
    {
        var grade1 = new Group("一班");
        await _gradeRepository.AddAsync(grade1, true).ConfigureAwait(false);
        var user1 = new User("admin1", "123456", "张三", SexEnum.Man, 10.12d, grade1.Id);
        await _userRepository.AddAsync(user1, true).ConfigureAwait(false);
        var scoreList1 = new List<Score>
        {
            new Score("语文", 80, user1.Id),
            new Score("数学", 90, user1.Id),
            new Score("英语", 30, user1.Id)
        };
        await _scoreRepository.AddAsync(scoreList1, true).ConfigureAwait(false);
        var user2 = new User("admin2", "123456", "李四", SexEnum.WoMan, 80.10d, grade1.Id);
        await _userRepository.AddAsync(user2, true).ConfigureAwait(false);
        var scoreList2 = new List<Score>
        {
            new Score("语文", 70, user2.Id),
            new Score("数学", 60, user2.Id),
            new Score("英语", 10, user2.Id)
        };
        await _scoreRepository.AddAsync(scoreList2, true).ConfigureAwait(false);

        var grade2 = new Group("二班");
        await _gradeRepository.AddAsync(grade2, true).ConfigureAwait(false);
        var user3 = new User("admin3", "123456", "王五", SexEnum.Man, 25.30d, grade2.Id);
        await _userRepository.AddAsync(user3).ConfigureAwait(false);
        var scoreList3 = new List<Score>
        {
            new Score("语文", 50, user3.Id),
            new Score("数学", 40, user3.Id)
        };
        await _scoreRepository.AddAsync(scoreList3, true).ConfigureAwait(false);

        var grade3 = new Group("三班");
        await _gradeRepository.AddAsync(grade3, true).ConfigureAwait(false);

        return true;
    }
}