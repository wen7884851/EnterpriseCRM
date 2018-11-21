﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Framework.Tool.Entity;

namespace Domain.Site.Models.Authen.FaceLiveness
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


    }
}
