using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http;
using System.Net.Http;
using System.ServiceModel;
using DTO;

namespace ClientWeb
{
    public class HandleApiExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            FaultException<DTOError> exception = context.Exception as FaultException<DTOError>;
            if (exception != null)
            {
                statusCode = HttpStatusCode.BadRequest;
                context.Response = context.Request.CreateResponse(statusCode, exception.Detail);
            }
            else
            {
                context.Response = context.Request.CreateResponse(statusCode, new DTOError() {ErrorMessage = context.Exception.Message});
            }

            base.OnException(context);            
        }
    }
}