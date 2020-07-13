using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data.Migration
{
    public class CasosAgendaMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CasosAgenda>().Property(prop => prop.NombrePaciente).HasColumnType("nvarchar").HasMaxLength(60);
            modelBuilder.Entity<CasosAgenda>().Property(prop => prop.HoraInicio).HasColumnType("nvarchar").HasMaxLength(10);
            modelBuilder.Entity<CasosAgenda>().Property(prop => prop.HoraFin).HasColumnType("nvarchar").HasMaxLength(10);
            modelBuilder.Entity<CasosAgenda>().Property(prop => prop.UrlSala).HasColumnType("text");
            modelBuilder.Entity<CasosAgenda>().Property(prop => prop.Observaciones).HasColumnType("text");
            modelBuilder.Entity<CasosAgenda>().Property(prop => prop.CodigoSala).HasColumnType("text");
        }
    }
}
