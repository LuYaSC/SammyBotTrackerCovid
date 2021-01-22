using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Business;
using TC.Functions.Administration.Business.Models;

namespace TC.Functions.Administration.Business
{
    public interface IAdministrationBusiness
    {
        Result<string> CreateUser(GetUserDto dto);

        Result<List<GetUserResult>> GetListUsers(GetUserDto dto);

        Result<GetUserResult> GetUserId(GetUserDto dto);

        Result<string> ChangeStateUser(GetUserDto dto);

        Result<string> SendDatesUser();
    }
}
