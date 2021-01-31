using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Data.Context;

namespace TC.Functions.GetInformationPatient.Business
{
    public class GetInformationPatientContext : SBTCContext
    {
        public GetInformationPatientContext(string nameOrConnectionString = "SBTCContext") : base(nameOrConnectionString)
        {
        }
    }
}
