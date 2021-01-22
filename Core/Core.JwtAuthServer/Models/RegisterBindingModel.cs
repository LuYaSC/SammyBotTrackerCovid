using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using TC.Core.JwtAuthServer.Entities;
using TC.Core.JwtAuthServer.Validators;
using TC.Core.Validation;

namespace TC.Core.JwtAuthServer.Models
{
    public class RegisterBindingModel : BaseValidatableModel<RegisterBindingModelValidator, RegisterBindingModel>
    {        
        public string AccessNumber { get; set; }

        public string User { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int AvailableDays { get; set; }

        public string Name { get; set; }

        public string FirstLastName { get; set; }

        public string SecondLastName { get; set; }

        public string IpClient { get; set; }

        public string NewPassword { get; set; }

        public List<UserRoleDto> Roles { get; set; }


        protected override RegisterBindingModel GetThis()
        {
            return this;
        }
    }

    public class UserRoleDto
    {
        public int RoleId { get; set; }

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
