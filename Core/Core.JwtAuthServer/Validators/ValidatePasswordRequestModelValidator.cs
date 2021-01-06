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
    public class ValidatePasswordRequestModelValidator : BaseValidator<ValidatePasswordRequestModel>
    {
        protected override List<IValidation> RulesValidate => new List<IValidation>()
        {
            //new StringValidation<Required>(this.Data.AccessNumber, "Número de acceso es requerido", "AccessNumber"),
            //new StringValidation<StregthPassword>(this.Data.ConfirmPassword, "Confirmación del password es invalido", "ConfirmPassword"),
            //new StringValidation<StregthPassword>(this.Data.NewPassword, "Password es invalido", "NewPassword")
        };
    }
}