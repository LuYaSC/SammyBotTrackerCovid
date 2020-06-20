using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SBTC.Core.Business;
using SBTC.Core.MicroService;
using SBTC.Functions.GetDataCovid.Business;
using SBTC.Functions.GetDataCovid.Business.Models;

namespace GetDataCovid.MicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    public class GetDataCovidController : BaseController
    {
        IGetDataCovidBusiness service;

        public GetDataCovidController(IGetDataCovidBusiness service)
        {
            this.service = service;
        }

        public Result<GetDateCasesResult> GetCasesCovid([FromBody] GetDateCasesDto dto) => service.GetCasesCovid(dto);

        public Result<bool> UpdateManualDates([FromBody] UpdateDatesDto dto) => service.UpdateManualDates(dto);

        //public Result<bool> AddingQueriesApp([FromBody] )
    }
}
