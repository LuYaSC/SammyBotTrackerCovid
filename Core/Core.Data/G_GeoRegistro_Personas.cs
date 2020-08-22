using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data
{
    public class G_GeoRegistro_Personas : Base
    {
        public string CI { get; set; }

        public float Latitud { get; set; }

        public float Longitud { get; set; }

        public DateTime FechaRegistro { get; set; }

        public bool QR { get; set; }
    }
}
