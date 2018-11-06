using System;
using System.Linq;

using Core.Tool;
using Domain.DB.Models.System;
using Domain.Site.Models.AdminCommon;

namespace Business.Service.System
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
