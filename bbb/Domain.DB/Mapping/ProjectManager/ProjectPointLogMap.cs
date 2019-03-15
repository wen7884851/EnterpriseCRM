using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DB.Mapping
{
    partial class ProjectPointLogMap
    {
        partial void ProjectPointLogMapAppend()
        {
            // Primary Key
            this.Property(t => t.Id).HasColumnName("Id");
            Property(t => t.ProjectPointId).HasColumnName("ProjectPointId");
            Property(t => t.LogType).HasColumnName("LogType");
            Property(t => t.Content).HasColumnName("Content");
            Property(t => t.CreateId).HasColumnName("CreateId");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateTime).HasColumnName("CreateTime");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            ToTable("Cms_Project_PointLog");

            //表的引用关系映射
            this.HasOptional(t => t.point).WithMany(t => t.logs).HasForeignKey(d => d.ProjectPointId);
        }
    }
}
