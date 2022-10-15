using Common.EFCore.Results;
using NetByDocker.Model.UserManage;

namespace NetByDocker.Service.UserManage;

/// <summary>
/// 用户服务接口
/// </summary>
public interface IUserService
{
    /// <summary>
    /// 分页获取用户
    /// </summary>
    /// <returns></returns>
    Task<GetQueryPageResult<GetUserPageInfoResponse>> GetPageListAsync(GetUserPageRequest request);

    /// <summary>
    /// 获取详情
    /// </summary>
    /// <returns></returns>
    Task<GetUserInfoResponse> GetDetailsAsync(long userId = 1407875772521123840, CancellationToken cancellationToken = default);

    /// <summary>
    /// 添加用户
    /// </summary>
    /// <param name="vm"></param>
    /// <returns></returns>
    Task<long> AddAsync(AddUserVm vm);

    /// <summary>
    /// 修改用户
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="vm"></param>
    /// <returns></returns>
    Task<int> UpdateAsync(long userId, UpdateUserVm vm);

    /// <summary>
    /// patch修改用户
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="vm"></param>
    /// <returns></returns>
    Task<int> PatchUpdateAsync(long userId, UpdateUserVm vm);

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<int> DeleteAsync(long userId);
}