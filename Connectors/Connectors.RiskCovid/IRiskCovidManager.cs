using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Connectors.RiskCovid.CalculateRiskCovid;
using TC.Core.Connectors.Models;

namespace TC.Connectors.RiskCovid
{
    public interface IRiskCovidManager
    {
        BaseResponse<CalculateRiskCovidResponse> CalculateRiskCovid(CalculateRiskCovidRequest request);
    }
}
