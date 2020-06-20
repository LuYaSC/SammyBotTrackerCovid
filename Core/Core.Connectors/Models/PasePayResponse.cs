using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Connectors.Models
{
    public class PasePayResponse
    {
        public string CodigoAutorizacion { get; set; }

        public string CodigoRespuesta { get; set; }

        public string DetalleRespuesta { get; set; }

        public string Facturas { get; set; }

        public string MonedaTransaccion { get; set; }

        public string MontoTransaccion { get; set; }

        public string PDFFacturacion { get; set; }

        public List<Details> Detalle { get; set; }
    }

    public class Details
    {

    }
}
