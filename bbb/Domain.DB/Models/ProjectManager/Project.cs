using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Tool.Entity;

namespace Domain.DB.Models
{
    public class Project : EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public Project()
        {
            this.points = new List<ProjectPoint>();
        }
        public string ProjectName { get; set; }
        public int? ProjectLeader { get; set; }
        public string Content { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public string LinkPerson { get; set; }
        public string LinkPhoneNo { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }
        public ICollection<ProjectPoint> points { get; set; }
    }
}
