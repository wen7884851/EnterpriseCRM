using Core.Tool.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Site.Models.System.FaceLiveness
{
    public class OrgModel: EntityCommon
    {
        public OrgModel()
        {
            CityList = new List<SelectListItem>();
            AreaList = new List<SelectListItem>();
            TownList = new List<SelectListItem>();
            Search = new SearchModel();
            HspLevelList = new List<SelectListItem>();
        }
        public int ID { get; set; }
        [Display(Name ="应用名")]
        public string OrgName { get; set; }
        [Display(Name = "应用级别")]
        public int HspLevel { get; set; }
        [Display(Name = "区县")]
        public int AreaID { get; set; }
        public int PsNum { get; set; }
        public int SecNum { get; set; }
        public int CityID { get; set; }
        [Display(Name = "认证次数上限")]
        [Required(ErrorMessage = "认证次数不能为空")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "次数必须大于0")]
        public int Ceiling { get; set; }
        public ICollection<SelectListItem> CityList { get; set; }
        public ICollection<SelectListItem> AreaList { get; set; }
        public List<SelectListItem> HspLevelList { get; set; }
        //乡镇
        public List<SelectListItem> TownList { get; set; }
        public SearchModel Search { get; set; }

    }
    public class SearchModel
    {
        [Display(Name = "应用名")]
        public string OrgName { get; set; }
    }
}
