using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Connectors.JwtAuth.RegisterUser;
using TC.Core.Connectors.Models;

namespace TC.Connectors.JwtAuth
{
    public class JwtAuthManager : IJwtAuthManager
    {
        private string url;
        private const string REGISTER_USER = "Register";

        public JwtAuthManager(string url) => this.url = url;

        public BaseResponse<ResultContract<RegisterUserResponse>> RegisterUser(RegisterUserRequest request)
        {
            var manager = new RegisterUserConnector(request, $"{url}{REGISTER_USER}");
            return manager.Response;
        }
    }
}
