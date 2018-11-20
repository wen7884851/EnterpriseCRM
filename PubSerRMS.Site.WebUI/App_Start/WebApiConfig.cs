using Domain.Site.WebUI.Extension.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Domain.Site.WebUI
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Formatters.JsonFormatter.AddQueryStringMapping("$format", "json", "application/json"); 
            //config.Formatters.XmlFormatter.AddQueryStringMapping("$format", "xml", "application/xml");

            config.EnableQuerySupport();
            config.Filters.Add(new WebApiExceptionFilterAttribute());
            //只返回Json格式
            //var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            //config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}