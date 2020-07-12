using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.Data
{
    public class E_Factores : Base
    {
        public int IdCategoriaFactor { get; set; }

        [ForeignKey("IdCategoriaFactor")]
        public virtual E_FactoresPatologicos E_FactoresPatologicos { get; set; }

        public string Factor { get; set; }

        public string Descripcion { get; set; }

        public bool Cunatificable { get; set; }

        public bool RespuestaAbierta { get; set; }

        public string PreguntaTest { get; set; }

        public Int16 NivelImportancia { get; set; }
    }
}
