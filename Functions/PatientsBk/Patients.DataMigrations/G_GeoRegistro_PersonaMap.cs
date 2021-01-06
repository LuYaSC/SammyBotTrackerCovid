using SBTC.Functions.Patients.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.Migration
{
    public class G_GeoRegistro_PersonaMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<G_GeoRegistro_Personas>().Property(prop => prop.CI).HasColumnType("varchar").HasMaxLength(12).IsRequired();
        }
    }
}
