using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models.Project
{
    public class ProjectUserStoreViewModel
    {
        public int? Id { get; set; }
        public int ProjectPointId { get; set; }
        public int UserId { get; set; }
        public string StoreContent { get; set; }
        public decimal UserTax { get; set; }
    }
}
