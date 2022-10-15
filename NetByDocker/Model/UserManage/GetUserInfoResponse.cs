using NetByDocker.Model.Enum;

namespace NetByDocker.Model.UserManage
{
    /// <summary>
    /// 获取用户详情返回类
    /// </summary>
    public class GetUserInfoResponse
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public SexEnum Sex { get; set; }
    }
}