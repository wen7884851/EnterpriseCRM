using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DB.Mapping
{
    partial class ProjectPointUserStoreMap
    {
        partial void ProjectPointUserStoreMapAppend()
        {
            // Primary Key
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.UserId).HasColumnName("UserId");
            Property(t => t.StoreContent).HasColumnName("StoreContent");
            Property(t => t.UserFund).HasColumnName("UserFund");
            Property(t => t.ProjectPointId).HasColumnName("ProjectPointId");
            Property(t => t.UserProportion).HasColumnName("UserProportion");
            Property(t => t.CreateId).HasColumnName("CreateId");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateTime).HasColumnName("CreateTime");
            Property(t => t.ModifyId).HasColumnName("ModifyId");
            Property(t => t.ModifyBy).HasColumnName("ModifyBy");
            Property(t => t.ModifyTime).HasColumnName("ModifyTime");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            ToTable("Cms_Project_UserStore");
            //表的引用关系映射
            this.HasRequired(t => t.projectPoint).WithMany(t => t.projectPointUserStores).HasForeignKey(d => d.ProjectPointId);
        }
    }
}
