﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//	   如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//
// <copyright file="IModulePermissionService.cs">
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


namespace Core.Service.Authen
{
	/// <summary>
    /// 服务层接口 —— IModulePermissionService
    /// </summary>
    public interface IModulePermissionService
    {
        #region 属性

        IQueryable<ModulePermission> ModulePermissions { get; }

        #endregion

        #region 公共方法

		OperationResult SetButton(ButtonModel model);

        #endregion
	}
}
