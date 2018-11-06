//**********************************
//*ClassName:
//*Version:
//*Date:
//*Author:
//*Effect:
//**********************************

using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

using Core.EFData;
using Domain.DB.Models.System;

namespace Domain.DB.Mapping.System
{
    /// <summary>
    /// 数据表映射 —— SystemSite
    /// </summary>    
    internal partial class SystemSiteMap : EntityTypeConfiguration<SystemSite>, IEntityMapper
    {
        /// <summary>
        /// Module-数据表映射构造函数
        /// </summary>
        public SystemSiteMap()
        {
            SystemSiteMapAppend();
        }

        /// <summary>
        /// 额外的数据映射
        /// </summary>
        partial void SystemSiteMapAppend();

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
