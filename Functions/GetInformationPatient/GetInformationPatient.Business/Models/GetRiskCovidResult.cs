using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Functions.GetInformationPatient.Business.Models
{
    public class GetRiskCovidResult
    {
        public string Points { get; set; }

        public string Percent { get; set; }

        public string Risk { get; set; }

        public string Message { get; set; }
    }
}
