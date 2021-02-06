using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Connectors.RiskCovid.CalculateRiskCovid
{
    public class CalculateRiskCovidResponse
    {
        public List<Response> output { get; set; }
    }

    public class Response
    {
        public string name { get; set; }

        public string value { get; set; }

        public string value_text { get; set; }

        public string message { get; set; }
    }
}
