using Domain.DB.Models;
using Framework.EFData;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DB.Mapping.Authen
{
    /// <summary>
    /// 数据表映射 —— RoleModule
    /// </summary>    
	internal partial class RoleModuleMap : EntityTypeConfiguration<RoleModule>, IEntityMapper
    {
        /// <summary>
        /// RoleModule-数据表映射构造函数
        /// </summary>
        public RoleModuleMap()
        {
            RoleModuleMapAppend();
        }

        /// <summary>
        /// 额外的数据映射
        /// </summary>
        partial void RoleModuleMapAppend();

        /// <summary>
        /// 将当前实体映射对象注册到当前数据访问上下文实体映射配置注册器中
        /// </summary>
        /// <param name="configurations">实体映射配置注册器</param>
        public void RegistTo(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
        }
    }
}
