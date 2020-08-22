using SBTC.Functions.Patients.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.DataMigrations
{
    public class FormDiagInicialMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.RiesgoParaMedicos).HasColumnType("varchar").HasMaxLength(300);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.MedicoAsignado).HasColumnType("varchar").HasMaxLength(50);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.EstaInternado).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.EnfermedadesPadece).HasColumnType("text");
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.MedicamentosPorEnfermedad).HasColumnType("text");
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.EstaEmbarazada).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.DisponeDomicilio).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.DisponePersonaAyudaCama).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.DisponePersonaAyudaHablar).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.DisponeAyudaComida).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.DisponeAyudaLimpieza).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.Fuma).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.CigarrillosAlDia).HasColumnType("text");
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.HaceCuantoNoFuma).HasColumnType("text");
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.BebidasAlcoholicas).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.CantidadConsumoBebidas).HasColumnType("text");
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.Estupefacientes).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.Sedentarismo).HasColumnType("char").HasMaxLength(100);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.CarenciaEconomica).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.TensionFamiliar).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.EscalaTensionFamiliar).HasColumnType("text");
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.ComentariosTensionFamiliar).HasColumnType("text");
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.EstadoSaludActual).HasColumnType("text");
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.Tos).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.DolorGarganta).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.DolorCabeza).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.Fiebre).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.Temperatura).HasColumnType("text");
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.Pulso).HasColumnType("text");
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.DificultadRespirar).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.FrecuenciaRespiratoria).HasColumnType("text");
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.DificultadTerminarFrases).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.MedicamentosConsumidos).HasColumnType("text");
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.VitaminasConsumidas).HasColumnType("text");
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.UsoMedicinaNaturista).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.MedicinaNaturistaConsumida).HasColumnType("text");
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.DeseaRecibirSuero).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.DeseaDonarSangre).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.TipoSangre).HasColumnType("text");
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.PerdioPeso).HasColumnType("char").HasMaxLength(2);
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.EnfermedadesPadeceDescripcion).HasColumnType("text");
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.MedicamentosPorEnfermedadDescripcion).HasColumnType("text");
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.MedicinaNaturistaConsumidaDescripcion).HasColumnType("text");
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.RiesgoParaMedicosDescripcion).HasColumnType("text");
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.VitaminasConsumidasDescripcion).HasColumnType("text");
            modelBuilder.Entity<FormDiagInicials>().Property(prop => prop.medicamentosConsumidosDescripcion).HasColumnType("text");
        }
    }
}
