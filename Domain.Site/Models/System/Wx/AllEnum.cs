using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models.Wx
{
   public enum EnmMsgType
    {
        text,
        image,
        voice,
        video,
        location,
        link,
        Event,
        sransfer_customer_service
    }
    public enum EnmModeType
    {
        /// <summary>
        /// 接收
        /// </summary>
        Receive,
        /// <summary>
        /// 回复
        /// </summary>
        Reply
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum EnumGender
    {
        [Description("男性")]
        Male = 1,
        [Description("女性")]
        Female = 2,
        [Description("未知")]
        Unknown = 0
    }

    /// <summary>
    /// 对象类型
    /// </summary>
    public enum RoleType
    {
        /// <summary>
        /// 角色
        /// </summary>
        Role,
        /// <summary>
        /// 用户
        /// </summary>
        User
    }
}
