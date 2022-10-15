using Common.EFCore.Results;
using NetByDocker.Model.TestManage;

namespace NetByDocker.Service.TestManage;

/// <summary>
/// 测试服务（目的用于演示EFCore基本使用
/// </summary>
public interface ITestService
{
    /// <summary>
    /// 获取基本信息
    /// </summary>
    /// <param name="account"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task GetBaseInfo(string account, string name);

    /// <summary>
    /// 获取班级人员信息(三表连接)
    /// </summary>
    /// <returns></returns>
    Task<List<GetGroupUserInfoResponseTest>> GetGroupUserListAsync();

    /// <summary>
    /// 分页获取用户
    /// </summary>
    /// <returns></returns>
    Task<GetQueryPageResult<GetUserPageInfoResponseTest>> GetPageListAsync(GetUserPageRequestTest request);

    /// <summary>
    /// 获取详情
    /// </summary>
    /// <returns></returns>
    Task<GetUserInfoResponseTest> GetDetailsAsync(long userId = 1407875772521123840);

    /// <summary>
    /// 添加用户
    /// </summary>
    /// <param name="vm"></param>
    /// <returns></returns>
    Task<long> AddAsync(AddUserVmTest vm);

    /// <summary>
    /// 修改用户
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="vm"></param>
    /// <returns></returns>
    Task<int> UpdateAsync(long userId, UpdateUserVmTest vm);

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<int> DeleteAsync(long userId);
}