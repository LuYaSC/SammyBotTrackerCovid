using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Connectors.Models;
using TC.Core.Validation;

namespace TC.Core.Connectors
{
    public abstract class FTPBaseConnector<REQUEST, RESPONSE, CONNECTOR_RESPONSE> : BaseConnector<REQUEST, RESPONSE>
       where REQUEST : class
       where RESPONSE : class, new()
    {
        public FTPBaseConnector(REQUEST request, IValidator<REQUEST> validator, bool isDummy = false) : base(request, validator, isDummy)
        {
        }

        protected override void BeforeProcess()
        {
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

        protected abstract CONNECTOR_RESPONSE CallService();

        protected abstract BaseResponse<RESPONSE> Mapping(CONNECTOR_RESPONSE responseService);

    }
}
