using System.Web;
using System.Web.Optimization;

namespace Shop_Nhi
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-ui.js",
                       "~/Scripts/bootstrap.min.js",
                      "~/Scripts/jquery.smartmenus.js",
                      "~/Scripts/jquery.smartmenus.bootstrap.js",
                      "~/Scripts/sequence.js",
                      "~/Scripts/sequence-theme.modern-slide-in.js"                                          
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jsController").Include(                          
                        "~/Scripts/custom.js",
                       "~/Scripts/toastr.js",                       
                       "~/Scripts/nouislider.js",
                      "~/Scripts/slick.js",
                       "~/Scripts/JS/JsAddCart.js",
                      "~/Scripts/JS/JsSearch.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",                      
                      "~/Content/jquery.smartmenus.bootstrap.css",
                      "~/Content/font-awesome.css",
                       "~/Content/slick.css",
                        "~/Content/nouislider.css",
                        "~/Content/sequence-theme.modern-slide-in.css",
                        "~/Content/eagle.gallery.css",                        
                        "~/Content/jquery-ui.css",
                        "~/Content/toastr.css",
                        "~/Content/style.css"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/JsAdmin").Include(
                     "~/Scripts/bootstrap.min.js",                                         
                     "~/Scripts/kendo/2016.2.607/kendo.all.min.js",
                     "~/Scripts/kendo/2016.2.607/kendo.aspnetmvc.min.js",
                     "~/Scripts/kendo/2016.2.607/jszip.min.js",
                     "~/Plugin/ckeditor/ckeditor.js",
                     "~/Plugin/ckfinder/ckfinder.js",
                     "~/Scripts/ng-ckeditor.js",                                          
                      "~/Scripts/Admin/Chart.min.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(                    
                    "~/Scripts/Admin/JS/DASH_IndexCtr.js",
                    "~/Scripts/Admin/JS/PRO_IndexCtr.js",
                    "~/Scripts/Admin/JS/CAT_IndexCtr.js",
                    "~/Scripts/Admin/JS/ORD_IndexCtr.js",                     
                     "~/Scripts/Admin/JS/USER_IndexCtr.js",
                     "~/Scripts/Admin/JS/USER_ChangePassCtr.js",
                     "~/Scripts/Admin/JS/POST_IndexCtr.js",
                     "~/Scripts/Admin/JS/CONTENT_IndexCtr.js",
                      "~/Scripts/Admin/JS/NOTI_IndexCtr.js",
                      "~/Scripts/Admin/JS/CONTACT_IndexCtr.js",
                      "~/Scripts/Admin/JS/MENU_IndexCtr.js",
                      "~/Scripts/Admin/JS/PAGE_IndexCtr.js",
                      "~/Scripts/Admin/JS/SLIDE_IndexCtr.js",
                      "~/Scripts/Admin/JS/FOOTER_IndexCtr.js",
                      "~/Scripts/Admin/JS/SEO_IndexCtr.js",
                      "~/Scripts/Admin/JS/PAYMENT_IndexCtr.js",
                      "~/Scripts/Admin/JS/ERROR_IndexCtr.js",
                      "~/Scripts/toastr.js"
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
            BundleTable.EnableOptimizations = true;
        }
    }
}
