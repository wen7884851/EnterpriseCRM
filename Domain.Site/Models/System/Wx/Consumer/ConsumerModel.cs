//**********************************
//*ClassName:
//*Version:
//*Date:
//*Author:
//*Effect:
//**********************************

using Domain.Site.Common.Models;
using Domain.Site.Models.System.Wx;
using Domain.Site.Models.System.Wx.YFPerson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models.Wx
{
    public class ConsumerModel : EntityCommon
    {
        [Display(Name = "用户的标识")]
        public string openId { get; set; }
        [Display(Name = "当前微信号")]
        public string wx_UserName { get; set; }
        [Display(Name = "用户的昵称")]
        public string nickname { get; set; }
        [Display(Name = "用户的性别，值为1时是男性，值为2时是女性，值为0时是未知")]
        public int? sex { get; set; }
        [Display(Name = "用户所在城市")]
        public string city { get; set; }
        [Display(Name = "用户所在国家")]
        public string country { get; set; }
        [Display(Name = "用户所在省份")]
        public string province { get; set; }
        [Display(Name = "用户的语言，简体中文为zh_CN")]
        public string language { get; set; }
        [Display(Name = "头像")]
        public string headimgurl { get; set; }
        [Display(Name = "用户关注时间")]
        public long subscribe_time { get; set; }
        [Display(Name = "只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。")]
        public string unionid { get; set; }
        [Display(Name = "备注")]
        public string remark { get; set; }
        [Display(Name = "分组ID")]
        public int groupId { get; set; }
        [Display(Name = "用户被打上的标签ID列表")]
        public List<int> tagid_list { get; set; }
        public int PlatformID { get; set; }

        [Display(Name = "用户绑定凭据")]
        public string Psid { get; set; }



  
    }
}
