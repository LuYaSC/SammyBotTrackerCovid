using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data.Migration
{
    public class G_TipoGeoreferenciacionMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<G_TipoGeoreferenciacion>().Property(prop => prop.TipoUbicacion).HasColumnType("varchar").HasMaxLength(50).IsRequired();
        }
    }
}
