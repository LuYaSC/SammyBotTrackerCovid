using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Connectors.JwtAuth.RegisterUser
{
    public class RegisterUserRequest
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
    }

    public class UserRoleDto
    {
        public int RoleId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
