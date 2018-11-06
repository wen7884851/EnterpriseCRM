//**********************************
//*ClassName:
//*Version:
//*Date:
//*Author:
//*Effect:
//**********************************

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Core.Tool.Entity
{
    [Serializable]
    public abstract class EntityHHM<TKey> : IEntity<TKey>
    {
        #region 构造函数
        protected EntityHHM()
        {
            IsDeleted = false;
        }
        #endregion

        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public TKey Id { get; set; }

        /// <summary>
        ///获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        public bool? IsDeleted { get; set; }
        #endregion
    }
}
