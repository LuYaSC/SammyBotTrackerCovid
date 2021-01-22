using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Data.Context;

namespace TC.Functions.Administration.Business
{
    public class AdministrationContext : SBTCContext
    {
        public AdministrationContext(string nameOrConnectionString = "SBTCContext") : base(nameOrConnectionString)
        {
        }

    }
}
