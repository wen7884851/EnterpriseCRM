using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models
{
    public class ProjectPointViewModel
    {
        public int? Id { get; set; }
        public int ProjectId { get; set; }
        public string PointName { get; set; }
        public int Status { get; set; }
        public decimal Budget { get; set; }
        public decimal Tax { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
