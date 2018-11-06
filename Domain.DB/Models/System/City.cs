//**********************************
//*ClassName:
//*Version:
//*Date:
//*Author:
//*Effect:
//**********************************


using System;
using Core.Tool.Entity;
using System.Collections.Generic;

namespace Domain.DB.Models.System
{
    public class City : EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public City()
        {
            this.Areas=new List<Area>();
        }
        public virtual int? Provice { get; set; }

        public virtual string CityName { get; set; }

        public virtual int? CreateId { get; set; }

        public virtual string CreateBy { get; set; }

        public virtual DateTime? CreateTime { get; set; }

        public virtual int? ModifyId { get; set; }

        public virtual string ModifyBy { get; set; }

        public virtual DateTime? ModifyTime { get; set; }

        /// <summary>
        /// Areas
        /// </summary>
        public virtual ICollection<Area> Areas { get; set; }
    }
}
