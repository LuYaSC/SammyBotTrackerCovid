using TC.Core.Connectors.Models;
using TC.Core.Validation;

namespace TC.Core.Connectors
{
    public abstract class BaseConnector<REQUEST, RESPONSE>
        where REQUEST: class
        where RESPONSE : class, new()
    {
        public BaseConnector(REQUEST request, IValidator<REQUEST> validator, bool isDummy = false)
        {            
            this.Response = new BaseResponse<RESPONSE>();
            this.IsDummy = isDummy;
            this.validator = validator;
            this.request = request;
        }

        public BaseResponse<RESPONSE> Response { get; set; }

        private IValidator<REQUEST> validator;

        protected REQUEST request;

        protected bool IsDummy;

        private const string ERROR_OPEN_CONNECTION = "Error al abrir la conexión";

        public void Execute()
        {
            this.validator.Data = request;
            if (!this.validator.valid())
            {
                this.Response.SetManyErrors(this.validator.ErrorMessages);
                return;
            }

            if (this.OpenConnenction())
            {
                this.BeforeProcess();
                this.Process();
            }
            else
            {
                this.Response = BaseResponse<RESPONSE>.SetOneError(ERROR_OPEN_CONNECTION);
            }

            this.DisposeConnection();
        }

        protected abstract bool OpenConnenction();

        protected abstract void Process();

        protected abstract void BeforeProcess();

        protected abstract void DisposeConnection();
    }
}