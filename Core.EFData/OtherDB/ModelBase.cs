//**********************************
//*ClassName:
//*Version:
//*Date:
//*Author:
//*Effect:
//**********************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EFData.OtherDB
{
    public class ModelBase
    {
        public ModelBase()
        {
            CreateTime = DateTime.Now;
            IsDeleted = false;
        }

        public virtual int ID { get; set; }
        public virtual DateTime CreateTime { get; set; }

        /// <summary>
        ///获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        public bool? IsDeleted { get; set; }
    }
}
