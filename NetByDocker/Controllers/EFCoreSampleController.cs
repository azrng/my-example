using AutoMapper;
using Common.EFCore.Results;
using Microsoft.AspNetCore.Mvc;
using NetByDocker.Model.TestManage;
using NetByDocker.Service.TestManage;

namespace NetByDocker.Controllers;

/// <summary>
/// 用户控制器(目的用于EFCore基本使用)
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
public class EfCoreSampleController : ControllerBase
{
    private readonly ITestService _testService;
    private readonly IMapper _mapper;

    public EfCoreSampleController(ITestService testService,
        IMapper mapper)
    {
        _testService = testService;
        _mapper = mapper;
    }

    /// <summary>
    /// 查询用户分页列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<GetQueryPageResult<GetUserPageInfoResponseTest>> GetPageListAsync(
        [FromQuery] GetUserPageRequestTest request)
    {
        return _testService.GetPageListAsync(request);
    }

    /// <summary>
    /// 查询详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    public Task<GetUserInfoResponseTest> GetDetailsAsync([FromRoute] long id)
    {
        return _testService.GetDetailsAsync(id);
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<long> AddAsync([FromBody] AddUserVmTest dto)
    {
        return _testService.AddAsync(dto);
    }

    /// <summary>
    /// 修改帐号密码
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    [HttpPut("{id:long}")]
    public Task<int> PutAsync(long id, [FromBody] UpdateUserVmTest dto)
    {
        return _testService.UpdateAsync(id, dto);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id:long}")]
    public Task<int> DeleteAsync(long id)
    {
        return _testService.DeleteAsync(id);
    }
}