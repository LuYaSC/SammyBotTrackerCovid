using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data
{
    public class G_GeoRegistro : Base
    {
        public int TipoGeo { get; set; }

        [ForeignKey("TipoGeo")]
        public virtual G_TipoGeoreferenciacion G_TipoGeoreferenciacion { get; set; }

        public int IdControl { get; set; }

        public float Latitud { get; set; }

        public float Longitud { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
