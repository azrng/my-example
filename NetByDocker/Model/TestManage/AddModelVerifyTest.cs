using System.ComponentModel.DataAnnotations;

namespace NetByDocker.Model.TestManage
{
    //[AddUserVerify]
    public class AddModelVerifyTest //: IValidatableObject
    {
        [Display(Name = "名称"), Required(ErrorMessage = "{0}不能为空")]
        [MinLength(6, ErrorMessage = "名称不能小于6位")]
        [MaxLength(10, ErrorMessage = "长度不超过10个")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码"), Required(ErrorMessage = "{0}不能为空")]
        [MinLength(6, ErrorMessage = "密码必须大于6位")]
        public string PassWord { get; set; }

        [Display(Name = "工号")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "{0}长度是{1}")]
        public string EmployeeNo { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }

        ///// <summary>
        ///// 属性级别的自定义验证
        ///// </summary>
        ///// <param name="validationContext"></param>
        ///// <returns></returns>
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Birthday > DateTime.Now)
        //    {
        //        yield return new ValidationResult("出生日期不能大于当前时间", new[] { nameof(Birthday) });
        //    }
        //    if (UserName == EmployeeNo)
        //    {
        //        yield return new ValidationResult("名称和工号不能一样", new[] { nameof(UserName), nameof(EmployeeNo) });
        //    }
        //}
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class AddUserVerifyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (AddModelVerifyTest)validationContext.ObjectInstance;//user 变量表示 AddUserinfoVm 对象，其中包含表单提交中的数据
            var date = (DateTime)value;
            if (date > DateTime.Now)
                return new ValidationResult("出生日期不能大于当前时间");
            if (user.UserName == user.EmployeeNo)
                return new ValidationResult("名称和工号不能一样");
            return ValidationResult.Success;
        }
    }

    public class UpdateOrderVm
    {
        public string Id { get; set; }

        public bool Enabled { get; set; }
    }
}