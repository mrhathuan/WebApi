using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Services
{
    public class CustomMessageInspector : IDispatchMessageInspector, IClientMessageInspector
    {
        #region IDispatchMessageInspector
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            if (request.Headers.FindHeader("UserName", "") == -1)
                throw new FaultException("UserName was not provided. Access denied.");

            var username = request.Headers.GetHeader<string>("UserName", "");
            if (string.IsNullOrEmpty(username))
                throw new FaultException("UserName was not provided. Access denied.");

            return instanceContext;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
        }
        #endregion

        #region IClientMessageInspector
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {

        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var messageHeader = new MessageHeader<string>("123");
            var untypedMessageHeader = messageHeader.GetUntypedHeader("UserName", "");
            request.Headers.Add(untypedMessageHeader);
            return null;
        }
        #endregion
    }

    public class CustomServiceBehavior : Attribute, IServiceBehavior
    {
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channel in serviceHostBase.ChannelDispatchers)
            {
                foreach (var endpoint in channel.Endpoints)
                {
                    endpoint.DispatchRuntime.MessageInspectors.Add(new CustomMessageInspector());
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }

    public class CustomEndpointBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(new CustomMessageInspector());
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }

    public class CustomBehaviorExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(CustomEndpointBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new CustomEndpointBehavior();
        }
    }
}