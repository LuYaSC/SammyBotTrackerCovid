using TC.Core.JwtAuthServer.Validators;
using TC.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TC.Core.JwtAuthServer.Models
{
    public class NewPasswordRequestModel : BaseValidatableModel<NewPasswordRequestModelValidator, NewPasswordRequestModel>/*, IVerifyCaptcha, ISmartlinkDto*/
    {
        public string AccessNumber { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string CaptchaValue { get; set; }
        public string CaptchaValueToVerify { get; set; }
        public string Card { get; set; }
        public string Pin { get; set; }
        public string IpClient { get; set; }

        protected override NewPasswordRequestModel GetThis()
        {
            return this;
        }
    }
}