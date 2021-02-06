using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Connectors;

namespace TC.Connectors.RiskCovid.CalculateRiskCovid
{
    public class CalculateRiskCovidConnector : RestFulPostConnector<CalculateRiskCovidRequest, CalculateRiskCovidResponse>
    {
        public CalculateRiskCovidConnector(CalculateRiskCovidRequest request, string url) :
            base(request, url, new CalculateRiskCovidValidator()) => Execute();
    }
}
