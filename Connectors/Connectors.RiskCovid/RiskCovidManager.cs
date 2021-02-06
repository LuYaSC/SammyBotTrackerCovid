using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Connectors.RiskCovid.CalculateRiskCovid;
using TC.Core.Connectors.Models;

namespace TC.Connectors.RiskCovid
{
    public class RiskCovidManager : IRiskCovidManager
    {
        private string url;
        private const string METHOD_SEND_TEXT = "10303";

        public RiskCovidManager(string url) => this.url = url;

        public BaseResponse<CalculateRiskCovidResponse> CalculateRiskCovid(CalculateRiskCovidRequest request)
        {
            var manager = new CalculateRiskCovidConnector(request, $"{url}{METHOD_SEND_TEXT}");
            var result = manager.Response.Header.IsOk ? "Notifiación enviada correctamente" : "";
            return manager.Response;
        }
    }
}
