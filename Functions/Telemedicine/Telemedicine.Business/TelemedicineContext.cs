using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Data;
using TC.Core.Data.Context;

namespace TC.Functions.Telemedicine.Business
{
    public class TelemedicineContext : SBTCContext
    {
        public TelemedicineContext(string nameOrConnectionString = "SBTCContext") : base(nameOrConnectionString)
        {
        }

        public DbSet<P_Controles> Controles { get; set; }

        public DbSet<CasosAgenda> CasosAgendas { get; set; }

        public DbSet<CasosGrupoRescate> CasosGrupoRescates { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }
    }
}
