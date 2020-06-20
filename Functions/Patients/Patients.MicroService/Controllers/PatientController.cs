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
        IInitialDiagnosisCardBusiness saveForm;

        public PatientController(IInitialDiagnosisCardBusiness saveForm)
        {
            this.saveForm = saveForm;
        }

        public Result<FormSaveResult> SaveForm([FromBody] FormDiagDto dto) => saveForm.SaveForm(dto);

        public Result<List<GetFormPrevious>> GetFormPrevious([FromBody] GetFormPreviousDto dto) => saveForm.GetFormPrevious(dto);

        public Result<FormDiagResult> GetFormById([FromBody] GetFormById dto) => saveForm.GetFormById(dto);
    }
}
