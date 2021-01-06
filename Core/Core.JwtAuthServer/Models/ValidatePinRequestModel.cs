using TC.Core.JwtAuthServer.Validators;
using TC.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TC.Core.JwtAuthServer.Models
{
    public class ValidatePinRequestModel : BaseValidatableModel<ValidatePinModelValidator, ValidatePinRequestModel>//, IVerifyCaptcha, ISmartlinkDto
    {
        public string Card { get; set; }
        public string Pin { get; set; }
        public string CaptchaValue { get; set; }
        public string CaptchaValueToVerify { get; set; }

        protected override ValidatePinRequestModel GetThis()
        {
            return this;
        }
    }

}