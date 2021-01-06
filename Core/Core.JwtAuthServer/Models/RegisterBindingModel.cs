using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using TC.Core.JwtAuthServer.Validators;
using TC.Core.Validation;

namespace TC.Core.JwtAuthServer.Models
{
    public class RegisterBindingModel : BaseValidatableModel<RegisterBindingModelValidator, RegisterBindingModel>
    {        
        public string AccessNumber { get; set; }

        public int CompanyId { get; set; }

        public string UserId { get; set; }

        protected override RegisterBindingModel GetThis()
        {
            return this;
        }
    }

    public class PasswordModel
    {
        public string User { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //PIN
    }
}
