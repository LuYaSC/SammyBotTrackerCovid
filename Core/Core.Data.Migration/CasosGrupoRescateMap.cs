using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data.Migration
{
    public class CasosGrupoRescateMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CasosGrupoRescate>().Property(prop => prop.Observaciones).HasColumnType("text");
            modelBuilder.Entity<CasosGrupoRescate>().Property(prop => prop.DireccionExplicita).HasColumnType("text");
        }
    }
}
