using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Data;
using TC.Core.Data.Context;

namespace TC.Functions.Patients.Business
{
    public class PatientContext : TeleCajaContext
    {
        public PatientContext(string nameOrConnectionString = "TeleCajaContext") : base(nameOrConnectionString)
        {
        }

        public DbSet<FormDiagInicials> FormDiagInicials { get; set; }

        public DbSet<Pacientes> Pacientes { get; set; }

        //public DbSet<Medicos> Medicos { get; set; }

    }
}
