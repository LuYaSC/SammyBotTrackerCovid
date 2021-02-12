using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Business;

namespace TC.Functions.Administration.Business.Models
{
    public class GetUserDto : IPagination
    {
        public string Enrollment { get; set; }

        public int userId { get; set; }

        public string State { get; set; }

        public string AccessNumber { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public string FirstLastName { get; set; }

        public string SecondLastName { get; set; }

        public string NewPassword { get; set; }

        public List<UserRoleDto> Roles { get; set; }

        public bool NotifyUser { get; set; }

        public int PageSize { get; set; }

        public int CurPage { get; set; }

        public int TotalItems { get; set; }
    }

    public class UserRoleDto
    {
        public int RoleId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
