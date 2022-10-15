using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetByDocker.Filters;

/// <summary>
/// 本来这个类是做自定义返回类，但是如果默认情况下是开启验证的，那么你就进不来
/// </summary>
public class CustomerActionFilter : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;
        var errormessage = new List<string>();
        foreach (var item in context.ModelState.Values)
        {
            errormessage.AddRange(item.Errors.Select(t => t.ErrorMessage));
        }

        context.Result = new JsonResult("将上面的错误信息拼接成语一个字符串");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}