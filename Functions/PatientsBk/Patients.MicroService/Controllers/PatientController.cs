using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SBTC.Core.MicroService;
using SBTC.Functions.Patients.Business;
using SBTC.Functions.Patients.Business.BaseBusiness;
using SBTC.Functions.Patients.Business.Models;

namespace SBTC.Functions.Patients.MicroService.Controllers
{
    [Route("api/[controller]/[action]")]

    public class PatientController : BaseController
    {
        IPatientBusiness service;
        IInitialDiagnosisCardBusiness saveForm;

        public PatientController(IPatientBusiness service, IInitialDiagnosisCardBusiness saveForm)
        {
            this.service = service;
            this.saveForm = saveForm;
        }

        public Result<List<GetPatientResult>> GetPatients([FromBody]  GetPatientsDto dto) => service.GetPatients(dto);

        public Result<List<TechnicalSheetResult>> GetTechnicalSheet([FromBody] TechnicalSheetDto dto) => service.GetTechnicalSheet(dto);

        public Result<string> ConfirmedTest([FromBody] TechnicalSheetDto dto) => service.ConfirmedTest(dto);

        public Result<FormSaveResult> SaveForm([FromBody] FormDiagDto dto) => saveForm.SaveForm(dto);

        public Result<List<GetFormPrevious>> GetFormPrevious([FromBody] GetFormPreviousDto dto) => saveForm.GetFormPrevious(dto);

        public Result<FormDiagResult> GetFormById([FromBody] GetFormById dto) => saveForm.GetFormById(dto);
    }
}
