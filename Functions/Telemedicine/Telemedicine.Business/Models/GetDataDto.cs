using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Functions.Telemedicine.Business.Models
{
    public class GetDataDto
    {
        public int CasoId { get; set; }

        public int PacienteId { get; set; }

        public int DoctorId { get; set; }

        public int Nivel { get; set; }

        public string Celular { get; set; }

        public bool Finalizar { get; set; }

        public bool EnvioBrigada { get; set; }

        public string DireccionExplicita { get; set; }

        public string NombrePaciente { get; set; }

        public string Observaciones { get; set; }

        public string RecetaMedica { get; set; }
    }
}
