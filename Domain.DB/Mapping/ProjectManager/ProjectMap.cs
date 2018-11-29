using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DB.Mapping
{
    partial class ProjectMap
    {
        partial void ProjectMapAppend()
        {
            // Primary Key
            this.Property(t => t.Id).HasColumnName("Id");
            Property(t => t.ProjectName).HasColumnName("ProjectName");
            Property(t => t.ProjectLeader).HasColumnName("ProjectLeader");
            Property(t => t.Content).HasColumnName("Content");
            Property(t => t.Address).HasColumnName("Address");
            Property(t => t.Note).HasColumnName("Note");
            Property(t => t.CreateId).HasColumnName("CreateId");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateTime).HasColumnName("CreateTime");
            Property(t => t.ModifyId).HasColumnName("ModifyId");
            Property(t => t.ModifyBy).HasColumnName("ModifyBy");
            Property(t => t.ModifyTime).HasColumnName("ModifyTime");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            ToTable("Cms_Project");
        }
    }
}
