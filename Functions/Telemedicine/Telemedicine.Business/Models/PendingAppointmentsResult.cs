using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Functions.Telemedicine.Business.Models
{
    public class PendingAppointmentsResult
    {
        public int CasoId { get; set; }

        public string NumeroContacto { get; set; }

        public string NombreInterno { get; set; }

        public string NombreDoctor { get; set; }

        public string NombrePaciente { get; set; }

        public int DescripcionNivel { get; set; }

        public string HoraInicio { get; set; }

        public string HoraFin { get; set; }

        public string Finalizado { get; set; }

        public string Inconcluso { get; set; }

        public bool EnAtencion { get; set; }

        public string Observaciones { get; set; }

        public string RecetaMedica { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
