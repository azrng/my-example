using NetByDocker.Model.Enum;

namespace NetByDocker.Model.UserManage
{
    /// <summary>
    ///获取用户列表
    /// </summary>
    public class GetUserPageInfoResponse
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public SexEnum Sex { get; set; }

        /// <summary>
        /// 学分
        /// </summary>
        public double Credit { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 班级ID
        /// </summary>
        public long ClassId { get; set; }
    }
}