using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Mvc;
using Core.Tool.Entity;
using Domain.Site.Common.Models;

namespace Domain.Site.Models.Authen.User
{
	public class UpdateUserModel : Core.Tool.Entity.EntityCommon
    {
        public UpdateUserModel()
        {
            RoleList = new List<KeyValueModel>();
            SelectedRoleList = new List<int>();
            SystemList = new List<KeyValueModel>();
            SelectedSystemList = new List<int>();
            Enabled = true;
            CityList = new List<SelectListItem>();
            AreaList = new List<SelectListItem>();
        }

        public int Id { get; set; }

        [Display(Name = "用户名")]
        public string LoginName { get; set; }

        [Display(Name = "电子邮件")]
        public string Email { get; set; }

        [Display(Name = "电话号码")]
        public string Phone { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "姓名不能为空")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "姓名{2}～{1}个字符")]
        public string FullName { get; set; }

        [Display(Name = "是否激活")]
        public bool Enabled { get; set; }

        [Display(Name = "注册时间")]
        public DateTime? RegisterTime { get; set; }

        [Display(Name = "最后登陆时间")]
        public DateTime? LastLoginTime { get; set; }
        public int LoginCount { get; set; }
        public ICollection<KeyValueModel> RoleList { get; set; }

        [KeyValue(DisplayProperty = "RoleList")]
        public ICollection<int> SelectedRoleList { get; set; }

        public ICollection<KeyValueModel> SystemList { get; set; }
        [KeyValue(DisplayProperty = "SystemList")]
        public ICollection<int> SelectedSystemList { get; set; }

        [Display(Name = "是否市级单位")]
        public bool IsCity { get; set; }
        [Display(Name = "区县/城市")]
        public int AreaID { get; set; }
        public int CityID { get; set; }
        public List<SelectListItem> CityList { get; set; }
        public ICollection<SelectListItem> AreaList { get; set; }

    }
}
