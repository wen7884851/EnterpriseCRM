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
            var JsBase = new ScriptBundle("~/scripts/jsBase").Include("~/Scripts/lib/bootstrap/bootstrap.min.js",
                "~/Scripts/lib/jquery/jquery-1.10.2.min.js",
                "~/Scripts/lib/ko/knockout-jqAutocomplete.js",
                "~/Scripts/lib/ko/ko_observableArray_fn.js");

            var JsLogin = new ScriptBundle("~/scripts/loginJs").Include("~/Scripts/app/account/login.js",
               "~/Scripts/app/common/cookie.js");
<<<<<<< HEAD

            bundles.Add(JsBase);
            bundles.Add(JsLogin);
=======
            var JsMain = new ScriptBundle("~/scripts/mainJs").Include("~/Content/admin/js/bootstrap.min.js",
                     "~/Content/admin/js/bootbox.min.js", "~/Content/admin/js/jquery.dataTables.min.js",
                      "~/Content/admin/js/jquery.dataTables.AjaxSource.min.js", "~/Content/admin/js/select2.min.js",
                      "~/Scripts/jquery.validate*", "~/Scripts/jquery.unobtrusive*", "~/Content/layer/jquery.layout.js",
                      "~/Content/layer/layer.min.js", "~/Content/admin/js/jquery.min.js", "~/Content/admin/js/matrix.js");
            bundles.Add(JsBase);
            bundles.Add(JsLogin);
            bundles.Add(JsMain);
>>>>>>> featrue.LoginTest
        }

        public static void RegisterCssBundles(BundleCollection bundles)
        {
            var CssBase = new StyleBundle("~/bundles/css/cssBase").Include(
                   "~/Content/admin/css/bootstrap.min.css",
                   "~/Content/admin/css/bootstrap-responsive.min.css",
                   "~/Content/admin/css/normalize.css");
            var CssLogin = new ScriptBundle("~/scripts/loginJs").Include("~/Content/admin/font-awesome/css/font-awesome.css",
               "~/Content/admin/css/font.css",
               "~/Content/admin/css/matrix-login.css");
<<<<<<< HEAD

=======
            bundles.Add(new StyleBundle("~/bundles/css/bootstrap2").Include(
                    "~/Content/admin/css/bootstrap.min.css",
                    "~/Content/admin/css/bootstrap-responsive.min.css",
                    "~/Content/admin/css/normalize.css"));

            bundles.Add(new StyleBundle("~/bundles/css/admin").Include(
                      "~/Content/admin/css/uniform.css",
                      "~/Content/admin/css/select2.css",
                      "~/Content/admin/css/matrix-style.css",
                      "~/Content/admin/css/matrix-media.css",
                      "~/Content/admin/css/font.css",
                      "~/Content/admin/font-awesome/css/font-awesome.css",
                      "~/Content/layer/skin/layer.css"
                      ));
>>>>>>> featrue.LoginTest
            bundles.Add(CssBase);
            bundles.Add(CssLogin);
        }
    }
}
