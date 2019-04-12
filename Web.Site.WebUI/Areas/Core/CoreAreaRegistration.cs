using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Site.WebUI.Areas.Core
{
    public class CoreAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Core";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Core_default",
                "Core/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Web.Site.WebUI.Areas.Core.Controllers" }
            );
        }
    }
}