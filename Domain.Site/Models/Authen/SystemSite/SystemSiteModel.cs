using Core.Tool.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models.Authen.SystemSite
{
    public class SystemSiteModel: EntityCommon
    {
        public SystemSiteModel()
        {
            Search = new SearchModel();
        }
        public int Id { get; set; }
        [Display(Name = "系统名称")]
        [Required(ErrorMessage = "系统名称不能为空")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "系统名称{2}～{1}个字符")]
        public string SystemName{ get; set; }
        [Display(Name = "系统Url")]
        [Required(ErrorMessage ="系统Url不能为空")]
        public string Url { get; set; }
        [Display(Name = "系统描述")]
        public string Description { get; set; }
        [Display(Name = "系统图标")]
        public string Icon { get; set; }
        [Display(Name = "系统代码")]
        public string Code { get; set; }
        public SearchModel Search { get; set; }
        public string SystemGid { get; set; }
    }
    public class SearchModel
    {
        [Display(Name = "系统名称")]
        public string SystemName { get; set; }

        [Display(Name = "系统地址")]
        public string SystemUrl { get; set; }
    }
}
