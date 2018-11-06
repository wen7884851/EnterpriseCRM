using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Core.Tool.Entity;
using System.Web.Mvc;

namespace Domain.Site.Models.System.FaceLiveness
{
    /// <summary>
    /// 机构信息Model
    /// </summary>
    public class BatchModel : EntityCommon
    {
        public BatchModel() {
            Search = new SearchModel();
        }
        public int Id { get; set; }
        [Display(Name = "名称")]
        public string BatchCtmName { get; set; }
        [Display(Name = "批次号")]
        public string BatchName { get; set; }
        [Display(Name = "应用ID")]
        public int OrgID { get; set; }
        [Display(Name = "Gid")]
        public string Gid { get; set; }
        [Display(Name ="认证数量")]
         public int Num { get; set; }
        [Display(Name = "开始时间")]
        public DateTime StartTime { get; set; }
        [Display(Name = "结束时间")]
        public DateTime EndTime { get; set; }
        [Display(Name = "状态")]
        public int State { get; set; }
        [Display(Name ="结束状态")]
        public int Expireddelay { get; set; }
        public SearchModel Search { get; set; }
        public RecordModel Records { get; set; }
        public class RecordModel
        {
            public string BatchGid { get; set; }

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
            public List<SelectListItem> OrgItems { get; set; }
            public List<SelectListItem> StateItems { get; set; }
            [Display(Name = "认证状态")]
            public int State { get; set; }
        }
    }
}
