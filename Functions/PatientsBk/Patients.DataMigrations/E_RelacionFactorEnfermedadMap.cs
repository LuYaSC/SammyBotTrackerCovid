using SBTC.Functions.Patients.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.Migration
{
    public class E_RelacionFactorEnfermedadMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<E_RelacionFactorEnfermedad>().Property(prop => prop.ScoreSeveridad).HasPrecision(10,2);
        }
    }
}
