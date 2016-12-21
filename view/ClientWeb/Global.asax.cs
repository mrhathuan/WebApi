using CacheManager.Core;
using Microsoft.AspNet.SignalR;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.WebApi;
using Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace ClientWeb
{
    public class Global : System.Web.HttpApplication
    {
        private BackgroundServerTimeTimer bstt;

        protected void Application_Start(object sender, EventArgs e)
        {
            bstt = new BackgroundServerTimeTimer();
            GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(50);

            GlobalConfiguration.Configure(WebApiConfig.Register);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //var container = new UnityContainer();
            //GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            //var cacheConfig = ConfigurationBuilder.BuildConfiguration(settings =>
            //{
            //    settings
            //        .WithSystemRuntimeCacheHandle("inprocess").WithExpiration(ExpirationMode.Absolute, TimeSpan.FromHours(23));
            //});
            //container.RegisterType(
            //    typeof(ICacheManager<>),
            //    new ContainerControlledLifetimeManager(),
            //    new InjectionFactory(
            //        (c, t, n) => CacheFactory.FromConfiguration(t.GetGenericArguments()[0], cacheConfig)));

            ServiceFactory.Init();

            ServiceFactory.SVApp((IServices.ISVSystem sv) =>
                {
                    sv.App_Init();
                }, "init", -1);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}