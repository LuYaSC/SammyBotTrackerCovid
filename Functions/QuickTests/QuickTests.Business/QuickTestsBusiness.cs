using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Business;
using TC.Core.Data;
using TC.Functions.QuickTests.Business.Models;

namespace TC.Functions.QuickTests.Business
{
    public class QuickTestsBusiness : BaseBusiness<CasosAgenda, QuickTestsContext>
    {
        public QuickTestsBusiness(QuickTestsContext context, IPrincipal userInfo) : base(context, userInfo, null)
        {
        }

        public Result<string> ScheduleQuickTest(GetDataDto dto)
        {
            return null;
        }

        //public Result<>
    }
}
