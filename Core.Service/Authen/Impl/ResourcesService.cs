using System;
using System.ComponentModel.Composition;
using System.Linq;

using Domain.DB.Models.Authen;
using Framework.Tool;
using Core.Repository.Authen;
using Domain.Site.Models.Authen.Permission;
using System.Collections.Generic;
using Domain.Site.Models.Authen.RoleModulePermission;
using Domain.Site.Models.SSO;
using Domain.DB.Models;
using Domain.DB.Enum;

namespace Core.Service.Authen.Impl
{
	/// <summary>
    /// 服务层实现 —— RoleModulePermissionService
    /// </summary>
    [Export(typeof(IResourcesService))]
    public class ResourcesService : CoreServiceBase, IResourcesService
    {
        #region 属性
        #endregion

        #region 公共方法


        #endregion
    }
}
