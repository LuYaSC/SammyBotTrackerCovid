using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data
{
    public class P_Controles : Base
    {
        public int Id_Paciente { get; set; }

        [ForeignKey("Id_Paciente")]
        public virtual P_Pacientes P_Paciente { get; set; }

        public bool ControlFinalizado { get; set; }

        public bool ControlCancelado { get; set; }

        public DateTime FechaControl { get; set; }

        public bool Atendido { get; set; }

        public DateTime? FechaAtendido { get; set; }

        public string Observaciones { get; set; }

        public virtual List<P_TablaControl> TablaControl { get; set; }
    }
}
