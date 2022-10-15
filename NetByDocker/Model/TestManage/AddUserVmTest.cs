using NetByDocker.Model.Enum;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NetByDocker.Model.TestManage
{
    /// <summary>
    /// 添加用户请求类
    /// </summary>
    public class AddUserVmTest
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "帐号不能为空")]
        [MinLength(6, ErrorMessage = "帐号必须大于6位")]
        [MaxLength(10, ErrorMessage = "帐号必须小于10位")]
        public string Account { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空")]
        [MaxLength(10, ErrorMessage = "姓名必须小于10位")]
        public string Name { get; set; }


        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        [MinLength(6, ErrorMessage = "密码必须大于6位")]
        [MaxLength(10, ErrorMessage = "密码必须小于10位")]
        public string PassWord { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Required(ErrorMessage = "性别不能为空")]
        public SexEnum Sex { get; set; }

        /// <summary>
        /// 学分
        /// </summary>
        [Required(ErrorMessage = "学分不能为空")]
        public double Credit { get; set; }

        /// <summary>
        /// 班级ID
        /// </summary>
        [Required(ErrorMessage = "班级ID不能为空")]
        public long ClassId { get; set; }
    }

    /// <summary>
    /// 更新用户请求类
    /// </summary>
    public class UpdateUserVmTest
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "帐号不能为空")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MinLength(6)]
        [Required(ErrorMessage = "密码不能为空")]
        public string PassWord { get; set; }
    }
}