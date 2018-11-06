using Domain.Site.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Site.Models.Authen.Wx
{
    public class PlantForm : EntityCommon
    {
        public PlantForm()
        {
            Search = new SearchModel();
            SelectedUserList = new List<int>();
        }
        public int ID { get; set; }
        [Display(Name = "公众号名")]
        [Required(ErrorMessage = "公众号名称不能为空")]
        public string PlatformName { get; set; }
        [Display(Name = "AppId")]
        [Required(ErrorMessage = "AppId不能为空")]
        public string AppId { get; set; }
        [Display(Name = "公众号密钥")]
        [Required(ErrorMessage = "公众号密钥不能为空")]
        public string Secret { get; set; }
        [Display(Name = "商户号")]
        [Required(ErrorMessage = "商户号不能为空")]
        public  string MCHID { get; set; }
        [Display(Name = "key")]
        [Required(ErrorMessage = "Key不能为空")]
        public string KEY { get; set; }

        /// <summary>
        /// 平台用户关系List
        /// </summary>
        public List<UserBindPlantForm> UserPlantForm { get; set; }
        public ICollection<KeyValueModel> UserList { get; set; }

        [KeyValue(DisplayProperty = "UserList")]
        public ICollection<int> SelectedUserList { get; set; }
        public SearchModel Search { get; set; }
    }
    public class SearchModel
    {

        [Display(Name = "公众号名")]
        public string PlatformName { get; set; }
    }
}
