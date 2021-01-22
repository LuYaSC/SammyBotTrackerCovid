using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Functions.Administration.Business.Models
{
    public class GetUserResult
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string State { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int AvailableDays { get; set; }
    }
}
