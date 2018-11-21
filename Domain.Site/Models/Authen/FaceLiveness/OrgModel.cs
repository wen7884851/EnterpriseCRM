using Framework.Tool.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models.Authen.FaceLiveness
{
    public class OrgModel: EntityCommon
    {
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
        public SearchModel Search { get; set; }

    }
    public class SearchModel
    {
        [Display(Name = "应用名")]
        public string OrgName { get; set; }
    }
}
