using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Functions.EvolutionForm.Business.Models
{
    public class GetBasicDatesFormResult
    {
        public int IdFormularioInicial { get; set; }

        public int IdUser { get; set; }

        public string NombreMedico { get; set; }

        public int Dia { get; set; }

        public string NumeroHistoria { get; set; }

        public string Paciente { get; set; }

        public string AntEstadoSalud { get; set; }

        public string AntDolorGarganta { get; set; }

        public string AntCuantoDolorGarganta { get; set; }

        public string AntDolorCabeza { get; set; }

        public string AntCuantoDolorCabeza { get; set; }

        public string AntTos { get; set; }

        public string AntFiebre { get; set; }

        public string AntTemperatura { get; set; }

        public string AntDificultadRespirar { get; set; }

        public string AntDificultadTerminarOraciones { get; set; }

        public string AntCansancio { get; set; }

        public bool RealizarPreguntaDonacion { get; set; }
    }
}
