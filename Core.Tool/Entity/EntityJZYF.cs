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
    public abstract class EntityJZYF<TKey> : IEntity<TKey> 
    {
        #region 构造函数
        protected EntityJZYF()
        {
           
        }
        #endregion

        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public TKey Id { get; set; }


        #endregion
    }
}
