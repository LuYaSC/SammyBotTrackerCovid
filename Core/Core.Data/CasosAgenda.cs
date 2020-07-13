using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data
{
    public class CasosAgenda : Base
    {
        public int PacienteId { get; set; }

        public int InternoId { get; set; }

        public int DoctorId { get; set; }

        public int DescripcionNivel { get; set; }

        public string NombrePaciente { get; set; }

        public string HoraInicio { get; set; }

        public string HoraFin { get; set; }

        public string UrlSala { get; set; }

        public string CodigoSala { get; set; }

        public bool Finalizado { get; set; }

        public bool Inconcluso { get; set; }

        public string Observaciones { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [ForeignKey("PacienteId")]
        public virtual P_Pacientes P_Pacientes { get; set; }

        [ForeignKey("InternoId")]
        public virtual User UserInterno { get; set; }

        [ForeignKey("DoctorId")]
        public virtual User UserDoctor { get; set; }
    }
}
