using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Data.Context;

namespace CaseRecovery.Business
{
    public class CaseRecoveryContext : SBTCContext
    {
        public CaseRecoveryContext(string nameOrConnectionString = "SBTCContext") : base(nameOrConnectionString)
        {
        }


    }
}
