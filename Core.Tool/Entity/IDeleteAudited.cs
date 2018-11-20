using System;

namespace Framework.Tool.Entity
{
    public interface IDeleteAudited 
    {
        /// <summary>
        /// 逻辑删除标记
        /// </summary>
        bool? DeleteMark { get; set; }

        /// <summary>
        /// 删除实体的用户
        /// </summary>
        int DeleteUserId { get; set; }

        /// <summary>
        /// 删除实体时间
        /// </summary>
        DateTime? DeleteTime { get; set; } 
    }
}