using Microsoft.AspNetCore.Mvc;
using NetByDocker.Model.TestManage;
using NetByDocker.Service.TestManage;
using System.Globalization;

namespace NetByDocker.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly ITestService _testService;

    public TestController(ITestService testService)
    {
        _testService = testService;
    }

    [HttpGet]
    public string GetDateTime()
    {
        var currTime = DateTime.Now.ToString(CultureInfo.CurrentCulture);
        Console.WriteLine(currTime);
        return currTime;
    }

    /// <summary>
    /// 获取组用户信息(三表连接)
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<GetGroupUserInfoResponseTest>> GetGroupUserListAsync()
    {
        return await _testService.GetGroupUserListAsync().ConfigureAwait(false);
    }
}