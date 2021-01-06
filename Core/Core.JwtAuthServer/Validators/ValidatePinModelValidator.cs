using TC.Core.JwtAuthServer.Models;
using TC.Core.Validation;
using TC.Core.Validation.Validations.Decimal;
using TC.Core.Validation.Validations.Decimal.Conditions;
using TC.Core.Validation.Validations.String;
using TC.Core.Validation.Validations.String.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TC.Core.JwtAuthServer.Validators
{
    public class ValidatePinModelValidator : BaseValidator<ValidatePinRequestModel>
    {
        protected override List<IValidation> RulesValidate => new List<IValidation>() {
            new StringValidation<Required>(this.Data.Card,"Numero de acceso es requerido","AccessNumber"),
            new StringValidation<Required>(this.Data.Pin,"El Pin es requerido","Pin"),
            new StringValidation<Required>(this.Data.CaptchaValue,"El Captcha es requerido","Captcha"),
            new StringValidation<Required>(this.Data.CaptchaValueToVerify,"El Captcha es requerido","Captcha")
        };
    }
}