using TC.Core.JwtAuthServer.Models;
using TC.Core.Validation;
using TC.Core.Validation.Validations.String;
using TC.Core.Validation.Validations.String.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TC.Core.JwtAuthServer.Validators
{
    public class RegisterBindingModelValidator : BaseValidator<RegisterBindingModel>
    {
        protected override List<IValidation> RulesValidate => new List<IValidation>()
        {
            new StringValidation<Required>(this.Data.AccessNumber, "Número de acceso es requerido", "AccessNumber")
        };
    }
}