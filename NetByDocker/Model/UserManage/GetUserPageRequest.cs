using Common.EFCore.Requests;

namespace NetByDocker.Model.UserManage
{
    /// <summary>
    /// 获取用户分页请求类
    /// </summary>
    public class GetUserPageRequest : GetPageRequest
    {
        /// <summary>
        /// 关键字  (帐号/名字搜索)
        /// </summary>
        public string Keyword { get; set; }
    }
}