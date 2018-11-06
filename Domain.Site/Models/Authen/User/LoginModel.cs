using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models.Authen.User
{
    public class LoginModel
    {
        [Display(Name = "用户名")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "用户名不能为空")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "用户名{2}～{1}个字符")]
        [DataType(DataType.Text)]
        public string LoginName { get; set; }

        [Display(Name = "密  码")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "密码不能为空")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "密码必须在{2} 和{1}之间")]
        public string LoginPwd { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "邮箱必填")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9]+\.[A-Za-z]{2,4}", ErrorMessage = "{0}的格式不正确")]
        public string Email { get; set; }
    }
}
