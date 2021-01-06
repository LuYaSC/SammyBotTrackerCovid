using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data.Migration
{
    public class P_PacienteMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<P_Pacientes>().Property(prop => prop.NumeroContacto).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<P_Pacientes>().Property(prop => prop.CI).HasColumnType("varchar").HasMaxLength(20);
        }
    }
}
