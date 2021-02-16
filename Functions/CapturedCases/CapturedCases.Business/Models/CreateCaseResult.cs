using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Functions.CapturedCases.Business.Models
{
    public class CreateCaseResult
    {
        public int CaseId { get; set; }

        public string NamePatient { get; set; }

        public string BornDate { get; set; }

        public bool IsInsurance { get; set; }

        public bool IsNewPatient { get; set; }

        public string InsuranceName { get; set; }

        public string Departament { get; set; }

        public string Municipality { get; set; }

        public string UrlRoom { get; set; }

        public int LastControlId { get; set; }

        public List<PreviousAttention> PreviousAttentions { get; set; }
    }

    public class PreviousAttention
    {
        public int CaseId { get; set; }

        public string AssignedDoctor { get; set; }

        public DateTime DateAttention { get; set; }
    }

}
