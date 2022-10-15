using Microsoft.AspNetCore.Mvc;
using NetByDocker.Model.GradeMange;
using NetByDocker.Service.GradeManage;

namespace NetByDocker.Controllers;

/// <summary>
/// 班级组控制器
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
public class GroupController : ControllerBase
{
    private readonly IGroupService _gradeService;

    public GroupController(IGroupService gradeService)
    {
        _gradeService = gradeService;
    }

    /// <summary>
    /// 获取班级信息
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<IEnumerable<GetGroupListResponse>> GetGroupListAsync()
    {
        return _gradeService.GetGroupListAsync();
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<bool> InitAsync()
    {
        return _gradeService.InitAsync();
    }
}