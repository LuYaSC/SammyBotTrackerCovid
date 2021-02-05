using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Business;

namespace TC.Functions.CaseRecovery.Business.Models
{
    public class GetDataDto : IPagination
    {
        public int CasoId { get; set; }

        public bool EnvioBrigada { get; set; }

        public bool Finalizar { get; set; }

        public string Observaciones { get; set; }

        public string DireccionExplicita { get; set; }

        public int Nivel { get; set; }

        public string RecetaMedica { get; set; }

        public string NombrePaciente { get; set; }

        public int PageSize { get; set; }

        public int CurPage { get; set; }
    }
}
