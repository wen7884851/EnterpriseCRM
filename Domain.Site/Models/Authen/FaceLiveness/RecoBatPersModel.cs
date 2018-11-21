using Framework.Tool.Entity;
using Domain.Site.Models.Authen.FaceLiveness.Person;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Site.Models.Authen.FaceLiveness
{
    /// <summary>
    /// 人员历史记录
    /// </summary>
    public class RecoBatPersModel : EntityCommon
    {
        public RecoBatPersModel()
        {
            Search = new Searchmodel();
            //Images = new List<ImagesModel>();
            PsImages = new List<PsImage>();
        }
        public int ID { get; set; }
        [Display(Name = "姓名")]
        [Required(ErrorMessage = "姓名不能为空")]
        public string RealName { get; set; }
        [Display(Name = "身份证")]
        [RegularExpression("\\d{17}[\\d|x]|\\d{15}", ErrorMessage = "身份证号格式错误")]
        [Required(ErrorMessage = "身份证不能为空")]
        public string IDCard { get; set; }
        [Display(Name = "性别")]
        public virtual int? Sex { get; set; }
        [Display(Name = "认证状态")]
        public int State { get; set; }
        public DateTime CheckTimes { get; set; }
        [Display(Name = "区域")]
        public int AreaID { get; set; }
        [Display(Name = "批次名称")]
        public string BatchName { get; set; }
        public int? LastFLID { get; set; }
        public int OrgID { get; set; }
        [Display(Name = "应用信息")]
        public string OrgName { get; set; }
        public string BatchGid { get; set; }
       // public List<ImagesModel> Images { get; set; }
        public List<PsImage> PsImages { get; set; }
        public int BatchState { get; set; }
        [Display(Name = "图片")]
        public string Photo { get; set; }
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
        public string Tittle { get; set; }
        [Display(Name = "手机号码")]
        public string Phone { get; set; }
        [Display(Name = "地址")]
        public string Address { get; set; }
        [Display(Name = "出生年月")]
        public string Birthday { get; set; }
        [Display(Name = "身份证地址")]
        public string IDCardAddress { get; set; }
        [Display(Name = "认证次数")]
        [Required(ErrorMessage = "认证次数不能为空")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "次数必须大于0")]
        public string Certification { get; set; }
        public Searchmodel Search { get; set; }
    }
    public class Searchmodel
    {
        [Display(Name = "应用信息")]
        public int OrgId { get; set; }

        [Display(Name = "身份证")]
        public string IDCard { get; set; }
        [Display(Name = "姓名")]
        public string RealName { get; set; }
        [Display(Name = "批次名称")]
        public string BatchGid { get; set; }
        [Display(Name = "认证状态")]
        public int State { get; set; }
    }

}
