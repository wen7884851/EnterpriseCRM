using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DB.Mapping
{
    partial class ProjectUserStoreMap
    {
        partial void ProjectUserStoreMapAppend()
        {
            // Primary Key
            this.Property(t => t.Id).HasColumnName("Id");
            Property(t => t.ProjectPointId).HasColumnName("ProjectPointId");
            Property(t => t.UserId).HasColumnName("UserId");
            Property(t => t.StoreContent).HasColumnName("StoreContent");
            Property(t => t.UserTax).HasColumnName("UserTax");
            Property(t => t.CreateId).HasColumnName("CreateId");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateTime).HasColumnName("CreateTime");
            Property(t => t.ModifyId).HasColumnName("ModifyId");
            Property(t => t.ModifyBy).HasColumnName("ModifyBy");
            Property(t => t.ModifyTime).HasColumnName("ModifyTime");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            ToTable("Project_UserStore");
        }
    }
}
