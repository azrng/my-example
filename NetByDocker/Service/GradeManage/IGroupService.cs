using NetByDocker.Model.GradeMange;

namespace NetByDocker.Service.GradeManage;

/// <summary>
/// 班级服务
/// </summary>
public interface IGroupService
{
    /// <summary>
    ///获取班级列表返回类
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<GetGroupListResponse>> GetGroupListAsync();

    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <returns></returns>
    Task<bool> InitAsync();
}