namespace TC.Core.Connectors
{
    using System;
    using System.Net;
    using TC.Core.Connectors.Models;
    using TC.Core.Validation;

    public abstract class FTPConnector<REQUEST, RESPONSE, CONNECTOR_RESPONSE> : FTPBaseConnector<REQUEST, RESPONSE, CONNECTOR_RESPONSE>
        where REQUEST : ProtocolsRequest, new()
        where RESPONSE: class, new()
        where CONNECTOR_RESPONSE: class, new()
    {
        bool isValid;
        protected FtpWebResponse response;

        public FTPConnector(REQUEST request, IValidator<REQUEST> validator, bool isDummy = false) : base(request, validator, isDummy)
        {
        }

        protected override void DisposeConnection()
        {
            if (response != null)
            {
                response.Close();
            }
        }

        protected override bool OpenConnenction()
        {
            FtpWebRequest request = FtpWebRequest.Create(this.request.Host) as FtpWebRequest;
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(this.request.UserName, this.request.Password);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;
            try
            {
                response = (FtpWebResponse)request.GetResponse();
            }
            catch(Exception e)
            {
                this.Response.SetException(e);
                return false;
            }
            return isValid = response.StatusCode == FtpStatusCode.OpeningData ? true : false || response.StatusCode == FtpStatusCode.DataAlreadyOpen;
        }

    }
}
