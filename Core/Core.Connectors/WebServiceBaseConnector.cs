namespace TC.Core.Connectors
{
    using TC.Core.Validation;
    using Microsoft.Extensions.Configuration;
    using System.ServiceModel;
    using System.Web.Services.Protocols;

    public abstract class WebServiceBaseConnector<REQUEST, RESPONSE, SERVICE, RESPONSE_SERVICE> : ServiceBaseConnector<REQUEST, SERVICE, RESPONSE_SERVICE, RESPONSE>
        where REQUEST : class, new()
        where RESPONSE : class, new()
        //where RESPONSE_SERVICE : new()
        where SERVICE : SoapHttpClientProtocol
    {
        public WebServiceBaseConnector(REQUEST request, IValidator<REQUEST> validator, IConfigurationSection configuration = null, bool isDummy = false)
            : base(request, validator, isDummy)
        {
            this.configuration = configuration;
        }

        protected IConfigurationSection configuration;

        protected override void DisposeConnection()
        {
            if (this.Service != null)
            {
                this.Service.Dispose();
            }
        }

        protected override bool OpenConnenction()
        {
            try
            {
                var openConnectionResponse = base.OpenConnenction();
                if (Service != null && configuration != null)
                {
                    Service.Url = configuration["Address"];
                }
                return openConnectionResponse;

            }
            catch (System.Exception)
            {
                return false;
            }
            
        }
    }
}
