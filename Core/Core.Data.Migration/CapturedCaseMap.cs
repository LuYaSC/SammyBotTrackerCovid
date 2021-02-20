using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data.Migration
{
    public class CapturedCaseMap
    {
        public static void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CapturedCase>().Property(prop => prop.InsuranceName).HasColumnType("nvarchar").HasMaxLength(60);
            modelBuilder.Entity<CapturedCase>().Property(prop => prop.BornDate).HasColumnType("nvarchar").HasMaxLength(10);
            modelBuilder.Entity<CapturedCase>().Property(prop => prop.Departament).HasColumnType("nvarchar").HasMaxLength(30);
            modelBuilder.Entity<CapturedCase>().Property(prop => prop.Municipality).HasColumnType("nvarchar").HasMaxLength(30);
        }
    }
}
