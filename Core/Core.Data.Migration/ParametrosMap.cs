using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data.Migration
{
    public class ParametrosMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parametros>().Property(prop => prop.NombreParametro).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Parametros>().Property(prop => prop.Valor).HasColumnType("varchar").HasMaxLength(50);
        }
    }
}
