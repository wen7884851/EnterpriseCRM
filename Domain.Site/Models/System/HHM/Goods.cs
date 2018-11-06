using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Tool.Entity;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domain.Site.Models.System.HHM
{
    public class Goods:EntityCommon
    {

        //public Good()
        //{
        //    ApplyList = new ApplyList();
        //    HostoryList = new HostoryList();
        //    tInventoryList = new InventoryList();
        //    Manufacturer = new Manufacturer();
        //}
        public virtual int Id { get; set; }

        /// <summary>
        /// 物品名称
        /// </summary>
        public virtual string GoodName { get; set; }

        /// <summary>
        /// 物品类别ID
        /// </summary>
        public virtual int GTypeID { get; set; }

        /// <summary>
        /// 条码号
        /// </summary>
        public virtual int? BarCode { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public virtual string Spec { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public virtual string Unit { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public virtual string Model { get; set; }

        /// <summary>
        /// 厂商ID
        /// </summary>
        public virtual int? VenderID { get; set; }

        /// <summary>
        /// 管理方式
        /// </summary>
        public virtual int? ManegStyle { get; set; }

        /// <summary>
        /// 物品状态
        /// </summary>
        public virtual int States { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        public virtual string PYCode { get; set; }

        public virtual string WBCode { get; set; }

        /// <summary>
        /// 账簿类别（方便财务统计)
        /// </summary>
        public virtual int? AccountStyle { get; set; }

        /// <summary>
        /// 库位标识
        /// </summary>
        public virtual string Library { get; set; }

        /// <summary>
        /// 预警低值
        /// </summary>
        public virtual decimal? Low_order { get; set; }

        /// <summary>
        /// 预警高值
        /// </summary>
        public virtual decimal? High_order { get; set; }

        /// <summary>
        /// 预警天数
        /// </summary>
        public virtual int? WarningDays { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public virtual string Attribute { get; set; }

        /// <summary>
        /// 折旧公式ID
        /// </summary>
        public virtual int? DepreciationID { get; set; }

        /// <summary>
        /// 折旧年限
        /// </summary>
        public virtual decimal? DYear { get; set; }

        public virtual decimal? Cost_Price { get; set; }

        public virtual string ControlCode { get; set; }

        public virtual int? OldId { get; set; }


        public virtual int? CreateId { get; set; }

        public virtual string CreateBy { get; set; }

        public virtual DateTime? CreateTime { get; set; }

        public virtual int? ModifyId { get; set; }

        public virtual string ModifyBy { get; set; }

        public virtual DateTime? ModifyTime { get; set; }

    }
}
