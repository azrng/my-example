namespace NetByDocker.Model.TestManage
{
    /// <summary>
    /// 获取班级用户信息
    /// </summary>
    public class GetGroupUserInfoResponseTest
    {
        /// <summary>
        /// 班级ID
        /// </summary>
        public long GroupId { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        public string GradeName { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        ///成绩
        /// </summary>
        public int Grade { get; set; }
    }
}