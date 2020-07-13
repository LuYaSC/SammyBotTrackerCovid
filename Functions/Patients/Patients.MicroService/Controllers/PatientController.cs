using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TC.Core.Business;
using TC.Core.MicroService;
using TC.Functions.Patients.Business;
using TC.Functions.Patients.Business.Models;

namespace TC.Functions.Patients.MicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]

    public class PatientController : BaseController
    {
        IPatientsBusiness service;

        public PatientController(IPatientsBusiness service)
        {
            this.service = service;
        }

        public Result<List<GetPatientResult>> GetPatients([FromBody] GetPatientsDto dto) => service.GetPatients(dto);

        public Result<List<TechnicalSheetResult>> GetTechnicalSheet([FromBody] TechnicalSheetDto dto) => service.GetTechnicalSheet(dto);

        public Result<string> ConfirmedTest([FromBody] TechnicalSheetDto dto) => service.ConfirmedTest(dto);
    }
}
