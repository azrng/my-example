using Common.EFCore.Requests;

namespace NetByDocker.Model.TestManage
{
    /// <summary>
    /// 获取用户分页请求类
    /// </summary>
    public class GetUserPageRequestTest : GetPageRequest
    {
        /// <summary>
        /// 关键字  (帐号/名字搜索)
        /// </summary>
        public string Keyword { get; set; }
    }
}