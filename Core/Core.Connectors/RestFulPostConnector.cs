namespace TC.Core.Connectors
{
    using TC.Core.Validation;
    using System;

    public class RestFulPostConnector<REQUEST, RESPONSE> : RestFulConnector<REQUEST, RESPONSE>
        where RESPONSE : class, new()
        where REQUEST : class, new()
    {
        public RestFulPostConnector(REQUEST request, string url, IValidator<REQUEST> validator, string jwt = null)
            : base(url, request, validator, jwt)
        {
        }

        protected override void Process()
        {
            try
            {
                var response = this.client.UploadData(this.url, "POST", this.GetStream(this.request));
                Response.Body = GetObject(response);
            }
            catch (Exception e)
            {
                this.Response.SetException(e);
            }
        }
    }
}
