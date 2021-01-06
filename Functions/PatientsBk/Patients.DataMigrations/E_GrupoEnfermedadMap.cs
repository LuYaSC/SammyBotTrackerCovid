using SBTC.Functions.Patients.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.Migration
{
    public class E_GrupoEnfermedadMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<E_GrupoEnfermedades>().Property(prop => prop.NombreGrupo).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<E_GrupoEnfermedades>().Property(prop => prop.Descripcion).HasColumnType("varchar").HasMaxLength(500).IsRequired();
        }
    }
}
