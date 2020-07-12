using SBTC.Functions.Patients.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.DataMigrations
{
    public class MedicoMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medicos>().Property(prop => prop.Nombre).HasColumnType("varchar").HasMaxLength(40).IsRequired();
            modelBuilder.Entity<Medicos>().Property(prop => prop.ApellidoPaterno).HasColumnType("varchar").HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Medicos>().Property(prop => prop.ApellidoMaterno).HasColumnType("varchar").HasMaxLength(30);
        }
    }
}
