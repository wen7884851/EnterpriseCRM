using Domain.Site.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Site.Models.System.Module
{
	public class ModuleModel : EntityCommon
    {
		public ModuleModel()
		{
            Enabled = true;
			IsMenu = true;
			Search = new SearchModel();
            ParentModuleItems = new List<SelectListItem>() {
                new SelectListItem { Text = "--- 请选择 ---", Value = "0"}, 
            };
            LayerItems = new List<SelectListItem>()
            {
                new SelectListItem { Text="--- 请选择 ---",Value="0" }
            };
		}

        public int Id { get; set; }
        
        [Display(Name = "模块名称")]
        [Required(ErrorMessage = "模块名称不能为空")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "模块名称{2}～{1}个字符")]
        public string Name { get; set; }

        [Display(Name = "模块编号")]
        [Required(ErrorMessage = "模块编号不能为空")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "模块编号{2}～{1}个字符")]
        public string Code { get; set; }
        [Display(Name="菜单层级")]
        [Remote("CheckLayer","Module", ErrorMessage = "请选择菜单层级")]
        public int Layer { get; set; }
        public List<SelectListItem> LayerItems { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        [Remote("CheckParentId", "Module", ErrorMessage = "请选择父节点")]
        public int? ParentId { get; set; }

        [Display(Name = "上级模块")]
        public string ParentName { get; set; }
        public List<SelectListItem> ParentModuleItems { get; set; }

        [Display(Name = "链接地址")]
		[StringLength(50, MinimumLength = 2, ErrorMessage = "链接地址{2}～{1}个字符")]
        public string LinkUrl { get; set; }

        [Display(Name = "菜单图标")]
        public string Icon { get; set; }

        [Display(Name = "排序")]
        [Required(ErrorMessage = "排序不能为空")]
        [RegularExpression(@"\d+", ErrorMessage = "排序必须是数字")]
        public int OrderSort { get; set; }

		[Display(Name = "是否菜单")]
        public bool IsMenu { get; set; }

        [Display(Name = "系统名称")]
        public int SystemID { get; set; }

        [Display(Name = "系统名称")]
        public string SystemName { get; set; }

        public string SystemGid { get; set; }
        [Display(Name ="同级")]
        public bool IsWith { get; set; }
        public string MenuText
		{
			get
			{
				if (IsMenu == true)
				{
					return "是";
				}
				else
				{
					return "否";
				}
			}
			set { }
		}

        [Display(Name = "是否激活")]
        public bool Enabled { get; set; }

		public string EnabledText
		{
			get
			{
				if (Enabled == true)
				{
					return "是";
				}
				else
				{
					return "否";
				}
			}
			set { }
		}

		public SearchModel Search { get; set; }
    }

	public class SearchModel
	{
		public SearchModel()
		{
			EnabledItems = new List<SelectListItem> { 
                new SelectListItem { Text = "--- 请选择 ---", Value = "-1", Selected = true }, 
                new SelectListItem { Text = "是", Value = "1" }, 
                new SelectListItem { Text = "否", Value = "0" }
            };
			ParentModuleItems = new List<SelectListItem>() {
                new SelectListItem { Text = "--- 请选择 ---", Value = "0"}, 
            };
            SystemModuleItems = new List<SelectListItem>() {
                new SelectListItem { Text = "--- 请选择 ---", Value = "0"},
            };
        }

		public int? ParentId { get; set; }


        public List<SelectListItem> SystemModuleItems { get; set; }

        [Display(Name = "模块名称")]
		public string Name { get; set; }

		[Display(Name = "链接地址")]
		public string LinkUrl { get; set; }

		[Display(Name = "上级模块")]
		public string ParentName { get; set; }
		public List<SelectListItem> ParentModuleItems { get; set; }

		[Display(Name = "是否菜单")]
		public bool IsMenu { get; set; }

		[Display(Name = "是否已激活")]
		public bool Enabled { get; set; }
        [Display(Name = "系统名称")]
        public int SystemID { get; set; }

        [Display(Name = "系统名称")]
        public string SystemName { get; set; }
        public List<SelectListItem> EnabledItems { get; set; }
	}
}
