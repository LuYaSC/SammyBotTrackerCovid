using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data
{
    public class CapturedCase 
    {
        [Key, ForeignKey("CasosAgendas")]
        public int CaseId { get; set; }

        public string BornDate { get; set; }

        public bool IsInsurance { get; set; }

        public bool IsNewPatient { get; set; }

        public string InsuranceName { get; set; }

        public string Departament { get; set; }

        public string Municipality { get; set; }

        public string Origin { get; set; }

        public virtual CasosAgenda CasosAgendas { get; set; }
    }
}
