using TC.Core.JwtAuthServer.Validators;
using TC.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TC.Core.JwtAuthServer.Models
{
    public class ChangePasswordRequestModel : BaseValidatableModel<ChangePasswordRequestModelValidator, ChangePasswordRequestModel>/*, IVerifyCaptcha*/
    {
        public string AccessNumber { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string CaptchaValue { get; set; }
        public string CaptchaValueToVerify { get; set; }
        public string IpClient { get; set; }

        protected override ChangePasswordRequestModel GetThis()
        {
            return this;
        }
    }
}