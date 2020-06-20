using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Functions.EvolutionForm.Business.Models
{
    public class SaveFormDto
    {
        public int IdFormularioInicial { get; set; }

        public int Dia { get; set; }

        public string EstadoActual { get; set; }

        public bool Cerrado { get; set; }

        public string AyudaExterna { get; set; }

        public string AyudaExternaDescripcion { get; set; }

        public int DescripcionSalud { get; set; }

        public string DolorGarganta { get; set; }

        public int CuantoDolorGarganta { get; set; }

        public string DolorCabeza { get; set; }

        public int CuantoDolorCabeza { get; set; }

        public string Tos { get; set; }

        public string Fiebre { get; set; }

        public string CuantaTemperatura { get; set; }

        public string DificultadRespirar { get; set; }

        public string DificultadTerminarOraciones { get; set; }

        public string SienteCansancio { get; set; }

        public string RecojoMedicamentos { get; set; }

        public string RecojoMedicamentosDescripcion { get; set; }

        public string TomaMedicamentos { get; set; }

        public string TomaMedicamentosDescripcion { get; set; }

        public string RecibirSuero { get; set; }

        public string DonarSangre { get; set; }

        public string Comentarios { get; set; }

        public string Alta { get; set; }

        public bool IsDeleted { get; set; }

        public string AyudaExternaComentarios { get; set; }
    }
}
