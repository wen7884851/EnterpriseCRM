//**********************************
//*ClassName:
//*Version:
//*Date:
//*Author:
//*Effect:
//**********************************

using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using Domain.DB.Models.Authen;
using Core.EFData;

namespace Domain.DB.Mapping.Authen
{
    internal partial class AreaMap : EntityTypeConfiguration<Area>, IEntityMapper
    {
        /// <summary>
        /// Module-数据表映射构造函数
        /// </summary>
        public AreaMap()
        {
            AreaMapAppend();
        }

        /// <summary>
        /// 额外的数据映射
        /// </summary>
        partial void AreaMapAppend();

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
