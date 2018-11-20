//**********************************
//*ClassName:
//*Version:
//*Date:
//*Author:
//*Effect:
//**********************************

using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using Framework.EFData;
using Domain.DB.Models.Authen;

namespace Domain.DB.Mapping.Authen
{
    internal partial class CityMap : EntityTypeConfiguration<City>, IEntityMapper
    {
        /// <summary>
        /// Module-数据表映射构造函数
        /// </summary>
        public CityMap()
        {
            CityMapAppend();
        }

        /// <summary>
        /// 额外的数据映射
        /// </summary>
        partial void CityMapAppend();

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
