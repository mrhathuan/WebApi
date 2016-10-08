using System.Web;
using System.Web.Optimization;

namespace Shop_Nhi
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/Scripts/js").Include(                       
                       "~/Scripts/bootstrap.min.js",
                      "~/Scripts/jquery.smartmenus.js",
                      "~/Scripts/jquery.smartmenus.bootstrap.js",
                      "~/Scripts/sequence.js",
                      "~/Scripts/sequence-theme.modern-slide-in.js",
                      "~/Scripts/nouislider.js",
                      "~/Scripts/slick.js",
                      "~/Scripts/custom.js",
                      "~/Scripts/jquery-ui.js",
                       "~/Scripts/toastr.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/style.css",
                      "~/Content/jquery.smartmenus.bootstrap.css",
                      "~/Content/font-awesome.css",
                       "~/Content/slick.css",
                        "~/Content/nouislider.css",
                        "~/Content/sequence-theme.modern-slide-in.css",
                        "~/Content/eagle.gallery.css",                        
                        "~/Content/jquery-ui.css",
                        "~/Content/toastr.css"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/JsAdmin").Include(
                     "~/Scripts/bootstrap.min.js",
                     "~/Scripts/kendo/2016.2.607/kendo.all.min.js",
                     "~/Scripts/kendo/2016.2.607/kendo.aspnetmvc.min.js",
                     "~/Scripts/kendo/2016.2.607/jszip.min.js",
                     "~/Plugin/ckeditor/ckeditor.js",
                     "~/Plugin/ckfinder/ckfinder.js",
                     "~/Scripts/ng-ckeditor.js",
                      "~/Scripts/toastr.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                    "~/Scripts/Admin/JS/app.js",
                    "~/Scripts/Admin/JS/PRO_IndexCtr.js",
                     "~/Scripts/Admin/JS/DASH_IndexCtr.js"
                    ));

            bundles.Add(new StyleBundle("~/Content/CssAdmin").Include(
                       "~/Content/bootstrap.css",
                     "~/fonts/css/font-awesome.min.css",                    
                     "~/Content/kendo/2016.2.607/kendo.common.min.css",
                      "~/Content/kendo/2016.2.607/kendo.default.min.css",
                       "~/Content/ng-ckeditor.css",
                      "~/Content/toastr.css",
                      "~/Content/Admin/css/styles.css"
                      ));
        }
    }
}
