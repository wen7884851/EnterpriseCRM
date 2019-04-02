using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.ProfessionalTypeId).HasColumnName("ProfessionalTypeId");
            Property(t => t.ProjectId).HasColumnName("ProjectId");
            Property(t => t.PointName).HasColumnName("PointName");
            Property(t => t.Status).HasColumnName("Status");
            Property(t => t.PointCommission).HasColumnName("PointCommission");
            Property(t => t.PonitContent).HasColumnName("PonitContent");
            Property(t => t.PointProportion).HasColumnName("PointProportion");
            Property(t => t.CreateId).HasColumnName("CreateId");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateTime).HasColumnName("CreateTime");
            Property(t => t.ModifyId).HasColumnName("ModifyId");
            Property(t => t.ModifyBy).HasColumnName("ModifyBy");
            Property(t => t.ModifyTime).HasColumnName("ModifyTime");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            ToTable("Cms_Project_Point");
            //表的引用关系映射
            this.HasRequired(t => t.project).WithMany(t => t.points).HasForeignKey(d => d.ProjectId);
            this.HasRequired(t => t.professionalType).WithMany(t => t.points).HasForeignKey(d => d.ProfessionalTypeId);
        }
    }
}
