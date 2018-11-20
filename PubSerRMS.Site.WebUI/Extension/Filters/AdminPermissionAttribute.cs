using Framework.Common;

using Framework.Common.ToolsHelper;
using Framework.Tool.Operator;
using Domain.Site.Common.Models;
using Core.Service.Authen;
using Domain.Site;
using Domain.Site.Models.AdminCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Domain.Site.WebUI.Extension.Filters
{
	/// <summary>
	/// 后台权限验证
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple= false)]
    public class AdminPermissionAttribute : System.Web.Mvc.AuthorizeAttribute
    {
		private PermissionCustomMode CustomMode;

        private IUserService UserService { get; set; }
		private IRoleService RoleService { get; set; }
		private IUserRoleService UserRoleService { get; set; }
		private IResourcesService ResourcesService { get; set; }
		private IModuleService ModuleService { get; set; }
		private IModulePermissionService ModulePermissionService { get; set; }
		private IPermissionService PermissionService { get; set; }

		public AdminPermissionAttribute(PermissionCustomMode mode)
		{
			var container = new HttpContextHelp().HttpContextGet("Container");//HttpContext.Current.Application["Container"] as CompositionContainer;
            UserService = container.GetExportedValueOrDefault<IUserService>();
			RoleService = container.GetExportedValueOrDefault<IRoleService>();
			UserRoleService = container.GetExportedValueOrDefault<IUserRoleService>();
            ResourcesService = container.GetExportedValueOrDefault<IResourcesService>();
			ModuleService = container.GetExportedValueOrDefault<IModuleService>();
			ModulePermissionService = container.GetExportedValueOrDefault<IModulePermissionService>();
			PermissionService = container.GetExportedValueOrDefault<IPermissionService>();

			CustomMode = mode;
		}

		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			//权限拦截是否忽略
			if (CustomMode == PermissionCustomMode.Ignore)
			{
				return;
			}

            //登录是否过期
            if (OperatorProvider.Provider.IsOverdue())
            {
                CookieHelper.SetCookie("pubser_login_error", "Overdue");//登录已超时,请重新登录
                filterContext.Result = new RedirectResult("~/Admin/Login");
                return;
            }
            //是否已登录
            var OnLine = OperatorProvider.Provider.IsOnLine();


            var user = OperatorProvider.Provider.GetCurrent();
			if (user == null)
			{
				//跳转到登录页面
				filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Common", controller = "Login", action = "Index"}));
			}
			else
			{
				// 权限拦截与验证
				var area = filterContext.RouteData.DataTokens.ContainsKey("area") ? filterContext.RouteData.DataTokens["area"].ToString() : string.Empty;
				var controller = filterContext.RouteData.Values["controller"].ToString().ToLower();
				var action = filterContext.RouteData.Values["action"].ToString().ToLower();

				var isAllowed = this.IsAllowed(user, controller, action);

				if (!isAllowed)
				{
					filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Common", controller = "Error", action = "Page400" }));
				}
			}
		}

        public bool IsAllowed(OperatorModel user, string controller, string action)
        {
            //var roleIdList = UserRoleService.UserRoles.Where(t => t.UserId == user.UserId && t.IsDeleted == false).Select(t => t.RoleId);
            //var module = ModuleService.Modules.FirstOrDefault(t => t.Controller.ToLower() == controller);

   //         var roleIdList = new UserRoleCache().GetUserRoleList(user.UserId).Select(t => t.RoleId);
   //         var module = new ModuleCache().GetModuleEntity(controller, "E2CE0E1F-9224-4561-B362-9B3DDC1F5301");
        
   //         var permission = PermissionService.Permissions.FirstOrDefault(t => t.Code.ToLower() == action);

			//if (module != null && permission != null)
			//{
			//	var roleModulePermisssion = ResourcesService.Resourcess.Where(t => roleIdList.Contains(t.ResourcesId)
			//									&& t.ModuleId == module.Id
			//									&& t.PermissionId == permission.Id
			//									&& t.IsDeleted == false);
			//	if (roleModulePermisssion.Count() > 0)
			//	{
			//		return true;
			//	}
			//}

			return false;
        }

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			//if (filterContext.HttpContext.User.Identity.IsAuthenticated)
			//{
			//	base.HandleUnauthorizedRequest();
			//}
			//else
			//{
			//	filterContext.Result = new RedirectToRouteResult(new
			//	RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
			//}
		}
    
		
	}
}