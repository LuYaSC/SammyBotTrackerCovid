using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Connectors;
using TC.Core.Connectors.Models;
using TC.Core.Validation;

namespace TC.Connectors.JwtAuth.RegisterUser
{
    public class RegisterUserConnector : RestFulPostConnector<RegisterUserRequest, ResultContract<RegisterUserResponse>>
    {
        public RegisterUserConnector(RegisterUserRequest request, string url)
            : base(request, url, new RegisterUserValidator()) => Execute();
    }
}
