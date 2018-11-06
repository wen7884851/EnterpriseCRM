using System;
using System.ComponentModel.DataAnnotations.Schema;

using Core.EFData;
using Domain.DB.Models.System;


namespace Domain.DB.Mapping.System
{
    /// <summary>
    /// 数据表映射 —— ResourcesMap
    /// </summary>   
	partial class ResourcesMap
    {
        /// <summary>
        /// 映射配置
        /// </summary>   		
		partial void ResourcesMapAppend()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CreateBy)
                .HasMaxLength(50);

            this.Property(t => t.ModifyBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Common_Auth_Resources");
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.ResourcesType).HasColumnName("ResourcesType");
            this.Property(t => t.ResourcesId).HasColumnName("ResourcesId");
            this.Property(t => t.SystemId).HasColumnName("SystemId");
            this.Property(t => t.ModuleId).HasColumnName("ModuleId");
			this.Property(t => t.PermissionId).HasColumnName("PermissionId");

            this.Property(t => t.CreateId).HasColumnName("CreateId");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.ModifyId).HasColumnName("ModifyId");
            this.Property(t => t.ModifyBy).HasColumnName("ModifyBy");
            this.Property(t => t.ModifyTime).HasColumnName("ModifyTime");

            // Relation
			//this.HasRequired(t => t.Module).WithMany(d => d.Resources).HasForeignKey(f => f.ModuleId).WillCascadeOnDelete(false);
			//this.HasOptional(t => t.Permission).WithMany(d => d.Resources).HasForeignKey(f => f.PermissionId).WillCascadeOnDelete(false);
		}
    }
}
