using SBTC.Core.Data;
using SBTC.Core.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.GetDataCovid.Business
{
    public class GetDataCovidContext : SBTCContext
    {
        public GetDataCovidContext(string nameOrConnectionString = "SBTCContext") : base(nameOrConnectionString)
        {
        }

        public DbSet<CasesByCountry> CasesByCountries  { get; set; }

        public DbSet<CasesByState> CasesByStates { get; set; }
    }
}
