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
using Framework.Tool.Operator;

namespace Domain.Site.WebUI.Extension.Filters
{
	/// <summary>
	/// 页面布局
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AdminLayoutAttribute : ActionFilterAttribute 
    {
		//public IUserService UserService { get; set; }
		//public IRoleService RoleService { get; set; }
  //      public IUserRoleService UserRoleService { get; set; }
  //      public IModuleService ModuleService { get; set; }
		//public IPermissionService PermissionService { get; set; }
		//public IModulePermissionService ModulePermissionService { get; set; }
		//public IResourcesService ResourcesService { get; set; }
  //      public ISystemSiteService SystemSiteService { get; set; }

        static int SystemID { get; set; }

        public AdminLayoutAttribute()
		{
			//TODO: Test
            //var userRole = new List<UserRole> { new UserRole { Id = 1, UserId = 1, RoleId = 1 } };
            //var user = new User { Id = 1, LoginName = "admin", LoginPwd = "8wdJLK8mokI=", UserRole = userRole };
            //SessionHelper.SetSession("CurrentUser", user);
			//var user = OperatorProvider.Provider.GetCurrent();
			//if (user != null)
			//{
			//	var container = new HttpContextHelp().HttpContextGet("Container");
			//	UserService = container.GetExportedValueOrDefault<IUserService>();
			//	RoleService = container.GetExportedValueOrDefault<IRoleService>();
   //             UserRoleService = container.GetExportedValueOrDefault<IUserRoleService>();
   //             ResourcesService = container.GetExportedValueOrDefault<IResourcesService>();
			//	ModuleService = container.GetExportedValueOrDefault<IModuleService>();
			//	ModulePermissionService = container.GetExportedValueOrDefault<IModulePermissionService>();
			//	PermissionService = container.GetExportedValueOrDefault<IPermissionService>();
   //             SystemSiteService = container.GetExportedValueOrDefault<ISystemSiteService>();


   //             SystemID = SystemSiteService.SystemSites.Where(t => t.Gid == "E2CE0E1F-9224-4561-B362-9B3DDC1F5301").FirstOrDefault().Id;
   //         }
		}

		public override void OnResultExecuting(ResultExecutingContext filterContext)
		{
   //         //var user = OperatorProvider.Provider.GetCurrent(); 
   //         var user = new ;

   //         if (user != null)
			//{
			//	//顶部菜单
			//	((ViewResult)filterContext.Result).ViewBag.LoginName = user.LoginName;

			//	//左侧菜单
			//	((ViewResult)filterContext.Result).ViewBag.SidebarMenuModel = InitSidebarMenu(user);

			//	//面包屑
			//	((ViewResult)filterContext.Result).ViewBag.BreadCrumbModel = InitBreadCrumb(filterContext);
			
			//	//按钮
			//	InitButton(user, filterContext);
			//}
		}

		//private List<SidebarMenuModel> InitSidebarMenu(OperatorModel user)
		//{
            //var entity = user.UserRole.Select(t => t.RoleId);
            //var entity = UserRoleService.UserRoles.Where(t => t.UserId == user.UserId && t.IsDeleted == false).Select(t => t.RoleId);
   //         var entity = new UserRoleCache().GetUserRoleList(user.UserId).Select(t=>t.RoleId);


   //         List<int> RoleIds = entity.ToList();

			//var model = new List<SidebarMenuModel>();

			////取出所有选中的节点
			//var parentModuleIdList = ResourcesService.Resourcess.Where(t => RoleIds.Contains(t.ResourcesId) && t.PermissionId == null&&t.SystemId==SystemID && t.IsDeleted == false).Select(t => t.ModuleId).Distinct();
			//var childModuleIdList = ResourcesService.Resourcess.Where(t => RoleIds.Contains(t.ResourcesId) && t.PermissionId != null && t.SystemId == SystemID && t.IsDeleted == false).Select(t => t.ModuleId).Distinct();

			//foreach (var pmId in parentModuleIdList)
			//{
   //             //取出父菜单
   //             //var parentModule = ModuleService.Modules.FirstOrDefault(t => t.Id == pmId);
   //             var parentModule = new ModuleCache().GetModuleEntity(pmId);

   //             if (parentModule != null)
			//	{
			//		var sideBarMenu = new SidebarMenuModel
			//		{
			//			Id = parentModule.Id,
			//			ParentId = parentModule.ParentId,
			//			Name = parentModule.Name,
			//			Code = parentModule.Code,
   //                     OrderSort=parentModule.OrderSort,
			//			Icon = parentModule.Icon,
			//			LinkUrl = parentModule.LinkUrl,
			//		};

			//		//取出子菜单
			//		foreach (var cmId in childModuleIdList)
			//		{
   //                     //var childModule = ModuleService.Modules.FirstOrDefault(t => t.Id == cmId);
   //                     var childModule = new ModuleCache().GetModuleEntity(cmId);
   //                     if (childModule != null && childModule.ParentId == sideBarMenu.Id)
			//			{
			//				var childSideBarMenu = new SidebarMenuModel
			//				{
			//					Id = childModule.Id,
			//					ParentId = childModule.ParentId,
			//					Name = childModule.Name,
			//					Code = childModule.Code,
   //                             OrderSort = childModule.OrderSort,
   //                             Icon = childModule.Icon,
			//					Area = childModule.Area,
			//					Controller = childModule.Controller,
			//					Action = childModule.Action
			//				};
			//				sideBarMenu.ChildMenuList.Add(childSideBarMenu);
			//			}
			//		}

			//		//子菜单排序
			//		sideBarMenu.ChildMenuList = sideBarMenu.ChildMenuList.OrderBy(t => t.OrderSort).ToList();
			//		model.Add(sideBarMenu);
			//	}
			//	//父菜单排序
			//	model = model.OrderBy(t => t.Code).ToList();
			//}

			//return model;
		//}

		//private BreadCrumbNavModel InitBreadCrumb(ResultExecutingContext filterContext)
		//{
		//	//var area = filterContext.RouteData.DataTokens.ContainsKey("area") ? filterContext.RouteData.DataTokens["area"].ToString().ToLower() : string.Empty;
		//	//var controller = filterContext.RouteData.Values["controller"].ToString().ToLower();
		//	//var action = filterContext.RouteData.Values["action"].ToString().ToLower();

		//	//string linkUrl = string.Format("{0}/{1}/{2}", area, controller, action);

		//	//var model = new BreadCrumbNavModel();

		//	//var indexModel = new BreadCrumbModel { 
		//	//	Name = "首页",
		//	//	Icon = "icon-home",
		//	//	IsParent = false,
		//	//	IsIndex = true
		//	//};

		//	//if (area == "common" && controller == "home" && action == "index")
		//	//{
		//	//	model.CurrentName = "首页";
		//	//}

		//	//model.BreadCrumbList.Add(indexModel);

  // //         //var module = ModuleService.Modules.FirstOrDefault(t => t.LinkUrl.ToLower().Contains(linkUrl) && t.SystemID == SystemID && t.IsDeleted == false && t.Enabled == true);
  // //         var module = new ModuleCache().GetModuleEntity(linkUrl, SystemID);
  // //         if (module != null)
		//	//{			
		//	//	//有父菜单
		//	//	if (module.ParentModule != null)
		//	//	{
		//	//		var parentModel = new BreadCrumbModel
		//	//		{
		//	//			IsParent = true,
		//	//			Name = module.ParentModule.Name,
		//	//			Icon = module.ParentModule.Icon
		//	//		};
		//	//		model.BreadCrumbList.Add(parentModel);
		//	//	}
				
		//	//	var currentModel = new BreadCrumbModel {
 	//	//			IsParent = false,
		//	//		Name = module.Name,
		//	//		Icon = ""
		//	//	};

		//	//	model.CurrentName = currentModel.Name;
		//	//	model.BreadCrumbList.Add(currentModel);

		//	//	((ViewResult)filterContext.Result).ViewBag.CurrentTitle = module.Name;
		//	//}
		//	//return model;
		//}

		private void InitButton(OperatorModel user, ResultExecutingContext filterContext)
		{
            //var roleIds = UserRoleService.UserRoles.Where(t => t.UserId == user.UserId).Select(t => t.RoleId);
            //var roleIds = user.UserRole.Select(t => t.RoleId);

   //         var roleIds = new UserRoleCache().GetUserRoleList(user.UserId).Select(t => t.RoleId);

   //         var controller = filterContext.RouteData.Values["controller"].ToString().ToLower();
			//var action = filterContext.RouteData.Values["action"].ToString().ToLower();
   //         //var module = ModuleService.Modules.FirstOrDefault(t => t.Controller.ToLower() == controller);

   //         var module = new ModuleCache().GetModuleEntity(controller, "E2CE0E1F-9224-4561-B362-9B3DDC1F5301");
   //         if (module != null)
			//{
			//	var permissionIds = ResourcesService.Resourcess.Where(t => roleIds.Contains(t.ResourcesId) && t.SystemId == SystemID && t.ModuleId == module.Id).Select(t => t.PermissionId).Distinct();
			//	foreach (var permissionId in permissionIds)
			//	{
			//		var entity = PermissionService.Permissions.FirstOrDefault(t => t.Id == permissionId && t.Enabled == true && t.IsDeleted == false);
			//		if (entity != null)
			//		{
			//			var btnButton = new ButtonModel
			//			{
			//				Icon = entity.Icon,
			//				Text = entity.Name
			//			};
			//			if (entity.Code.ToLower() == "create")
			//			{
			//				((ViewResult)filterContext.Result).ViewBag.Create = btnButton;
			//			}
			//			else if (entity.Code.ToLower() == "edit")
			//			{
			//				((ViewResult)filterContext.Result).ViewBag.Edit = btnButton;
			//			}
			//			else if (entity.Code.ToLower() == "delete")
			//			{
			//				((ViewResult)filterContext.Result).ViewBag.Delete = btnButton;
			//			}
			//			else if (entity.Code.ToLower() == "setbutton")
			//			{
			//				((ViewResult)filterContext.Result).ViewBag.SetButton = btnButton;
			//			}
			//			else if (entity.Code.ToLower() == "setpermission")
			//			{
			//				((ViewResult)filterContext.Result).ViewBag.SetPermission = btnButton;
			//			}
			//			else if (entity.Code.ToLower() == "changepwd")
			//			{
			//				((ViewResult)filterContext.Result).ViewBag.ChangePwd = btnButton;
			//			}
			//			else if (entity.Code.ToLower() == "deleteall")
			//			{
			//				((ViewResult)filterContext.Result).ViewBag.DeleteAll = btnButton;
			//			}
			//		}
			//	}
			//}
		}
	}
}