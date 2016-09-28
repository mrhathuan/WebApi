using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Shop_Nhi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
              name: "Demo",
              url: "demo",
              defaults: new { controller = "Home", action = "Demo", id = UrlParameter.Optional },
              namespaces: new[] { "Shop_Nhi.Controllers" }
          );

            //Cart
            routes.MapRoute(
               name: "Cart",
               url: "gio-hang",
               defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "Shop_Nhi.Controllers" }
           );


            //Product Category
            routes.MapRoute(
                name: "Product Category",
                url: "san-pham/{metatTitle}-{cateId}",
                defaults: new { controller = "Product", action = "ProductCategory", id = UrlParameter.Optional },
                namespaces: new[] { "Shop_Nhi.Controllers" }
                );
            
            //Tag
            routes.MapRoute(
               name: "Tag",
               url: "tag/{tagId}",
               defaults: new { controller = "Post", action = "Tag", id = UrlParameter.Optional },
               namespaces: new[] { "Shop_Nhi.Controllers" }
               );

            //Tag
            routes.MapRoute(
               name: "Contact",
               url: "lien-he",
               defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional },
               namespaces: new[] { "Shop_Nhi.Controllers" }
               );

            
            routes.MapRoute(
                name: "Payment",
                url: "thanh-toan-don-hang",
                defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
                namespaces: new[] { "Shop_Nhi.Controllers" }
                );


            //Page
            routes.MapRoute(
                name: "Page",
                url: "page/{linkMenu}",
                defaults: new { controller = "Home", action = "PageContent", id = UrlParameter.Optional },
                namespaces: new[] { "Shop_Nhi.Controllers" }
                );

            //baiviet
            routes.MapRoute(
               name: "Post",
               url: "bai-viet",
               defaults: new { controller = "Post", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "Shop_Nhi.Controllers" }
               );

            //baiviet
            routes.MapRoute(
               name: "PostDetail",
               url: "bai-viet/{metatTitle}-{id}",
               defaults: new { controller = "Post", action = "Detail", id = UrlParameter.Optional },
               namespaces: new[] { "Shop_Nhi.Controllers" }
               );

            //ListAll
            routes.MapRoute(
                name: "ListAll",
                url: "san-pham-moi",
                defaults: new { controller = "Product", action = "ListAll", id = UrlParameter.Optional },
                namespaces: new[] { "Shop_Nhi.Controllers" }
                );

          

            //Search
            routes.MapRoute(
                name: "Search",
                url: "tim-kiem",
                defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
                namespaces: new[] { "Shop_Nhi.Controllers" }
                );

            //Details
            routes.MapRoute(
                name: "Details",
                url: "chi-tiet/{metatTitle}-{id}",
                defaults: new { controller = "Product", action = "Details", id = UrlParameter.Optional },
                namespaces: new[] { "Shop_Nhi.Controllers" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
