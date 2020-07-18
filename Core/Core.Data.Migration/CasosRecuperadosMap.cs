using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data.Migration
{
    public class CasosRecuperadosMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CasosRecuperados>().Property(prop => prop.Observaciones).HasColumnType("text");
        }
    }
}
