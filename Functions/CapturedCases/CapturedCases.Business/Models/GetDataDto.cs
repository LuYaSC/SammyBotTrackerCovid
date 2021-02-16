using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Functions.CapturedCases.Business.Models
{
    public class GetDataDto
    {
        public string PhoneNumber { get; set; }

        public string DocumentNumber { get; set; }

        public int Level { get; set; }

        public string Origin { get; set; }

        public bool IsInsured { get; set; }

        public string InsuredName { get; set; }
    }
}
