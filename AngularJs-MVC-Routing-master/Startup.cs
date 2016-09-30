using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AngularJs_MVC_Routing.Startup))]
namespace AngularJs_MVC_Routing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
