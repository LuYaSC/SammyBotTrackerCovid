namespace TC.Core.Connectors
{
    using TC.Core.Validation;
    using Microsoft.Extensions.Configuration;
    using System.ServiceModel;

    public abstract class ServiceReferenceBaseConnector<REQUEST, RESPONSE, SERVICE, ISERVICE, RESPONSE_SERVICE> : ServiceBaseConnector<REQUEST, SERVICE, RESPONSE_SERVICE, RESPONSE>
        where REQUEST : class, new()
        where RESPONSE : class, new()
        where ISERVICE : class
        where SERVICE : ClientBase<ISERVICE>
    {
        public ServiceReferenceBaseConnector(REQUEST request, IValidator<REQUEST> validator, IConfigurationSection configuration = null, bool isDummy = false)
            : base(request, validator, isDummy)
        {
            this.configuration = configuration;
        }

        protected IConfigurationSection configuration;

        protected override void DisposeConnection()
        {
            if (Service != null && Service.State != CommunicationState.Faulted)
            {
                Service.Close();
            }
            else
            {
                if (Service != null && Service.State == CommunicationState.Faulted)
                {
                    Service.Abort();
                }
            }
        }

        protected override bool OpenConnenction()
        {
            var openConnectionResponse = base.OpenConnenction();
            if (Service != null && configuration != null)
            {
                Service.Endpoint.Address = new EndpointAddress(configuration["Address"]);
            }
            return openConnectionResponse;
        }
    }
}
