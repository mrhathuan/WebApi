using System.Web.Mvc;

namespace Shop_Nhi.Areas.Pn
{
    public class PnAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Pn";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Pn_default",
                "Pn/{controller}/{action}/{id}",
                new { controller = "Pn", action = "Index", id = UrlParameter.Optional 
                });

                //Quên mật khẩu
            context.MapRoute(
                "Quenmatkhau",
                "quen-mat-khau",
                new { controller = "Login", action = "Quenmatkhau", id = UrlParameter.Optional }
            );

            //error
            context.MapRoute(
              "Error",
              "loi-truy-xuat",
              new { controller = "Login", action = "Error", id = UrlParameter.Optional }
          );


            //Đăng nhập
            context.MapRoute(
                "Login",
                "dang-nhap",
                new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}