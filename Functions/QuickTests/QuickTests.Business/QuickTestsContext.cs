using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Data.Context;

namespace TC.Functions.QuickTests.Business
{
    public class QuickTestsContext : SBTCContext
    {
        public QuickTestsContext(string nameOrConnectionString = "SBTCContext") : base(nameOrConnectionString)
        {
        }
    }
}
