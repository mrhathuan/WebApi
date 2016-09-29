[assembly: Microsoft.Owin.OwinStartup(typeof(Shop_Nhi.Startup))]

namespace Shop_Nhi
{
    using System.Configuration;

    using CKSource.CKFinder.Connector.Config;
    using CKSource.CKFinder.Connector.Core.Builders;
    using CKSource.CKFinder.Connector.Core.Logs;
    using CKSource.CKFinder.Connector.Host.Owin;
    using CKSource.CKFinder.Connector.Logs.NLog;
    using CKSource.CKFinder.Connector.KeyValue.EntityFramework;
    using CKSource.FileSystem.Dropbox;
    using CKSource.FileSystem.Local;

    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;

    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            LoggerManager.LoggerAdapterFactory = new NLogLoggerAdapterFactory();

            RegisterFileSystems();

            builder.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                AuthenticationMode = AuthenticationMode.Active
            });

            var route = ConfigurationManager.AppSettings["ckfinderRoute"];
            builder.Map(route, SetupConnector);
        }

        private static void RegisterFileSystems()
        {
            FileSystemFactory.RegisterFileSystem<LocalStorage>();
            FileSystemFactory.RegisterFileSystem<DropboxStorage>();
        }

        private static void SetupConnector(IAppBuilder builder)
        {
            var keyValueStoreProvider = new EntityFrameworkKeyValueStoreProvider("CacheConnectionString");

            var allowedRoleMatcherTemplate = ConfigurationManager.AppSettings["ckfinderAllowedRole"];
            var authenticator = new RoleBasedAuthenticator(allowedRoleMatcherTemplate);
            
            var connectorFactory = new OwinConnectorFactory();
            var connectorBuilder = new ConnectorBuilder();
            var connector = connectorBuilder
                .LoadConfig()
                .SetAuthenticator(authenticator)
                .SetRequestConfiguration(
                    (request, config) =>
                    {
                        config.LoadConfig();
                        config.SetKeyValueStoreProvider(keyValueStoreProvider);
                    })
                .Build(connectorFactory);

            builder.UseConnector(connector);
        }
    }
}
