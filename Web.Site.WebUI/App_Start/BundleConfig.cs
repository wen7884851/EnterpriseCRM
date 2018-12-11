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
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery.unobtrusive*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/jquery-ui-zh.js"
                ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));


            //---Admin Site---

            //-----CSS-----
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

            bundles.Add(new StyleBundle("~/bundles/css/jqueryui").Include(
                      "~/Content/themes/base/jquery.ui.core.css",
                      //"~/Content/themes/base/jquery.ui.resizable.css",
                      //"~/Content/themes/base/jquery.ui.selectable.css",
                      //"~/Content/themes/base/jquery.ui.accordion.css",
                      "~/Content/themes/base/jquery.ui.autocomplete.css",
                      //"~/Content/themes/base/jquery.ui.button.css",
                      //"~/Content/themes/base/jquery.ui.dialog.css",
                      //"~/Content/themes/base/jquery.ui.slider.css",
                      //"~/Content/themes/base/jquery.ui.tabs.css",
                      "~/Content/themes/base/jquery.ui.datepicker.css",
                      //"~/Content/themes/base/jquery.ui.progressbar.css",
                      "~/Content/themes/base/jquery.ui.theme.css"
                      ));

            bundles.Add(new StyleBundle("~/bundles/css/tagsinput").Include(
              "~/Content/jquery-tags-input/jquery.tagsinput.css",
              "~/Content/Uploadify/uploadify.v3.2.css"
              ));

            //-----JS-----
            bundles.Add(new ScriptBundle("~/bundles/js/admin-jq").Include(
                     "~/Content/admin/js/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/Content/froalaEditor").Include(
              "~/Content/codemirror/js/codemirror-5.3.0.min.js",
              "~/Content/codemirror/js/codemirror-5.3.0.xml.min.js",
              "~/Content/froalaEditor/js/froala_editor.min.js",
              "~/Content/froalaEditor/js/plugins/align.min.js",
              "~/Content/froalaEditor/js/plugins/code_beautifier.min.js",
              "~/Content/froalaEditor/js/plugins/code_view.min.js",
              "~/Content/froalaEditor/js/plugins/colors.min.js",
              "~/Content/froalaEditor/js/plugins/draggable.min.js",
              "~/Content/froalaEditor/js/plugins/emoticons.min.js",
              "~/Content/froalaEditor/js/plugins/font_size.min.js",
              "~/Content/froalaEditor/js/plugins/font_family.min.js",
              "~/Content/froalaEditor/js/plugins/image.min.js",
              "~/Content/froalaEditor/js/plugins/file.min.js",
              "~/Content/froalaEditor/js/plugins/image_manager.min.js",
              "~/Content/froalaEditor/js/plugins/line_breaker.min.js",
              "~/Content/froalaEditor/js/plugins/link.min.js",
              "~/Content/froalaEditor/js/plugins/lists.min.js",
              "~/Content/froalaEditor/js/plugins/paragraph_format.min.js",
              "~/Content/froalaEditor/js/plugins/paragraph_style.min.js",
              "~/Content/froalaEditor/js/plugins/video.min.js",
              "~/Content/froalaEditor/js/plugins/table.min.js",
              "~/Content/froalaEditor/js/plugins/url.min.js",
              "~/Content/froalaEditor/js/plugins/entities.min.js",
              "~/Content/froalaEditor/js/plugins/inline_style.min.js",
              "~/Content/froalaEditor/js/plugins/save.min.js",
              "~/Content/froalaEditor/js/plugins/fullscreen.min.js",
              "~/Content/froalaEditor/js/plugins/quote.min.js",
              "~/Content/froalaEditor/js/plugins/quick_insert.min.js",
              "~/Content/froalaEditor/js/languages/zh_cn.js"
              ));

            bundles.Add(new ScriptBundle("~/Content/layout").Include(
           "~/Content/layer/jquery.layout.js",
           "~/Content/layer/layer.min.js"
           ));

            bundles.Add(new ScriptBundle("~/bundles/js/uploadify").Include(
              "~/Scripts/flashupload.js",
             "~/Content/Uploadify/jquery.uploadify.v3.2.min.js",
             "~/Content/jquery-tags-input/jquery.tagsinput.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/bootstrap2").Include(
                     "~/Content/admin/js/bootstrap.min.js",
                     "~/Content/admin/js/bootbox.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/jqdataTables").Include(
                      "~/Content/admin/js/jquery.dataTables.min.js",
                      "~/Content/admin/js/jquery.dataTables.AjaxSource.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/admin-plugin").Include(
                      "~/Content/admin/js/select2.min.js"));
        }

        public static void RegisterJsBundles(BundleCollection bundles)
        {
            var JsBase = new ScriptBundle("~/scripts/jsBase").Include(
                 "~/Content/admin/js/bootstrap.min.js",
                 "~/Content/admin/js/jquery.min.js",
                "~/Scripts/lib/bootstrap/bootstrap.min.js",
                "~/Scripts/jquery.cookie.js");

            var JsLogin = new ScriptBundle("~/scripts/loginJs").Include("~/Scripts/app/account/login.js",
               "~/Scripts/app/common/cookie.js");
            var JsMain = new ScriptBundle("~/scripts/mainJs").Include("~/Content/admin/js/bootstrap.min.js",
                     "~/Content/admin/js/bootbox.min.js", "~/Content/admin/js/jquery.dataTables.min.js",
                      "~/Content/admin/js/jquery.dataTables.AjaxSource.min.js", "~/Content/admin/js/select2.min.js",
                      "~/Scripts/jquery.validate*", "~/Scripts/jquery.unobtrusive*", "~/Content/layer/jquery.layout.js",
                      "~/Content/layer/layer.min.js", "~/Content/admin/js/jquery.min.js", "~/Content/admin/js/matrix.js");
            bundles.Add(JsBase);
            bundles.Add(JsLogin);
            bundles.Add(JsMain);
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
            bundles.Add(CssBase);
            bundles.Add(CssLogin);
        }
    }
}
