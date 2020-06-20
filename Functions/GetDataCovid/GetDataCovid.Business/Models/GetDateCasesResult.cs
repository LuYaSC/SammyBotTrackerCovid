using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.GetDataCovid.Business.Models
{
    public class GetDateCasesResult
    {
        public GetDateCasesResult()
        {
            TotalCasesCountry = new TotalCases();
            TotalCasesState = new TotalCases();
        }

        public TotalCases TotalCasesCountry { get; set; }

        public TotalCases TotalCasesState { get; set; }
    }

    public class TotalCases
    {
        public int TotalInfecteds { get; set; }

        public int TotalWishes { get; set; }

        public int TotalRecovered { get; set; }
    }
}
