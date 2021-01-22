using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TC.Connectors.JwtAuth.RegisterUser
{
    [DataContract]
    public class RegisterUserResponse
    {
        [DataMember(Name = "userId")]
        public int UserId { get; set; }

        [DataMember(Name = "errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
