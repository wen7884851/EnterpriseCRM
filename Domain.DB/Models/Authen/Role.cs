using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Framework.Tool.Entity;

namespace Domain.DB.Models
{
    public class Role : EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public Role()
        {
			this.Users = new List<User>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
