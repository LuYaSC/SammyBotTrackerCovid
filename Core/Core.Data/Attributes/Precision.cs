﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC.Core.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Precision : Attribute
    {
        public byte precision { get; set; }
        public byte scale { get; set; }

        public Precision(byte precision, byte scale)
        {
            this.precision = precision;
            this.scale = scale;
        }

        public static void ConfigureModelBuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties().Where(x => x.GetCustomAttributes(false).OfType<Precision>().Any()).Configure(c => c.HasPrecision(c.ClrPropertyInfo.GetCustomAttributes(false).OfType<Precision>().First().precision, c.ClrPropertyInfo.GetCustomAttributes(false).OfType<Precision>().First().scale));
        }
    }
}
