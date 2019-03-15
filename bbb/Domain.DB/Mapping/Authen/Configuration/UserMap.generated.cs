using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

using Framework.EFData;
using Domain.DB.Models;


namespace Domain.DB.Mapping.Authen
{
    /// <summary>
    /// 数据表映射 —— User
    /// </summary>    
	internal partial class UserMap : EntityTypeConfiguration<User>, IEntityMapper
    {
        /// <summary>
        /// User-数据表映射构造函数
        /// </summary>
        public UserMap()
        {
			UserMapAppend();
        }

		/// <summary>
        /// 额外的数据映射
        /// </summary>
        partial void UserMapAppend();
		
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
