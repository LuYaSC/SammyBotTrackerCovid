using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data.Migration
{
    public class E_FactorMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<E_Factores>().Property(prop => prop.Factor).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<E_Factores>().Property(prop => prop.Descripcion).HasColumnType("nvarchar").HasMaxLength(500);
            modelBuilder.Entity<E_Factores>().Property(prop => prop.PreguntaTest).HasColumnType("nvarchar").HasMaxLength(500);
            modelBuilder.Entity<E_Factores>().Property(prop => prop.NivelImportancia).HasColumnType("SMALLINT");
        }
    }
}
