//**********************************
//*ClassName:
//*Version:
//*Date:
//*Author:
//*Effect:
//**********************************

using System;
using Core.Tool.Entity;

namespace Domain.DB.Models.Authen
{
    public class Area : EntityBase<int>, ICreationAudited, IModificationAudited
    {

        public virtual string AreaName { get; set; }

        public virtual string AreaCode { get; set; }

        public virtual int? CityID { get; set; }

        public virtual int? Estate { get; set; }

        public virtual int? Layer { get; set; }

        public virtual int? Parent { get; set; }

        public virtual int? CreateId { get; set; }

        public virtual string CreateBy { get; set; }

        public virtual DateTime? CreateTime { get; set; }

        public virtual int? ModifyId { get; set; }

        public virtual string ModifyBy { get; set; }

        public virtual DateTime? ModifyTime { get; set; }

        public virtual City City { get; set; }
    }
}
