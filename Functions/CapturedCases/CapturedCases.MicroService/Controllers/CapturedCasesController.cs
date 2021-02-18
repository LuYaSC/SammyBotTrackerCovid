using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TC.Core.Business;
using TC.Core.MicroService;
using TC.Functions.CapturedCases.Business;
using TC.Functions.CapturedCases.Business.Models;

namespace TC.Functions.CapturedCases.MicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]

    public class CapturedCasesController : BaseController
    {
        ICapturedCasesBusiness business;

        public CapturedCasesController(ICapturedCasesBusiness business)
        {
            this.business = business;
        }

        public Result<CreateCaseResult> CreateCase([FromBody] GetDataDto dto) => business.CreateCase(dto);

        public Result<PreviousCaseResult> GetPreviousCase([FromBody] PreviousCaseDto dto) => business.GetPreviousCase(dto);
    }
}
