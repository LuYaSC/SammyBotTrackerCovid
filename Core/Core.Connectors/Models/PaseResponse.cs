using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Connectors.Models
{
    public class PaseResponse
    {
        public string CodigoAutorizacion { get; set; }

        public string CodigoRespuesta { get; set; }

        public string DetalleRespuesta { get; set; }

        public List<DetailsPase> Detalle { get; set; }

    }

    public class DetailsPase
    {
        public string CodigoAfiliado { get; set; }

        public string CodigoConsulta { get; set; }

        public string CodigoEmpresa { get; set; }

        public string CodigoErrorOriginal { get; set; }

        public string CodigoServicio { get; set; }

        public string ComisionCobrarBCP { get; set; }

        public string Cuenta { get; set; }

        public string DescripcionPagoRealizado { get; set; }

        public bool FLGAbonoenlinea { get; set; }

        public string FechaVencimientoSaldo { get; set; }

        public string FlagSFT { get; set; }

        public decimal ImporteAdeudado { get; set; }

        public decimal ImporteComisionMoraSaldo { get; set; }

        public decimal ImporteConsumoSaldo { get; set; }

        public decimal ImporteTotal { get; set; }

        public decimal ImporteVencidoSaldo { get; set; }

        public string IndComisionCuotas { get; set; }

        public string IndImporteFijo { get; set; }

        public string IndPagoMinimo { get; set; }

        public string IndicadorCuotasIntercaladas { get; set; }

        public string IndicadorPago { get; set; }

        public string MensajeError { get; set; }

        public string MonedaPagos { get; set; }

        public decimal Mora { get; set; }

        public string NombreCentral { get; set; }

        public string NombreCliente { get; set; }

        public string NombreEmpresa { get; set; }

        public int NumeroPagos { get; set; }

        public string RazonSocialEmpresa { get; set; }

        public string TipoImpresion { get; set; }

        public decimal TotalComisionesPagadas { get; set; }
        public decimal TotalConsumo { get; set; }

        public decimal TotalGeneral { get; set; }

        public decimal TotalImportesPagados { get; set; }

        public decimal TotalImpuesto { get; set; }

        public decimal TotalMorasPagadas { get; set; }

        public List<Payment> Pagos { get; set; }
    }

    public class Payment
    {
        public string FechaVencimiento { get; set; }

        public decimal ImporteComision { get; set; }

        public decimal ImporteConsumo { get; set; }

        public decimal ImportePagoMinimo { get; set; }

        public decimal ImporteVencidoMora { get; set; }

        public string MensajeAvisoUsuario { get; set; }

        public int NumeroCuota { get; set; }
    }
}