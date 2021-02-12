using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Functions.GetInformationPatient.Business.Models
{
    public class GetRiskCovidDto
    {
        // "UOMSYSTEM": false,
        public bool Xray { get; set; }
        public int Age { get; set; }
        public bool Hemo { get; set; }
        public bool Dys { get; set; }
        public bool Uncon { get; set; }
        public int Comorbid { get; set; }
        public bool Cancer { get; set; }
        public decimal Ratio { get; set; }
        public decimal Lactate { get; set; }
        public decimal Bili { get; set; }
    }
}
