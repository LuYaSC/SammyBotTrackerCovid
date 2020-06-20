using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Data;
using TC.Core.Data.Context;

namespace TC.Functions.EvolutionForm.Business
{
    public class EvolutionFormContext : TeleCajaContext
    {
        public EvolutionFormContext(string nameOrConnectionString = "TeleCajaContext") : base(nameOrConnectionString)
        {
        }

        public DbSet<FormDiagInicials> FormDiagInicials { get; set; }

        public DbSet<FormEvolucion> FormEvolucions { get; set; }

        public DbSet<HistoriaClinicas> HistoriaClinicas { get; set; }

        public DbSet<Parameter> Parametros { get; set; }
    }
}
