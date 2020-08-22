using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data
{
    public class CasosCaptados : Base
    {
        public int InternoId { get; set; }

        public string Observaciones { get; set; }

        public string NombrePaciente { get; set; }

        public string NumeroCelular { get; set; }

        public string RedSocial { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaFinalizacion { get; set; }

        [ForeignKey("InternoId")]
        public virtual User UserInterno { get; set; }
    }
}
