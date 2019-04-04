using System.Web;
using System.Web.Optimization;

namespace Web.Site.WebUI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterJsBundles(bundles);
            RegisterCssBundles(bundles);
        }

        public static void RegisterJsBundles(BundleCollection bundles)
        {
            var JsBase = new ScriptBundle("~/bundles/scripts/jsBase").Include(
                 "~/Scripts/new/jquery.min.js",
                 "~/Scripts/new/bootstrap.min.js",
                 "~/Scripts/new/perfect-scrollbar.min.js",
                 "~/Scripts/new/main.min.js",
                 "~/Scripts/new/common.js");
            var JsNotify = new ScriptBundle("~/bundles/scripts/jsNotify").Include("~/Scripts/new/bootstrap-notify.min.js",
               "~/Scripts/new/lightyear.js");
            bundles.Add(JsBase);
            bundles.Add(JsNotify);
        }

        public static void RegisterCssBundles(BundleCollection bundles)
        {
            var CssBase = new StyleBundle("~/bundles/css/cssBase").Include(
                   "~/Content/new/bootstrap.min.css",
                   "~/Content/new/materialdesignicons.min.css",
                   "~/Content/new/style.min.css");
            bundles.Add(CssBase);
        }
    }
}
