﻿using System.Linq;
using Framework.Tool;
using Domain.DB.Models.Authen;
using System.Collections.Generic;
using Domain.Site.Common.Models;

namespace Core.Service.Authen
{
	/// <summary>
    /// 服务层接口 —— IRoleService
    /// </summary>
    public interface IRoleService
    {
		#region 属性

        IQueryable<Role> Roles { get; }



        #endregion

        #region 公共方法
        /// <summary>
        /// 复选框数据源
        /// </summary>
        /// <returns></returns>
        List<KeyValueModel> GetKeyValueList();

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        OperationResult Delete(int Id);

        #endregion
	}
}