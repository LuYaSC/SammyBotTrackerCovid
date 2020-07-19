using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Functions.CaseRecovery.Business.Models
{
    public class GetDataDto
    {
        public int CasoId { get; set; }

        public bool EnvioBrigada { get; set; }

        public bool Finalizar { get; set; }

        public string Observaciones { get; set; }

        public string DireccionExplicita { get; set; }
    }
}
