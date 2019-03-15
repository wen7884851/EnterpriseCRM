using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.Site.WebUI.Extension.ViewEngine;
using Web.Site.WebUI.Extension.ModelBinder;
using System.Web.Http;
using System;
using Framework.Tool.Operator;

namespace Web.Site.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private const string errorCode404 = "page404";
        private const string errorCode500 = "page500";
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = false;
            //GlobalConfiguration.Configuration.Filters.Add(new WebApiExceptionFilterAttribute());
            //自定义View
            ViewEngines.Engines.Clear();
            ExtendedRazorViewEngine engine = new ExtendedRazorViewEngine();
            engine.AddPartialViewLocationFormat("~/Areas/Common/Views/Shared/{0}.cshtml");
            engine.AddPartialViewLocationFormat("~/Areas/Common/Views/Shared/{0}.vbhtml");
            ViewEngines.Engines.Add(engine);

            //Model去除前后空格
            ModelBinders.Binders.DefaultBinder = new TrimModelBinder();

            //设置MEF依赖注入容器
            MefConfig.RegisterMef();

           //初始化EF6的性能监控
             // MiniProfilerEF6.Initialize();

            //初始化DB
            //DatabaseInitializer.Initialize();
        }

        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)//这里是允许本地访问启动监控,可不写
            {
                StackExchange.Profiling.MiniProfiler.Start();
            }
        }
        protected void Application_EndRequest()
        {
            StackExchange.Profiling.MiniProfiler.Stop();
        }
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

            //  处理日志权限问题
            if (Request.Url.ToString().ToUpper().Contains("PUBSERLOG.AXD"))
            {
                // 获取当前访问用户
                var user = OperatorProvider.Provider.GetCurrent();

                // 业务逻辑，如果用户不为超级管理员则无法访问该请求数据
                if (user == null || !user.IsSystem)
                {
                    Response.Write("无权限访问");
                    Response.End();
                }
            }
        }
        //could not be found
        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            if (ex.Message.Contains("could not be found"))
            {
                Server.ClearError();//在Global.asax中调用Server.ClearError方法相当于是告诉Asp.Net系统抛出的异常已经被处理过了，
                                    //不需要系统跳转到Asp.Net的错误黄页了。如果想在Global.asax外调用ClearError方法可以使用
                                    //HttpContext.Current.ApplicationInstance.Server.ClearError()。
                Response.TrySkipIisCustomErrors = true;
                RedirectOnError(this, ex);
               // Response.Redirect("~/Common/Login", true);//调用Server.ClearError方法后再调用Response.Redirect就可以成功跳转到自定义错误页面了
            }
            else if (ex.Message == "用户过期需要重新登录")
            {
                Server.ClearError();
                Response.TrySkipIisCustomErrors = true;
                Response.Clear();
                string loginurl = "/Common/Account";
                string url = $"/error/{errorCode404}?refer={loginurl}";
                Response.Redirect(url);
            }
        }

        private static void RedirectOnError(HttpApplication application, Exception ex)
        {
            var httpException = ex as HttpException;

            string errorCode = errorCode500;
            if (httpException != null)
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        errorCode = errorCode404;
                        break;
                    default:
                        break;
                }
            }

            string url = string.Format("/error/{0}?refer={1}", errorCode, application.Request.Url.PathAndQuery);
            application.Response.Clear();
            application.Response.Redirect(url);
        }
    }
}
