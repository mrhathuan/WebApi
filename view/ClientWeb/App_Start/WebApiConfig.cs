using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ClientWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}"
            );

            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;

            //// Enforce HTTPS
            //config.Filters.Add(new ClientWeb.Common.RequireHttpsAttribute());

            //Filter to generate custom web api error base on DTOError
            config.Filters.Add(new HandleApiExceptionAttribute());

            //log4net
            log4net.Config.XmlConfigurator.Configure(); 

            Telerik.Reporting.Services.WebApi.ReportsControllerConfiguration.RegisterRoutes(config);
        }
    }
}
