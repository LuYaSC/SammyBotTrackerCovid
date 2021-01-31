using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TC.Core.Business;
using TC.Core.MicroService;
using TC.Functions.GetInformationPatient.Business;
using TC.Functions.GetInformationPatient.Business.Models;

namespace GetInformationPatient.MicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    //[Authorize]

    public class GetInformationPatientController : BaseController
    {
        IGetInformationPatientBusiness business;

        public GetInformationPatientController(IGetInformationPatientBusiness business)
        {
            this.business = business;
        }

        public Result<GetInsurancePatientResult> GetInsurancePatient([FromBody] GetInsurancePatientDto dto) => business.GetInsurancePatient(dto);
    }
}
