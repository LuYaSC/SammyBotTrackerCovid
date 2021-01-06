using SBTC.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.Data
{
    public class E_Enfermedades : Base
    {
        public int Id_Grupo { get; set; }

        [ForeignKey("Id_Grupo")]
        public virtual E_GrupoEnfermedades E_GrupoEnfermedad { get; set; }

        public string NombreEnfermedad { get; set; }

        public string NombreCientifico { get; set; }

        public string Descripcion { get; set; }
    }
}
