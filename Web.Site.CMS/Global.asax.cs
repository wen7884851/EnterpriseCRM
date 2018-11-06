using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.Site.CMS.Extension.ModelBinder;

namespace Web.Site.CMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = false;
            ViewEngines.Engines.Clear();

            ModelBinders.Binders.DefaultBinder = new TrimModelBinder();

            //设置MEF依赖注入容器
            MefConfig.RegisterMef();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception objErr = Server.GetLastError().GetBaseException();
            if (objErr.Message.Contains("could not be found"))
            {
                Server.ClearError();//在Global.asax中调用Server.ClearError方法相当于是告诉Asp.Net系统抛出的异常已经被处理过了，
                                    //不需要系统跳转到Asp.Net的错误黄页了。如果想在Global.asax外调用ClearError方法可以使用
                                    //HttpContext.Current.ApplicationInstance.Server.ClearError()。
                Response.Redirect("~/Common/Login", true);//调用Server.ClearError方法后再调用Response.Redirect就可以成功跳转到自定义错误页面了
            }

        }
    }
}
