using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Connectors.Models
{
    public class PasePayRequest
    {
        public string AplicacionFacturacion { get; set; } = "00000000000000000000";
        public string CiudadEmision { get; set; } = "La Paz";
        public string ClaveCanal { get; set; }
        public string CodEmpresa { get; set; }
        public List<RequestDetailPay> DetallePago { get; set; }
        public string Parameter { get; set; }
        public string MonedaTransaccion { get; set; }
        public decimal MontoTransaccion { get; set; }
        public string NumeroTerminal { get; set; }
        public string PIN { get; set; }
        public string PuntoServicio { get; set; }
        public string Trace { get; set; }
    }
    public class RequestDetailPay
    {
        public string CodigoServicio { get; set; }
        public string FormaPago { get; set; } = "WEB";
        public string CodigoSucursal { get; set; }
        public string CodigoAgencia { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public string TipoPago { get; set; } = "N";        
        public string PeriodoCotizacion { get; set; } = "000000";
        public string TipoDocumento { get; set; } = "02";
        public string NroDocumento { get; set; } = string.Empty;
        public int NumeroImportes { get; set; }
        public string NumeroCheque { get; set; } = string.Empty;
        public string NombreGirador { get; set; } = string.Empty;
        public string FechaGiro { get; set; } = string.Empty;
        public string CuentaDebitoPago { get; set; } = string.Empty;
        public string CIC { get; set; } = string.Empty;
        public string NombreDepositante { get; set; } = string.Empty;
        public string IDCDepositante { get; set; } = string.Empty;
        public string DireccionDepositante { get; set; } = string.Empty;
        public string TelefonoDepositante { get; set; } = string.Empty;
        public decimal ImportePago { get; set; }
        public string CodigoConsulta { get; set; } = string.Empty;
        public string CodigoAfiliado { get; set; } = string.Empty;      
        public List<RequestRubro> Rubros { get; set; }
    }

    public class RequestRubro
    {
        public string CodigoRubro { get; set; } = "07";
        public string FechaVencimiento { get; set; } = null;
        public decimal ImporteRubro { get; set; }
        public int NumeroCuota { get; set; }
    }
}


