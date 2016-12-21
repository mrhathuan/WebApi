using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Hosting;
using Microsoft.AspNet.SignalR;
using System.Threading;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Generic;
using DTO;

[assembly: OwinStartup(typeof(ClientWeb.Startup))]
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace ClientWeb
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR(new HubConfiguration
            {
                EnableJSONP = true
            });
        }
    }

    public class ClientHub : Hub
    {
        public void MessageCall(int userid, int total)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();
            IClientProxy proxy = context.Clients.All;
            dynamic objDynamic = new { total = total };
            proxy.Invoke("messagecall" + userid, objDynamic);
        }

        public void DITOMasterChangeStatus(int id, int statusid, string name)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();
            dynamic objDynamic = new { id = id, statusid = statusid, drivername = name };
            context.Clients.All.ditoMasterChangeStatus(objDynamic);
        }
    }

    public class BackgroundServerTimeTimer : IRegisteredObject
    {
        private Timer taskTimer;
        private IHubContext hub;

        public BackgroundServerTimeTimer()
        {
            HostingEnvironment.RegisterObject(this);
            if (EventCommon.IsRegistryWorkflow())
                EventCommon.OnEventCommonWorkflowChanged += EventCommon_OnEventCommonWorkflowChanged;

            hub = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();

            taskTimer = new Timer(OnTimerElapsed, null, new TimeSpan(0, 0, 1, 0, 0), new TimeSpan(0, 0, 1, 0, 0));
        }

        protected void EventCommon_OnEventCommonWorkflowChanged(List<EventCommonWorkflow> sender)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ClientHub>();
            dynamic objDynamic = new { sender = sender };
            context.Clients.All.eventcommonworkflow(objDynamic);
        }

        private void OnTimerElapsed(object sender)
        {
            hub.Clients.All.serverTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        }

        public void Stop(bool immediate)
        {
            taskTimer.Dispose();

            HostingEnvironment.UnregisterObject(this);
        }
    }
}
