using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Functions.Patients.Business.Models
{
    public class FormDiagDto
    {
        public string NumeroHistoria { get; set; }

        public string MedicoAsignado { get; set; }

        public string Matricula { get; set; }

        public int Documento { get; set; }

        public string TipoDocumento { get; set; }

        public string Extension { get; set; }

        public string Complemento { get; set; }

        public string Nombre { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string Ocupacion { get; set; }

        public string OcupacionDescripcion { get; set; }

        public string AreaLaboral { get; set; }

        public string Direccion { get; set; }

        public string Email { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public int Edad { get; set; }

        public string Genero { get; set; }

        public int Telefono { get; set; }

        public string Celular { get; set; }

        public int TimeStampCreacion { get; set; }

        public int TimeStampModificacion { get; set; }

        public string UsuarioCreacion { get; set; }

        public string UsuarioModificacion { get; set; }

        public string RiesgoParaMedicos { get; set; }

        public string RiesgoParaMedicosDescripcion { get; set; }

        public DateTime FechaTomaMuestra { get; set; }

        public string EstaInternado { get; set; }

        public string EnfermedadesPadece { get; set; }

        public string EnfermedadesPadeceDescripcion { get; set; }

        public string MedicamentosPorEnfermedad { get; set; }

        public string MedicamentosPorEnfermedadDescripcion { get; set; }

        public string EstaEmbarazada { get; set; }

        public string FechaUltimaMenstruacion { get; set; }

        public decimal Peso { get; set; }

        public decimal Talla { get; set; }

        public decimal IndiceMasaCorporal { get; set; }

        public string DisponeDomicilio { get; set; }

        public string DisponePersonaAyudaCama { get; set; }

        public string DisponePersonaAyudaHablar { get; set; }

        public string DisponeAyudaComida { get; set; }

        public string DisponeAyudaLimpieza { get; set; }

        public string Fuma { get; set; }

        public string CigarrillosAlDia { get; set; }

        public string HaceCuantoNoFuma { get; set; }

        public string BebidasAlcoholicas { get; set; }

        public string CantidadConsumoBebidas { get; set; }

        public string Estupefacientes { get; set; }

        public string Sedentarismo { get; set; }

        public string CarenciaEconomica { get; set; }

        public string TensionFamiliar { get; set; }

        public string EscalaTensionFamiliar { get; set; }

        public string ComentariosTensionFamiliar { get; set; }

        public string EstadoSaludActual { get; set; }

        public int RangoEstadoSalud { get; set; }

        public string Tos { get; set; }

        public string DolorGarganta { get; set; }

        public string DolorCabeza { get; set; }

        public string Fiebre { get; set; }

        public string Temperatura { get; set; }

        public string Pulso { get; set; }

        public string DificultadRespirar { get; set; }

        public string FrecuenciaRespiratoria { get; set; }

        public string DificultadTerminarFrases { get; set; }

        public string MedicamentosConsumidos { get; set; }

        public string MedicamentosConsumidosDescripcion { get; set; }

        public string VitaminasConsumidas { get; set; }

        public string VitaminasConsumidasDescripcion { get; set; }

        public string UsoMedicinaNaturista { get; set; }

        public string MedicinaNaturistaConsumida { get; set; }

        public string MedicinaNaturistaConsumidaDescripcion { get; set; }

        public string DeseaRecibirSuero { get; set; }

        public string DeseaDonarSangre { get; set; }

        public string TipoSangre { get; set; }

        public string PerdioPeso { get; set; }

        public string EmpresaTrabaja { get; set; }

        public DateTime FechaInicioSintomas { get; set; }

        public string ApoyoEmocional { get; set; }

        public string MetodoTratamientoUtilizado { get; set; }

        public string CansancioReposo { get; set; }

        public string EstadoActual { get; set; }

        public decimal DireccionLatitud { get; set; }

        public decimal DireccionLongitud { get; set; }
    }
}
