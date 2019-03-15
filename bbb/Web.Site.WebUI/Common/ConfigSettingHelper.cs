using Domain.Site.Models.Common;
using Web.Site.WebUI.Extension.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Web.Site.WebUI.Common
{
    public class ConfigSettingHelper
    {

        private const string WebNamespace = "Web.Site.WebUI";
        private const string AdminController = "AdminController";

        /// <summary>
        /// 应用程序加载Module LinkUrl
        /// </summary>
        /// <returns></returns>
        public static List<MVCModuleModel> GetAllModuleLinkUrl()
        {
            var model = new List<MVCModuleModel>();
            if (HttpContext.Current.Application["ModuleLinkUrl"] == null)
            {
                model = GetAllModuleByAssembly();
                HttpContext.Current.Application["ModuleLinkUrl"] = model;
            }
            else
            {
                model = (List<MVCModuleModel>)HttpContext.Current.Application["ModuleLinkUrl"];
            }
            return model;
        }

        /// <summary>
        /// 获取Controller下的Action, 组合成LinkUrl提供给Module模块使用
        /// </summary>
        /// <returns></returns>
        private static List<MVCModuleModel> GetAllModuleByAssembly()
        {
            var model = new List<MVCModuleModel>();

            var types = Assembly.Load(WebNamespace).GetTypes();

            foreach (var type in types)
            {
                if (type.BaseType.Name == AdminController)
                {
                    var members = type.GetMethods();
                    foreach (var member in members)
                    {
                        if (member.ReturnType.Name == "ActionResult")
                        {

                        }
                    }
                }
            }
            return model;
        }
    }
}