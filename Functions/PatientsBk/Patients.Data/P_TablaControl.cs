using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.Data
{
    public class P_TablaControl: Base
    {
        public int IdControl { get; set; }

        [ForeignKey("IdControl")]
        public virtual P_Controles P_Control { get; set; }

        public int IdFactor { get; set; }

        [ForeignKey("IdControl")]
        public virtual E_Factores E_Factores { get; set; }

        public bool FactorPresente { get; set; }

        public int? ValorFactor { get; set; }

        public string RespuestaAlternativa { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public bool Confirmado { get; set; }

        public DateTime? FechaConfirmacion { get; set; }
    }
}
