using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data.Migration
{
    public class NivelesAlertaMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NivelesAlertas>().Property(prop => prop.NivelAlerta).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            //modelBuilder.Entity<NivelesAlerta>().Property(prop => prop.PorcentajeMinimo).HasColumnType("varchar").HasPrecision(10, 2);
            //modelBuilder.Entity<NivelesAlerta>().Property(prop => prop.PorcentajeMaximo).HasColumnType("varchar").HasPrecision(10, 2);
        }
    }
}
