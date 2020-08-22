using SBTC.Functions.Patients.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.Migration
{
    public class TipoRiesgoLaboralMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoRiesgoLaboral>().Property(prop => prop.Descripcion).HasColumnType("nvarchar").HasMaxLength(250).IsRequired();
            modelBuilder.Entity<TipoRiesgoLaboral>().Property(prop => prop.Descripcion).HasColumnType("nvarchar").HasMaxLength(500);
        }
    }
}
