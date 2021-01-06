﻿using SBTC.Functions.Patients.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBTC.Functions.Patients.Migration
{
    public class TensionFamiliarPacienteMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TensionFamiliarPaciente>().Property(prop => prop.Descripcion).HasColumnType("nvarchar").HasMaxLength(250).IsRequired();
            modelBuilder.Entity<TensionFamiliarPaciente>().Property(prop => prop.Observaciones).HasColumnType("nvarchar").HasMaxLength(500);
        }
    }
}
