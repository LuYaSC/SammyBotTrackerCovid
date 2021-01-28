using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Functions.Administration.Business.Models
{
    public class GetUserResult
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string FirstLastName { get; set; }

        public string SecondLastName { get; set; }

        public string State { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int AvailableDays { get; set; }

        public DateTime DateCreation { get; set; }

        public int TotalItems { get; set; }

        public List<UserRoleResult> UserRoles { get; set; }
    }

    public class UserRoleResult
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
