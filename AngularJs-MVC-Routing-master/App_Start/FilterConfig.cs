using System.Web;
using System.Web.Mvc;

namespace AngularJs_MVC_Routing
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
