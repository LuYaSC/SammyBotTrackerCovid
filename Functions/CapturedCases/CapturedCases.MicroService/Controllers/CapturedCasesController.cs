using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TC.Core.MicroService;

namespace TC.Functions.CapturedCases.MicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]

    public class CapturedCasesController : BaseController
    {
       
    }
}
