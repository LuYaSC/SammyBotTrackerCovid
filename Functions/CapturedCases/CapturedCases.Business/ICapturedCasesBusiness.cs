using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Business;
using TC.Functions.CapturedCases.Business.Models;

namespace TC.Functions.CapturedCases.Business
{
    public interface ICapturedCasesBusiness
    {
        Result<CreateCaseResult> CreateCase(GetDataDto dto);

        Result<PreviousCaseResult> GetPreviousCase(PreviousCaseDto dto);
    }
}
