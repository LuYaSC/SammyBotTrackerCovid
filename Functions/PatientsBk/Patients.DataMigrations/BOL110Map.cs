using SBTC.Functions.Patients.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.Migration
{
    public class BOL110Map
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BOL110>().Property(prop => prop.NroEscalafon).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            modelBuilder.Entity<BOL110>().Property(prop => prop.CI).HasColumnType("varchar").HasMaxLength(10).IsRequired();
        }
    }
}
