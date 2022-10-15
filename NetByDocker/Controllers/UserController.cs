using AutoMapper;
using Common.EFCore.Results;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NetByDocker.Model.UserManage;
using NetByDocker.Service.UserManage;

namespace NetByDocker.Controllers;

/// <summary>
/// 用户控制器
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService,
        IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }


    /// <summary>
    /// 查询用户分页列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<GetQueryPageResult<GetUserPageInfoResponse>> GetPageListAsync(
        [FromQuery] GetUserPageRequest request)
    {
        return _userService.GetPageListAsync(request);
    }

    /// <summary>
    /// 查询详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    public Task<GetUserInfoResponse> GetDetailsAsync([FromRoute] long id,
        CancellationToken cancellationToken)
    {
        return _userService.GetDetailsAsync(id, cancellationToken);
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<long> AddAsync([FromBody] AddUserVm dto)
    {
        return _userService.AddAsync(dto);
    }

    /// <summary>
    /// 修改帐号密码
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    [HttpPut("{id:long}")]
    public Task<int> PutAsync(long id, [FromBody] UpdateUserVm dto)
    {
        return _userService.UpdateAsync(id, dto);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id:long}")]
    public Task<int> DeleteAsync(long id)
    {
        return _userService.DeleteAsync(id);
    }

    /// <summary>
    /// 修改  PATCH仅指定更改
    /// </summary>
    /// <param name="id"></param>
    /// <param name="jsonPatch"></param>
    /// <returns></returns>
    [HttpPatch("{id:long}")]
    public async Task<int> PatchAsync([FromRoute] long id,
        [FromBody] JsonPatchDocument<UpdateUserVm> jsonPatch)
    {
        var entity = await _userService.GetDetailsAsync(id).ConfigureAwait(false);
        if (entity is null)
            throw new NotFoundException("用户不存在");

        var dto = _mapper.Map<UpdateUserVm>(entity);
        jsonPatch.ApplyTo(dto, ModelState);

        return await _userService.PatchUpdateAsync(id, dto).ConfigureAwait(false);
    }
}