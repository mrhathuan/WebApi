using System.Web.Mvc;

namespace Client_Web.Areas.Tk
{
    public class TkAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Tk";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Tk_default",
                "Tk/{controller}/{action}/{id}",
                new {controller = "Main", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}