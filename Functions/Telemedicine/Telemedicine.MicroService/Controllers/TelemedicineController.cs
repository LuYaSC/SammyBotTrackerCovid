using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TC.Core.Business;
using TC.Core.MicroService;
using TC.Functions.Telemedicine.Business;
using TC.Functions.Telemedicine.Business.Models;

namespace Telemedicine.MicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]

    public class TelemedicineController : BaseController
    {
        ITelemedicineBusiness service;

        public TelemedicineController(ITelemedicineBusiness service)
        {
            this.service = service;
        }

        public Result<List<PendingAppointmentsResult>> GetPendingAppointments() => service.GetPendingAppointments();

        public Result<List<PendingAppointmentsResult>> GetPatientsAttended([FromBody] GetDataDto dto) => service.GetPatientsAttended(dto);

        public Result<AssingCaseResult> AssingCase([FromBody] GetDataDto dto) => service.AssingCase(dto);

        public Result<string> AddingDoctor([FromBody] GetDataDto dto) => service.AddingDoctor(dto);

        public Result<string> UpdateCase([FromBody] GetDataDto dto) => service.UpdateCase(dto);

        public Result<List<DoctorResult>> GetListDoctor() => service.GetListDoctor();
    }
}
