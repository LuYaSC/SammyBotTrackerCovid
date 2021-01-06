using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data
{
    public class HistoriaClinicas : Base
    {
        public string NumeroHistoria { get; set; }

        //public int IdMedico { get; set; }

        //[ForeignKey("IdMedico")]
        //public virtual Medicos Medicos { get; set; }

        public int IdPaciente { get; set; }

        [ForeignKey("IdPaciente")]
        public virtual Pacientes Paciente { get; set; }
    }
}
