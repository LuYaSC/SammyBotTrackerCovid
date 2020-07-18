using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data
{
    public class CasosGrupoRescate : Base
    {
        public int CasoId { get; set; }

        [ForeignKey("CasoId")]
        public virtual CasosAgenda CasosAgenda { get; set; }

        public string DireccionExplicita { get; set; }

        public string Observaciones { get; set; }

        public DateTime FechaPriorizacion { get; set; }

        public DateTime? FechaAtencion { get; set; }
    }
}
