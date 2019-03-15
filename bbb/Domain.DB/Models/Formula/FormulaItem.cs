using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Tool.Entity;

namespace Domain.DB.Models
{
    public class FormulaItem : EntityBase<int>, ICreationAudited, IModificationAudited
    {
        public FormulaItem()
        {
            this.formula = new Formula();
        }
        public int FormulaId { get; set; }
        public int ProjectPointId { get; set; }
        public string ItemName { get; set; }
        public decimal Value { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }
        public Formula formula { get; set; }
    }
}
