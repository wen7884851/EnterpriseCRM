using Domain.Site.Common.Models;
using Domain.Site.Models.Wx;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models.Authen.Wx
{
    public class Reg_YFPresonModel : EntityCommon
    {
        [Display(Name = "姓名")]
        [Required(ErrorMessage = "姓名不能为空")]
        public string RealName { get; set; }
        [Display(Name = "身份证")]
        [Required(ErrorMessage = "身份证不能为空")]
        [RegularExpression(@"\d{17}[\d|x]|\d{15}", ErrorMessage = "身份证格式不正确")]
        public string IdCard { get; set; }
        public string AddressPart { get; set; }
        [Display(Name = "地址")]
        public int? AddressID { get; set; }
        [Display(Name = "性别")]
        public bool? Sex { get; set; }
        [Display(Name = "电话")]
        public string Phone { get; set; }
        [Display(Name = "救助类型")]
        public string TypeName { get; set; }
        [Display(Name = "出生日期")]
        public DateTime? Birthday { get; set; }
        [Display(Name = "救助时间")]
        public DateTime? SalvDate { get; set; }
        [Display(Name = "入院时间")]
        public DateTime? InHspt { get; set; }
        [Display(Name = "出院时间")]
        public DateTime? OutHspt { get; set; }
        [Display(Name = "总金额")]
        public Decimal TotalMoney { get; set; }
        [Display(Name = "救助金额")]
        public Decimal SalvMoney { get; set; }
        [Display(Name = "医院名称")]
        public string HsptName { get; set; }

        public ConsumerModel Consumer { get; set; }
    }
}
