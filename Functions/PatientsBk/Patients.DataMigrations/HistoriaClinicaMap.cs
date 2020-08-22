using SBTC.Functions.Patients.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.DataMigrations
{
    public class HistoriaClinicaMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistoriaClinicas>().Property(prop => prop.NumeroHistoria).HasColumnType("varchar").HasMaxLength(25).IsRequired();
        }
    }
}
