using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.Core.Data;
using TC.Core.Data.Context;

namespace CaseRecovery.Business
{
    public class CaseRecoveryContext : SBTCContext
    {
        public CaseRecoveryContext(string nameOrConnectionString = "SBTCContext") : base(nameOrConnectionString)
        {
        }

        public DbSet<P_Controles> Controles { get; set; }

        public DbSet<CasosAgenda> CasosAgendas { get; set; }

        public DbSet<CasosGrupoRescate> CasosGrupoRescates { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<CasosRecuperados> CasosRecuperados { get; set; }
    }
}
