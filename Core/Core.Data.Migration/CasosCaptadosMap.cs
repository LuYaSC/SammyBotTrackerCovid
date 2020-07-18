using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data.Migration
{
    public class CasosCaptadosMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CasosCaptados>().Property(prop => prop.NombrePaciente).HasColumnType("nvarchar").HasMaxLength(60);
            modelBuilder.Entity<CasosCaptados>().Property(prop => prop.Observaciones).HasColumnType("text");
            modelBuilder.Entity<CasosCaptados>().Property(prop => prop.NumeroCelular).HasColumnType("nvarchar").HasMaxLength(15);
            modelBuilder.Entity<CasosCaptados>().Property(prop => prop.RedSocial).HasColumnType("nvarchar").HasMaxLength(30);
        }
    }
}
