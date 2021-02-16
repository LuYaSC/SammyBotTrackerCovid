using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Functions.CapturedCases.Business.Models
{
    public class PreviousCaseResult
    {
        public string DoctorAttended { get; set; }

        public DateTime DateAttention { get; set; }

        public string Observations { get; set; }

        public string Prescription { get; set; }

        public bool IsBrigade { get; set; }
    }
}
