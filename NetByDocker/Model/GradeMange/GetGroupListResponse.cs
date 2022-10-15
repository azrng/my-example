namespace NetByDocker.Model.GradeMange
{
    /// <summary>
    /// 获取班级列表返回类
    /// </summary>
    public class GetGroupListResponse
    {
        /// <summary>
        /// 班级ID
        /// </summary>
        public long GradeId { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        public string GradeName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
    }
}