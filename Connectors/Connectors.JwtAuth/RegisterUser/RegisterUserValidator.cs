using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Validation;

namespace TC.Connectors.JwtAuth.RegisterUser
{
    public class RegisterUserValidator : BaseValidator<RegisterUserRequest>
    {
        protected override List<IValidation> RulesValidate => new List<IValidation>
        {

        };
    }
}