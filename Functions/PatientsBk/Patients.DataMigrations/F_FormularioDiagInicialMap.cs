using SBTC.Functions.Patients.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.Migration
{
    public class F_FormularioDiagInicialMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<F_FormularioDiagInicial>().Property(prop => prop.NombreSeccion).HasColumnType("text");
            modelBuilder.Entity<F_FormularioDiagInicial>().Property(prop => prop.Enunciado).HasColumnType("text");
            modelBuilder.Entity<F_FormularioDiagInicial>().Property(prop => prop.Opciones).HasColumnType("text");
            modelBuilder.Entity<F_FormularioDiagInicial>().Property(prop => prop.Tipo).HasColumnType("text");
        }
    }
}
