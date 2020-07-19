using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseRecovery.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TC.Core.Business;
using TC.Core.MicroService;
using TC.Functions.CaseRecovery.Business.Models;

namespace TC.Functions.CaseRecovery.MicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]

    public class CasesRecoveryController : BaseController
    {
        ICaseRecoveryBusiness service;

        public CasesRecoveryController(ICaseRecoveryBusiness service)
        {
            this.service = service;
        }

        public Result<List<CasesForRecoverResult>> GetCasesForRecovers() => service.GetCasesForRecovers();

        public Result<string> RecoverCase([FromBody] GetDataDto dto) => service.RecoverCase(dto);

        public Result<string> GenerateRoom([FromBody] GetDataDto dto) => service.GenerateRoom(dto);

        public Result<string> FinalizeCase([FromBody] GetDataDto dto) => service.FinalizeCase(dto);
    }
}
