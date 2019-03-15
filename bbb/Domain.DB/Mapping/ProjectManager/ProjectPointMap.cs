using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DB.Mapping
{
    partial class ProjectPointMap
    {
        partial void ProjectPointMapAppend()
        {
            // Primary Key
            this.Property(t => t.Id).HasColumnName("Id");
            Property(t => t.ProjectId).HasColumnName("ProjectId");
            Property(t => t.PointName).HasColumnName("PointName");
            Property(t => t.Status).HasColumnName("Status");
            Property(t => t.PointFund).HasColumnName("PointFund");
            Property(t => t.Budget).HasColumnName("Budget");
            Property(t => t.Tax).HasColumnName("Tax");
            Property(t => t.FormulaId).HasColumnName("FormulaId");
            Property(t => t.UserId).HasColumnName("UserId");
            Property(t => t.UserTax).HasColumnName("UserTax");
            Property(t => t.Commission).HasColumnName("Commission");
            Property(t => t.CreateId).HasColumnName("CreateId");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateTime).HasColumnName("CreateTime");
            Property(t => t.ModifyId).HasColumnName("ModifyId");
            Property(t => t.ModifyBy).HasColumnName("ModifyBy");
            Property(t => t.ModifyTime).HasColumnName("ModifyTime");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            ToTable("Cms_Project_Point");
            //表的引用关系映射
            this.HasOptional(t => t.project).WithMany(t => t.points).HasForeignKey(d => d.ProjectId);
        }
    }
}
