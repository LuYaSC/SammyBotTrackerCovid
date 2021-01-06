using SBTC.Functions.Patients.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.Migration
{
    public class E_FactoresPatologicosMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<E_FactoresPatologicos>().Property(prop => prop.NombreCategoria).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<E_FactoresPatologicos>().Property(prop => prop.Descripcion).HasColumnType("varchar").HasMaxLength(500);
        }
    }
}
