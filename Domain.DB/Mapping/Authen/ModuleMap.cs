using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DB.Mapping.Authen
{
    partial class ModuleMap
    {
        /// <summary>
        /// 映射配置
        /// </summary>   		
        partial void ModuleMapAppend()
        {
            this.HasKey(t => t.Id);
            this.Property(t => t.Name)
                .HasMaxLength(50);
            this.Property(t => t.LinkUrl)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Common_Auth_Module");
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.LinkUrl).HasColumnName("LinkUrl");
            this.Property(t => t.Layer).HasColumnName("Layer");
            this.Property(t => t.Area).HasColumnName("Area");
            this.Property(t => t.Controller).HasColumnName("Controller");
            this.Property(t => t.Action).HasColumnName("Action");
            this.Property(t => t.Icon).HasColumnName("Icon");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.OrderSort).HasColumnName("OrderSort");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsMenu).HasColumnName("IsMenu");
            this.Property(t => t.Enabled).HasColumnName("Enabled");
            this.Property(t => t.CreateId).HasColumnName("CreateId");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.ModifyId).HasColumnName("ModifyId");
            this.Property(t => t.ModifyBy).HasColumnName("ModifyBy");
            this.Property(t => t.ModifyTime).HasColumnName("ModifyTime");
        }
    }
}
