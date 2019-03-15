using System;
using System.Collections.Generic;
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
            this.Property(t => t.Id).HasColumnName("Id");
            Property(t => t.UserId).HasColumnName("UserId");
            Property(t => t.StoreContent).HasColumnName("StoreContent");
            Property(t => t.StoreFund).HasColumnName("StoreFund");
            Property(t => t.ProjectPointId).HasColumnName("ProjectPointId");
            Property(t => t.ProjectPointProportion).HasColumnName("ProjectPointProportion");
            Property(t => t.CreateId).HasColumnName("CreateId");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateTime).HasColumnName("CreateTime");
            Property(t => t.ModifyId).HasColumnName("ModifyId");
            Property(t => t.ModifyBy).HasColumnName("ModifyBy");
            Property(t => t.ModifyTime).HasColumnName("ModifyTime");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            ToTable("Cms_Project_UserStore");
            //表的引用关系映射
            this.HasOptional(t => t.projectPoint).WithMany(t => t.projectPointUserStores).HasForeignKey(d => d.ProjectPointId);
        }
    }
}
