using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Site.Models.System.Wx.YFPersonPermission
{
    /// <summary>
	/// 
	/// </summary>
	public class YFPersonPermissionModel
    {
        public YFPersonPermissionModel()
        {
            this.YFPersonData = new YFPersonSelecteModel();
            this.YFSalvRecordData = new YFSalvRecordModel();
        }

        public int ConsumerId { get; set; }
        public string OpenId { get; set; }
        [Display(Name = "微信号")]
        public string Wx_UserName { get; set; }
        public string Nickname { get; set; }
        public int? Sex { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string Language { get; set; }
        [Display(Name = "头像")]
        public string Headimgurl { get; set; }
        public DateTime? Subscribe_time { get; set; }
        public string Unionid { get; set; }
        public string Remark { get; set; }
        public int? GroupId { get; set; }
        public string Tagid_list { get; set; }
        public int PlatformID { get; set; }

        public string Psid { get; set; }

        public YFPersonSelecteModel YFPersonData { get; set; }

        public YFSalvRecordModel YFSalvRecordData { get; set; }
    }


    public class YFPersonSelecteModel
    {
        public int YFPersonId { get; set; }

        [Display(Name = "姓名")]
        public string RealName { get; set; }
        [Display(Name = "是否认证")]
        public bool IsCheck { get; set; }
        [Display(Name = "身份证")]
        public string IDCard { get; set; }
        public string Nickname { get; set; }

        //   [ValueDescription(EnumType = typeof(EnumGender))]
        [Display(Name = "性别")]
        public int? Sex { get; set; }

        public DateTime? Birthday { get; set; }
        [Display(Name = "手机号")]
        public string Phone { get; set; }
        public DateTime? CreateTime { get; set; }
        [Display(Name = "个人类别")]
        public string YFType { get; set; }
        [Display(Name = "地址")]
        public string AddressPart { get; set; }

    }

    public class YFSalvRecordModel
    {
        public int Id { get; set; }

        [Display(Name = "姓名")]
        public string RealName { get; set; }

        [Display(Name = "身份证")]
        public string IDCard { get; set; }

        public int PsID { get; set; }
        [Display(Name = "救助金额")]
        public decimal SalvMoney { get; set; }
        [Display(Name = "救助时间")]
        public DateTime SalvDate { get; set; }
        [Display(Name = "医院名称")]
        public string Hspt { get; set; }

        [Display(Name = "总金额")]
        public decimal TotalMoney { get; set; }

        [Display(Name = "入院日期")]
        public DateTime InHspt { get; set; }

        [Display(Name = "出院日期")]
        public DateTime OutHspt { get; set; }

    }
}