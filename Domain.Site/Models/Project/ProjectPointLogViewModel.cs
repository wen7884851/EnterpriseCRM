using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models
{
    public class ProjectPointLogViewModel
    {
        public int? Id { get; set; }
        public int ProjectPointId { get; set; }
        public int LogType { get; set; }
        public string Content { get; set; }
    }
}
