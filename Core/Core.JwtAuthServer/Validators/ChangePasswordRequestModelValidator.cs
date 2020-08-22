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
    public class ChangePasswordRequestModelValidator : BaseValidator<ChangePasswordRequestModel>
    {
        protected override List<IValidation> RulesValidate => new List<IValidation>()
        {
            new StringValidation<Required>(this.Data.AccessNumber,"Numero de acceso es requerido","AccessNumber"),
            new StringValidation<StregthPassword>(this.Data.OldPassword, "Password inválido","OldPassword"),
            new StringValidation<StregthPassword>(this.Data.NewPassword, "Password inválido","NewPassword"),
            new StringValidation<StregthPassword>(this.Data.ConfirmPassword, "Password inválido","ConfirmPassword")
        };
    }
}