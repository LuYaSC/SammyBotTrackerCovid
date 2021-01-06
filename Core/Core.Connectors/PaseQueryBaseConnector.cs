using TC.Core.Connectors.Models;
using TC.Core.Validation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TC.Core.Connectors
{
    public abstract class PaseQueryBaseConnector<REQUEST, SERVICE, RESPONSE> : ServiceBaseConnector<REQUEST, SERVICE, PaseResponse, RESPONSE>
        where REQUEST : class, new()
        where RESPONSE : class, new()
        where SERVICE : class
    {
        IConfigurationSection configuration;
        HttpWebResponse responseService;
        public string resultService;
        const string METHOD_GET = "GET";
        public const string CODE_OK_PASE = "000";
        public string Trace;

        public PaseQueryBaseConnector(REQUEST request, IValidator<REQUEST> validator, IConfigurationSection configuration) 
            : base(request, validator)
        {
            this.configuration = configuration;
            GetTrace();
        }

        protected abstract string ConfigureUrl();

        protected override bool OpenConnenction()
        {
            try
            {
                WebRequest request = WebRequest.Create(ConfigureUrl());
                request.Method = METHOD_GET;
                responseService = request.GetResponse() as HttpWebResponse;
            }
            catch
            {
                return false;
            }
            return true;
        }

        protected override PaseResponse CallService()
        {
            PaseResponse result = new PaseResponse();
            if (responseService.StatusCode == HttpStatusCode.OK)
            {
                Stream respStream = responseService.GetResponseStream();
                StreamReader streamReader = new StreamReader(respStream);
                resultService = streamReader.ReadToEnd();
                if (resultService != null)
                {
                    using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(resultService)))
                    {
                        DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(PaseResponse));
                        result = (PaseResponse)deserializer.ReadObject(ms);
                    }
                }
            }
            return result;
        }

        protected override void DisposeConnection()
        {
            if(responseService != null)
            {
                responseService.Dispose();
            }
        }

        public void GetTrace()
        {
            Thread.Sleep(1000);
            Trace = $"{DateTime.Now.ToString("yyMMdd")}{DateTime.Now.ToString("HHmmss")}";
        }

        protected override void BeforeProcess()  { }
    }
}
