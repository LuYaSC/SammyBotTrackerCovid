using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Functions.CaseRecovery.Business.Models
{
    public class GenerateRoomResult
    {
        public string Message { get; set; }

        public string Url { get; set; }

        public int CasoId { get; set; }

        public int? ControlId { get; set; }

        public bool CasoRetomado { get; set; }
    }
}
