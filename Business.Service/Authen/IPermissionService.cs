﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//	   如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//
// <copyright file="IPermissionService.cs">
//		Copyright(c)2013 QuickCore.All rights reserved.
//		开发组织：QuickCore
//		公司网站：QuickCore
//		所属工程：Core.Service
//		生成时间：2013-12-11 23:55
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Linq;

using Framework.Tool;
using Core.Repository.Authen;
using Domain.DB.Models.Authen;
using Domain.Site.Models.Authen.Permission;
using Domain.Site.Common.Models;
using System.Collections.Generic;


namespace Core.Service.Authen
{
	/// <summary>
    /// 服务层接口 —— IPermissionService
    /// </summary>
    public interface IPermissionService
    {
        #region 属性

        IQueryable<Permission> Permissions { get; }

        #endregion

        #region 公共方法
		/// <summary>
		/// 复选框数据源
		/// </summary>
		/// <returns></returns>
		List<KeyValueModel> GetKeyValueList();

        OperationResult Insert(PermissionModel model);

        OperationResult Update(PermissionModel model);

        OperationResult Delete(int Id);

        #endregion
	}
}
