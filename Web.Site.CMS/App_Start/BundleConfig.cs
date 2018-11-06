using System.Web.Optimization;
using System.Web.Routing;

namespace Web.Site.CMS
{
    internal class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterJsBundles(bundles);
            RegisterCssBundles(bundles);
        }

        public static void RegisterJsBundles(BundleCollection bundles)
        {
            var JsBase=new ScriptBundle("~/scripts/jsBase").Include("~/Scripts/lib/bootstrap/bootstrap.min.js",
                "~/Scripts/lib/jquery/jquery-1.10.2.min.js",
                "~/Scripts/lib/ko/knockout-jqAutocomplete.js",
                "~/Scripts/lib/ko/ko_observableArray_fn.js");

            var JsLogin = new ScriptBundle("~/scripts/loginJs").Include("~/Scripts/app/account/login.js",
               "~/Scripts/app/common/cookie.js");

            bundles.Add(JsBase);
            bundles.Add(JsLogin);
        }

        public static void RegisterCssBundles(BundleCollection bundles)
        {
            var CssBase=new StyleBundle("~/bundles/css/cssBase").Include(
                   "~/Content/admin/css/bootstrap.min.css",
                   "~/Content/admin/css/bootstrap-responsive.min.css",
                   "~/Content/admin/css/normalize.css");
            var CssLogin = new ScriptBundle("~/scripts/loginJs").Include("~/Content/admin/font-awesome/css/font-awesome.css",
               "~/Content/admin/css/font.css",
               "~/Content/admin/css/matrix-login.css");

            bundles.Add(CssBase);
            bundles.Add(CssLogin);
        }
    }

}