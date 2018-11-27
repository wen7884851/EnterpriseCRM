using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models
{
    public class ProjectViewModel
    {
        public int? Id { get; set; }
        public string ProjectName { get; set; }
        public int? ProjectLeader { get; set; }
        public string Content { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
