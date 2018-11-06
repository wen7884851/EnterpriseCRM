using Domain.Site.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Site.Models.Authen.Wx.YFPerson
{
    public class YFPersonModel : EntityCommon
    {
        public YFPersonModel()
        {
            SearchModel = new SearchModel();
        }

        public int Id { get; set; }

        public int CId { get; set; }


        public string RealName { get; set; }

        public bool? IsCheck { get; set; }

        public string IDCard { get; set; }

        [Display(Name = "用户的昵称")]
        public string Nickname { get; set; }

        [Display(Name = "用户的性别，值为1时是男性，值为2时是女性，值为0时是未知")]
        public int? Sex { get; set; }

        [Display(Name = "出生日期")]
        public DateTime? Birthday { get; set; }

        [Display(Name = "个人类别")]
        public string YFType { get; set; }

        [Display(Name = "地址")]
        public string AddressPart { get; set; }

        public SearchModel SearchModel { get; set; }

        public int Age
        {
            get
            {
                int m_Y1 = DateTime.Parse(Birthday.ToString()).Year;
                int m_Y2 = DateTime.Now.Year;
                int m_Age = m_Y2 - m_Y1;
                return m_Age;
            }
            set { }
        }

        public string SexText
        {
            get
            {
                if (Sex==1)
                {
                    return "男";
                }
                else if (Sex==2)
                {
                    return "女";
                }
                else
                {
                    return "未知";
                }
            }
            set { }
        }
        public string EnabledText
        {
            get
            {
                if (IsCheck == true)
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
        }

        public List<SelectListItem> EnabledItems { get; set; }

        //[Display(Name = "用户的标识")]
        //public string OpenId { get; set; }

        //[Display(Name = "当前微信号")]
        //public string Wx_UserName { get; set; }

       

        [Display(Name = "是否认证")]
        public bool? IsCheck { get; set; }

        [Display(Name = "姓名")]
        public string RealName { get; set; }

        [Display(Name = "身份证")]
        [StringLength(18, MinimumLength = 15, ErrorMessage = "身份证{2}～{1}个字符")]
        public string IDCard { get; set; }




    }



}
