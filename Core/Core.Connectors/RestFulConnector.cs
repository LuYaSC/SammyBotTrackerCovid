using TC.Core.Validation;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace TC.Core.Connectors
{
    public abstract class RestFulConnector<REQUEST, RESPONSE> : BaseConnector<REQUEST, RESPONSE>
        where REQUEST : class
        where RESPONSE : class, new()
    {
        public RestFulConnector(string url, REQUEST request, IValidator<REQUEST> validator, string jwt = null)
            : base(request, validator)
        {
            this.url = url;
            this.Jwt = jwt;
        }

        protected string url;

        protected WebClient client;
        protected DataContractJsonSerializerSettings dataContractJsonSerializerSettings = new DataContractJsonSerializerSettings();

    public string Jwt { get; set; }

        protected override void BeforeProcess()
        {

        }

        protected override void DisposeConnection()
        {
            if (this.client != null)
            {
                this.client.Dispose();
            }
        }

        protected override bool OpenConnenction()
        {
            try
            {
                this.client = new WebClient();
                this.client.Headers["Content-type"] = "application/json";
                if (!string.IsNullOrEmpty(Jwt))
                {
                    this.client.Headers.Add(HttpRequestHeader.Authorization, this.Jwt);
                }

                return true;
            }
            catch (Exception e)
            {
                this.Response.SetException(e);
                return false;
            }
        }

        protected RESPONSE GetObject(byte[] data)
        {
            var stream = new MemoryStream(data);
            var jsonSerializer = new DataContractJsonSerializer(typeof(RESPONSE), dataContractJsonSerializerSettings);
            var result = (RESPONSE)jsonSerializer.ReadObject(stream);
            return result;
        }

        protected byte[] GetStream(object data)
        {
            var stream = new MemoryStream();
            var serializer = new DataContractJsonSerializer(data.GetType());
            serializer.WriteObject(stream, data);
            var temp = stream.ToArray();
            return stream.ToArray();
        }
    }
}
