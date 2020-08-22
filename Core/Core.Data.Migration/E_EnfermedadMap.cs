using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data.Migration
{
    public class E_EnfermedadMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<E_Enfermedades>().Property(prop => prop.NombreEnfermedad).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<E_Enfermedades>().Property(prop => prop.NombreCientifico).HasColumnType("varchar").HasMaxLength(10).IsRequired();
            modelBuilder.Entity<E_Enfermedades>().Property(prop => prop.Descripcion).HasColumnType("varchar").HasMaxLength(500);
        }
    }
}
