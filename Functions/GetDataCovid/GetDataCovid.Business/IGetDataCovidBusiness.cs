using SBTC.Core.Business;
using SBTC.Functions.GetDataCovid.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.GetDataCovid.Business
{
    public interface IGetDataCovidBusiness
    {
        Result<GetDateCasesResult> GetCasesCovid(GetDateCasesDto dto);

        Result<bool> UpdateManualDates(UpdateDatesDto dto);
    }
}
