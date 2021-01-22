using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TC.Core.Business;
using TC.Functions.Administration.Business;
using TC.Functions.Administration.Business.Models;

namespace TC.Functions.Administration.MicroService.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]

    public class AdministrationController : ControllerBase
    {
        IAdministrationBusiness business;

        public AdministrationController(IAdministrationBusiness business)
        {
            this.business = business;
        }

        public Result<string> CreateUser([FromBody] GetUserDto dto) => business.CreateUser(dto);
        public Result<List<GetUserResult>> GetListUsers([FromBody] GetUserDto dto) => business.GetListUsers(dto);
        public Result<GetUserResult> GetUserId([FromBody] GetUserDto dto) => business.GetUserId(dto);
        public Result<string> ChangeStateUser([FromBody] GetUserDto dto) => business.ChangeStateUser(dto);
        public Result<string> SendDatesUser() => business.SendDatesUser();
    }
}
