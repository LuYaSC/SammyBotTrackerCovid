﻿using SBTC.Functions.Patients.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.Migration
{
    public class P_TablaControlMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<P_TablaControl>().Property(prop => prop.RespuestaAlternativa).HasColumnType("text");
        }
    }
}
