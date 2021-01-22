using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Connectors.JwtAuth.RegisterUser;
using TC.Core.Connectors.Models;

namespace TC.Connectors.JwtAuth
{
    public interface IJwtAuthManager
    {
        BaseResponse<ResultContract<RegisterUserResponse>> RegisterUser(RegisterUserRequest request);
    }
}
