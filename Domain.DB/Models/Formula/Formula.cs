using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Tool.Entity;

namespace Domain.DB.Models
{
    public class Formula : EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public Formula()
        {
            this.formulaItems = new List<FormulaItem>();   
        }
        public string Name { get; set; }
        public string FormulaContent { get; set; }
        public int FormulaType { get; set; }
        public string Introduce { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }
        public IEnumerable<FormulaItem> formulaItems { get; set; }

    }
}
