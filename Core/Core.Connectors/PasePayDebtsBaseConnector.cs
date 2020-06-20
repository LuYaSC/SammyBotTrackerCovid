using TC.Connectors.Models;
using TC.Core.Connectors.Models;
using TC.Core.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Connectors
{
    public abstract class PasePayDebtsBaseConnector<SERVICE, RESPONSE> : ServiceBaseConnector<PasePayRequest, SERVICE, PasePayResponse, RESPONSE>
        where RESPONSE : class, new()
        where SERVICE : class
    {
        WebClient client = new WebClient();
        const string METHOD_POST = "POST";
        DataContractJsonSerializer jsonSerializer;
        Stream stream;
        byte[] response;

        public PasePayDebtsBaseConnector(PasePayRequest request) : base(request, new PasePayValidator()) { }

        protected override void BeforeProcess() {}

        protected abstract string ConfigureUrl();

        protected override bool OpenConnenction()
        {
            client.Headers["Content-type"] = "application/json";
            client.Credentials = CredentialCache.DefaultCredentials;
            client.UseDefaultCredentials = true;
            return true;
        }

        protected override PasePayResponse CallService()
        {
            MemoryStream MStream = new MemoryStream();
            jsonSerializer = new DataContractJsonSerializer(typeof(PasePayRequest));
            jsonSerializer.WriteObject(MStream, request);
            try
            {
                response = client.UploadData(ConfigureUrl(), METHOD_POST, MStream.ToArray());
            }
            catch (Exception ex)
            {
                return new PasePayResponse { CodigoRespuesta = "001", DetalleRespuesta = ex.Message };
            }
            return GetResponseService(response);
            
        }

        private PasePayResponse GetResponseService(byte[] responseService)
        {
            jsonSerializer = new DataContractJsonSerializer(typeof(PasePayResponse));
            stream = new MemoryStream(response);
            return (PasePayResponse)jsonSerializer.ReadObject(stream);
        }

        protected override void DisposeConnection()
        {
            client.Dispose();
        }
    }
}
