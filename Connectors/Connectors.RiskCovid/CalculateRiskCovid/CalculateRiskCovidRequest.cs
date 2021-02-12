using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Connectors.RiskCovid.CalculateRiskCovid
{
    public class CalculateRiskCovidRequest
    {
        public bool UOMSYSTEM { get; set; }
        public int xray { get; set; }
        public int age { get; set; }
        public int hemo { get; set; }
        public int dys { get; set; }
        public int uncon { get; set; }
        public int comorbid { get; set; }
        public int cancer { get; set; }
        public decimal ratio { get; set; }
        public decimal lactate { get; set; }
        public decimal bili { get; set; }
        public string webLanguage { get; set; }
    }
}
