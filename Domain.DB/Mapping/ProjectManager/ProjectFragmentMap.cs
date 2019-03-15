using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DB.Mapping
{
    partial class ProjectFragmentMap
    {
        partial void ProjectFragmentMapAppend()
        {
            // Primary Key
            this.Property(t => t.Id).HasColumnName("Id");
            Property(t => t.FragmentFund).HasColumnName("FragmentFund");
            Property(t => t.FragmentProportion).HasColumnName("FragmentProportion");
            Property(t => t.ProjectTypeId).HasColumnName("ProjectTypeId");
            Property(t => t.CreateId).HasColumnName("CreateId");
            Property(t => t.CreateBy).HasColumnName("CreateBy");
            Property(t => t.CreateTime).HasColumnName("CreateTime");
            Property(t => t.ModifyId).HasColumnName("ModifyId");
            Property(t => t.ModifyBy).HasColumnName("ModifyBy");
            Property(t => t.ModifyTime).HasColumnName("ModifyTime");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            ToTable("Cms_Project_Fragment");
            //表的引用关系映射
            this.HasOptional(t => t.projectType).WithMany(t => t.projectFragments).HasForeignKey(d => d.ProjectTypeId);
        }
    }
}
