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
        public string LeaderName { get; set; }
        public string Content { get; set; }
        public string LinkPerson { get; set; }
        public string LinkPhoneNo { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public string CreateTime { get; set; }
    }

    public class ProjectSerchModel:DataTableParameter
    {
        public int? projectId { get; set; }
        public string projectName { get; set; }
    }
}
