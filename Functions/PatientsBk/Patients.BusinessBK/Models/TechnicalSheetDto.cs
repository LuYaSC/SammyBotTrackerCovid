using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.Business.Models
{
    public class TechnicalSheetDto
    {
        public int Id { get; set; }

        public string Observations { get; set; }

        public bool IsCancel { get; set; }
    }
}
