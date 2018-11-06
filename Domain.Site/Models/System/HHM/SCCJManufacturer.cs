using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models.System.HHM
{
   public class SCCJManufacturer
    {
        #region Property Members


        public virtual int id { get; set; }
        /// <summary>
        /// 法人代表
        /// </summary>
        public virtual string LegalPerson { get; set; }

        /// <summary>
        /// 厂家称呼
        /// </summary>
        public virtual string ManuName { get; set; }

        /// <summary>
        /// 厂家全称
        /// </summary>
        public virtual string ManuRealName { get; set; }

        /// <summary>
        /// 企业性质
        /// </summary>
        public virtual string Nature { get; set; }

        /// <summary>
        /// 网站
        /// </summary>
        public virtual string WebSite { get; set; }

        /// <summary>
        /// 拼音代码
        /// </summary>
        public virtual string Py { get; set; }

        /// <summary>
        /// 五笔代码
        /// </summary>
        public virtual string Wb { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        public virtual string Bank { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        public virtual string BankNo { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public virtual string Person { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string DHNumber { get; set; }

        /// <summary>
        /// 联系手机
        /// </summary>
        public virtual string SJNumber { get; set; }

        /// <summary>
        /// 联系邮箱
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public virtual string ZipCode { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public virtual string Fax { get; set; }

        /// <summary>
        /// 状态(0 注销 1 正常)
        /// </summary>
        public virtual int? States { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remake { get; set; }

        /// <summary>
        /// 营业执照
        /// </summary>
        public virtual string Business_Permit { get; set; }

        /// <summary>
        /// 许可证时间
        /// </summary>
        public virtual DateTime? Business_Permit_Date { get; set; }

        public virtual string Business_License { get; set; }

        public virtual DateTime? Business_License_Date { get; set; }

        public virtual int? OldId { get; set; }

        /// <summary>
        /// HostoryLists
        /// </summary>
        //public virtual IList<HostoryList> HostoryLists { get; set; }

        /// <summary>
        /// Goods
        /// </summary>
        //public virtual IList<Good> Goods { get; set; }

        public virtual int? CreateId { get; set; }

        public virtual string CreateBy { get; set; }

        public virtual DateTime? CreateTime { get; set; }

        public virtual int? ModifyId { get; set; }

        public virtual string ModifyBy { get; set; }

        public virtual DateTime? ModifyTime { get; set; }
        #endregion
    }
}
