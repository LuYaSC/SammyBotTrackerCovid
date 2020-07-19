using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data
{
    public class CasosRecuperados : Base
    {
        public int CasoId { get; set; }

        public int InternoId { get; set; }

        public string Observaciones { get; set; }

        public string RecetaMedica { get; set; }

        public string Url { get; set; }

        public string CodigoSala { get; set; }

        public bool Finalizado { get; set; }

        public bool Inconcluso { get; set; }

        public bool EnvioBrigada { get; set; }

        public DateTime FechaAtencion { get; set; }

        public DateTime? FechaFinalizacion { get; set; }

        [ForeignKey("CasoId")]
        public virtual CasosAgenda CasosAgenda { get; set; }

        [ForeignKey("InternoId")]
        public virtual User UserInterno { get; set; }

    }
}
