//**********************************
//*ClassName:
//*Version:
//*Date:
//*Author:
//*Effect:
//**********************************

using Domain.Site.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models.Wx
{
   public class MessageModel : EntityCommon
    {
        public MessageModel() {
            Searchmodel = new SearchModel();
        }
        public int Id { get; set; }
        public int PlatformID { get; set; }
        [Display(Name = "用户的信息Id")]
        public int PsId { get; set; }
        [Display(Name = "接受消息用户")]
        public int UserAccept { get; set; }
        [Display(Name = "Gid随机生成，用于分组")]
        public string Gid { get; set; }
        [Display(Name = "消息创建时间")]
        public DateTime Time { get; set; }
        [Display(Name = "消息类型")]
        public EnmMsgType MsgType { get; set; }
        [Display(Name = "消息类容")]
        public string Content { get; set; }
        [Display(Name = "媒体Id")]
        public string MediaId { get; set; }
        [Display(Name = "消息Id")]
        public long MsgId { get; set; }
        public bool IsRead { get; set; }

        [Display(Name ="姓名")]
        public string RealName { get; set; }
        [Display(Name = "身份证")]
        [StringLength(18, MinimumLength = 15, ErrorMessage = "身份证{2}-{1}个字符")]
        public string IDCard { get; set; }
        public string Nickname { get; set; }
        public string Headimgurl { get; set; }
        public EnmModeType ModeType { get; set; }
       
        public SearchModel Searchmodel { get; set; }

        public class SearchModel {
            [Display(Name = "姓名")]
            public string RealName { get; set; }
            [Display(Name ="身份证")]
            [StringLength(18,MinimumLength =15,ErrorMessage ="身份证{2}-{1}个字符")]
            public string IDCard { get; set; }
        }
    }
}
