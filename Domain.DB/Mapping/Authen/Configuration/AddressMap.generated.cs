﻿using Framework.EFData;
using Domain.DB.Models.Authen;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DB.Mapping.Authen
{
    internal partial class AddressMap : EntityTypeConfiguration<Address>, IEntityMapper
    {
        /// <summary>
        /// Module-数据表映射构造函数
        /// </summary>
        public AddressMap()
        {
            AddressMapAppend();
        }

        /// <summary>
        /// 额外的数据映射
        /// </summary>
        partial void AddressMapAppend();

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
