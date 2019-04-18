using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DB.Mapping.Authen
{
    partial class RoleModuleMap
    {
        /// <summary>
        /// 映射配置
        /// </summary>   		
        partial void RoleModuleMapAppend()
        {
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Common_Auth_RoleModule");
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.ModuleId).HasColumnName("ModuleId");
            this.Property(t => t.IsSearch).HasColumnName("IsSearch");
            this.Property(t => t.IsCreate).HasColumnName("IsCreate");
            this.Property(t => t.IsEdit).HasColumnName("IsEdit");
            this.Property(t => t.CreateId).HasColumnName("CreateId");
            this.Property(t => t.CreateBy).HasColumnName("CreateBy");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.ModifyId).HasColumnName("ModifyId");
            this.Property(t => t.ModifyBy).HasColumnName("ModifyBy");
            this.Property(t => t.ModifyTime).HasColumnName("ModifyTime");

            this.HasRequired(t => t.Role).WithMany(t => t.RoleModules).HasForeignKey(d => d.RoleId);
            this.HasRequired(t => t.Module).WithMany(t => t.RoleModules).HasForeignKey(d => d.ModuleId);
        }
    }
}
