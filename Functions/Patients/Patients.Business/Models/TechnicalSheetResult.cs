using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Functions.Patients.Business.Models
{
    public class TechnicalSheetResult
    {
        public string Factor { get; set; }

        public string  CategoryName { get; set; }

        public string PresentFactor { get; set; }

        public string AlterResponse { get; set; }

        public DateTime RegisterDate { get; set; }

        public string Confirmed { get; set; }
    }
}
