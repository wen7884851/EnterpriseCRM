//**********************************
//*ClassName:
//*Version:
//*Date:
//*Author:
//*Effect:
//**********************************

using System;
using System.ComponentModel.DataAnnotations.Schema;

using Core.EFData;
using Domain.DB.Models.Authen;

namespace Domain.DB.Mapping.Authen
{

    /// <summary>
    /// 数据表映射 —— SystemSiteMap
    /// </summary>   
    partial class SystemSiteMap
    {
        /// <summary>
        /// 映射配置
        /// </summary>   
        partial void SystemSiteMapAppend()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Gid)
                .HasMaxLength(100);

            this.Property(t => t.SystemName)
                .HasMaxLength(50);

            this.Property(t => t.Url)
                .HasMaxLength(1000);

            this.Property(t => t.Icon)
                .HasMaxLength(100);

            this.Property(t => t.Code)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(100);

            // Properties
            this.Property(t => t.CreateBy)
                .HasMaxLength(50);

            this.Property(t => t.ModifyBy)
                .HasMaxLength(50);

            this.ToTable("Common_Auth_SystemSite");
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.SystemName).HasColumnName("SystemName");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Icon).HasColumnName("Icon");
            this.Property(t => t.Code).HasColumnName("Code");

            this.Property(t => t.CreateId).HasColumnName("CreateId");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.ModifyId).HasColumnName("ModifyId");
            this.Property(t => t.ModifyBy).HasColumnName("ModifyBy");
            this.Property(t => t.ModifyTime).HasColumnName("ModifyTime");

            // Relation
        }
    }
    
}
