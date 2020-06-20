using TC.Core.Connectors.Models;
using TC.Core.Validation;
using System;

namespace TC.Core.Connectors
{
    public abstract class ServiceBaseConnector<REQUEST, SERVICE, RESPONSE_SERVICE, RESPONSE> : BaseConnector<REQUEST, RESPONSE>
        where REQUEST: class, new()
        where RESPONSE : class, new()
        where SERVICE : class
    {
        public ServiceBaseConnector(REQUEST request, IValidator<REQUEST> validator, bool isDummy = false)
            :base(request, validator, isDummy)
        { }

        protected SERVICE Service;        

        protected override bool OpenConnenction()
        {            
            try
            {                
                this.Service = (SERVICE)Activator.CreateInstance(typeof(SERVICE));
                return true;
            }
            catch (Exception e)
            {
                this.Response.SetException(e);
                return false;
            }
        }

        protected override void Process()
        {
            try
            {
                var responseService = this.CallService();
                this.Response = this.Mapping(responseService);
            }
            catch (Exception e)
            {
                this.Response.SetException(e);
            }
        }
        
        protected abstract RESPONSE_SERVICE CallService();

        protected abstract BaseResponse<RESPONSE> Mapping(RESPONSE_SERVICE responseService);
    }
}
