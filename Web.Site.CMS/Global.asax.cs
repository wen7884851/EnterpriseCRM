using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Site.CMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private const string errorCode404 = "404";
        private const string errorCode500 = "500";
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
