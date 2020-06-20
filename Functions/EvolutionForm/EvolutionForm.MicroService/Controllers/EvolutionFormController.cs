using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TC.Core.Business;
using TC.Core.MicroService;
using TC.Functions.EvolutionForm.Business;
using TC.Functions.EvolutionForm.Business.Models;

namespace TC.Functions.EvolutionForm.MicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]

    public class EvolutionFormController : BaseController
    {
        IEvolutionFormBusiness service;

        public EvolutionFormController(IEvolutionFormBusiness service)
        {
            this.service = service;
        }

        public Result<List<GetHistoryClinicResult>> GetHistoryClinics() => service.GetHistoryClinics();

        public Result<string> SaveEvolutionForm([FromBody] SaveFormDto dto) => service.SaveEvolutionForm(dto);

        public Result<GetBasicDatesFormResult> GetBasicData([FromBody] GetBasicDatesFormDto dto) => service.GetBasicData(dto);
    }
}
