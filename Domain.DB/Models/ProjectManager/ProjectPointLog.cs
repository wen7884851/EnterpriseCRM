using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Tool.Entity;

namespace Domain.DB.Models
{
    public class PointLog : EntityBase<int>, ICreationAudited
    {
        public int ProjectPointId { get; set; }
        public int LogType { get; set; }
        public string Content { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
