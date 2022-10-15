using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NetByDocker.Extensions
{
    /// <summary>
    /// 框架绑定器
    /// </summary>
    public class TokenModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.Metadata.ModelType == typeof(TokenModel) ? new TokenModelBinder() : null;
        }
    }

    /// <summary>
    /// 自定义模型绑定器
    /// </summary>
    public class TokenModelBinder : IModelBinder
    {
        /// <summary>
        /// 请求里传递参数token
        /// </summary>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            //参数必须包含token
            if (!bindingContext.ActionContext.HttpContext.Request.Headers.ContainsKey("token"))
                return Task.CompletedTask;

            var token = bindingContext.ActionContext.HttpContext.Request.Headers["token"];

            //TODO  解析token
            var result = new TokenModel()
            {
                UserID = 111,
                UserName = "azrng",
            };
            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }

    public class TokenModel
    {
        public int UserID { get; set; }

        public string UserName { get; set; }
    }
}