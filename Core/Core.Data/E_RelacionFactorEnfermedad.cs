using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data
{
    public class E_RelacionFactorEnfermedad : Base
    {
        public int IdEnfermedad { get; set; }

        [ForeignKey("IdEnfermedad")]
        public virtual E_Enfermedades E_Enfermedad { get; set; }

        public int IdFactor { get; set; }

        [ForeignKey("IdFactor")]
        public virtual E_Factores E_Factor { get; set; }

        public decimal ScoreSeveridad { get; set; }
    }
}
