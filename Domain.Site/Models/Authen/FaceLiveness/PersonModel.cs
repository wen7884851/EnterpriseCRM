using Core.Tool.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Site.Models.Authen.FaceLiveness.Person
{
    /// <summary>
    /// 人员信息model
    /// </summary>
    public class PersonModel : EntityCommon
    {
        public PersonModel()
        {
            Search = new SearchModel();
        }
        public int ID { get; set; }
        [Display(Name = "身份证")]
        [Required(ErrorMessage = "身份证不能为空")]
        [RegularExpression(@"\d{17}[\d|x]|\d{15}", ErrorMessage = "身份证号格式错误")]
        public string IDCard { get; set; }
        [Display(Name = "姓名")]
        [Required(ErrorMessage = "姓名不能为空")]
        public string RealName { get; set; }
        [RegularExpression(@"\d{11}", ErrorMessage = "联系电话格式错误")]
        [Display(Name = "联系电话")]
        public string Phone { get; set; }
        [Display(Name = "性别")]
        public int? Sex { get; set; }
        [Display(Name = "地址")]
        public string Address { get; set; }
        [Display(Name = "出生日期")]
        public string Birthday { get; set; }
        [Display(Name = "年龄")]
        public int Age { get; set; }
        [Display(Name = "照片")]
        public string Photo { get; set; }
        [Display(Name = "区域")]
        public int AreaID { get; set; }
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
        public string Tittle { get; set; }
        public int OrgID { get; set; }
        public int BatchID { get; set; }
        public DateTime? CheckTimes { get; set; }
        [Display(Name = "身份证地址")]
        public string IDCardAddress { get; set; }

        public int? LastFLID { get; set; }
        public int? State { get; set; }
        public List<PsImage> PsImages { get; set; }
        public List<ImagesModel> Images { get; set; }
        public SearchModel Search { get; set; }

    }
    public class SearchModel
    {
        public SearchModel()
        {
            OrgItems = new List<SelectListItem>() {
                new SelectListItem { Text = "--- 请选择 ---", Value = "0"},
            };
            StateItems = new List<SelectListItem>()
            {
                new SelectListItem { Text = "--- 请选择 ---", Value = "-1"},
            };
        }


        [Display(Name = "应用信息")]
        public int OrgId { get; set; }
        [Display(Name = "身份证")]
        public string IDCard { get; set; }
        [Display(Name = "姓名")]
        public string RealName { get; set; }
        [Display(Name = "批次名称")]
        public string BatchGid { get; set; }
        public List<SelectListItem> OrgItems { get; set; }
        public List<SelectListItem> StateItems { get; set; }
        [Display(Name = "当前认证状态")]
        public int State { get; set; }
    }

    public class PsImage
    {
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
        public string Tittle { get; set; }
    }
}
