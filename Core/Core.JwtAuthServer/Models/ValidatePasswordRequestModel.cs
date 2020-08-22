using TC.Core.JwtAuthServer.Validators;
using TC.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TC.Core.JwtAuthServer.Models
{
    public class ValidatePasswordRequestModel : BaseValidatableModel<ValidatePasswordRequestModelValidator, ValidatePasswordRequestModel>
    {
        public string Password { get; set; }
        protected override ValidatePasswordRequestModel GetThis()
        {
            return this;
        }
    }
}