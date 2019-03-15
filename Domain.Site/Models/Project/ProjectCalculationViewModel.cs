using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models
{
    public class ProjectCalculationViewModel
    {
        public int ProjectTypeId { get; set; }
        public int ProfessionalTypeId { get; set; }
        public decimal PointFund { get; set; }
    }
}
