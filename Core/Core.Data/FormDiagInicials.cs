﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data
{
    public class FormDiagInicials : Base
    {
        public int IdHistoriaClinica { get; set; }

        [ForeignKey("IdHistoriaClinica")]
        public virtual HistoriaClinicas HistoriaClinicas { get; set; }

        public string MedicoAsignado { get; set; }

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

        public string medicamentosConsumidosDescripcion { get; set; }

        public string VitaminasConsumidas { get; set; }

        public string VitaminasConsumidasDescripcion { get; set; }

        public string UsoMedicinaNaturista { get; set; }

        public string MedicinaNaturistaConsumida { get; set; }

        public string MedicinaNaturistaConsumidaDescripcion { get; set; }

        public string DeseaRecibirSuero { get; set; }

        public string DeseaDonarSangre { get; set; }

        public string TipoSangre { get; set; }

        public string PerdioPeso { get; set; }
    }
}
