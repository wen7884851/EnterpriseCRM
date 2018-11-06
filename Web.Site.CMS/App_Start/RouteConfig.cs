using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Site.CMS
{
    internal class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            Contract.Assert(routes != null);

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            AreaRegistration.RegisterAllAreas();
            RegisterDefaultRoute(routes);
        }
        public static void RegisterDefaultRoute(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
