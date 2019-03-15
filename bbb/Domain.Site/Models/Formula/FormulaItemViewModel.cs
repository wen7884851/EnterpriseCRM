using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models
{
    public class FormulaItemViewModel
    {
        public int Id { get; set; }
        public int FormulaId { get; set; }
        public int ProjectPointId { get; set; }
        public string ItemName { get; set; }
        public decimal Value { get; set; }
    }
}
