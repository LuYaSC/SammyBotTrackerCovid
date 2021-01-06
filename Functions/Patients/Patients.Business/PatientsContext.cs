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
    public class PatientsContext : SBTCContext
    {
        public PatientsContext(string nameOrConnectionString = "SBTCContext") : base(nameOrConnectionString)
        {
        }

        public DbSet<P_Controles> Controles { get; set; }

        public DbSet<P_Pacientes> Pacientes { get; set; }

        public DbSet<E_Factores> Factores { get; set; }

        public DbSet<E_FactoresPatologicos> FactorPatologico { get; set; }

        public DbSet<FormDiagInicials> formDiagInicials { get; set; }

        public DbSet<Pacientes> NPacientes { get; set; }
    }
}
