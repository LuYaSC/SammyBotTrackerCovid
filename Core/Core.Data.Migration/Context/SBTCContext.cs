namespace TC.Core.Data.Migrations.Context
{
    using TC.Core.Data.Attributes;
    using TC.Core.Data.Migration;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class SBTCContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public SBTCContext() : base("SBTCContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("doctor");
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<UserRole>().HasKey(m => new { m.RoleId, m.UserId }).ToTable("UserRoles");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().Property(m => m.UserName).HasMaxLength(16).IsFixedLength();
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<User>().Property(m => m.PhoneNumber).HasMaxLength(12).IsFixedLength();
            modelBuilder.Entity<User>().Property(m => m.Email).HasMaxLength(50).IsFixedLength();
            Precision.ConfigureModelBuilder(modelBuilder);
            UserDetailMap.Map(modelBuilder);
            BOL110Map.Map(modelBuilder);
            E_EnfermedadMap.Map(modelBuilder);
            E_FactorMap.Map(modelBuilder);
            E_FactoresPatologicosMap.Map(modelBuilder);
            E_GrupoEnfermedadMap.Map(modelBuilder);
            E_RelacionFactorEnfermedadMap.Map(modelBuilder);
            G_GeoRegistroMap.Map(modelBuilder);
            G_GeoRegistro_PersonaMap.Map(modelBuilder);
            G_TipoGeoreferenciacionMap.Map(modelBuilder);
            NivelesAlertaMap.Map(modelBuilder);
            P_ControlMap.Map(modelBuilder);
            P_PacienteMap.Map(modelBuilder);
            P_TablaControlMap.Map(modelBuilder);
            ParametrosMap.Map(modelBuilder);
            CasosAgendaMap.Map(modelBuilder);
            CasosGrupoRescateMap.Map(modelBuilder);
            CasosRecuperadosMap.Map(modelBuilder);
            CasosCaptadosMap.Map(modelBuilder);
            SendNotificationMap.Map(modelBuilder);
        }

        public DbSet<Audience> Audiences { get; set; }
        public DbSet<Audit> Audit { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AuditGroup> AuditGroups { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserCardHistory> UserCardsHistory { get; set; }
        public DbSet<BOL110> BOL110 { get; set; }
        public DbSet<E_Enfermedades> E_Enfermedades { get; set; }
        public DbSet<E_Factores> E_Factores { get; set; }
        public DbSet<E_FactoresPatologicos> E_FactoresPatologicos { get; set; }
        public DbSet<E_GrupoEnfermedades> E_GrupoEnfermedades { get; set; }
        public DbSet<E_RelacionFactorEnfermedad> E_RelacionFactorEnfermedad { get; set; }
        public DbSet<G_GeoRegistro> G_GeoRegistro { get; set; }
        public DbSet<G_GeoRegistro_Personas> G_GeoRegistro_Personas { get; set; }
        public DbSet<G_TipoGeoreferenciacion> G_TipoGeoreferenciacion { get; set; }
        public DbSet<NivelesAlertas> NivelesAlerta { get; set; }
        public DbSet<P_Controles> P_Controles { get; set; }
        public DbSet<P_Pacientes> P_Pacientes { get; set; }
        public DbSet<P_TablaControl> P_TablaControl { get; set; }
        public DbSet<Parametros> CasesByCountries { get; set; }
        public DbSet<Pacientes> Pacientes { get; set; }
        public DbSet<HistoriaClinicas> HistoriaClinicas { get; set; }
        public DbSet<FormDiagInicials> FormDiagInicials { get; set; }
        public DbSet<CasosAgenda> CasosAgenda { get; set; }
        public DbSet<CasosGrupoRescate> CasosRescateGrupos { get; set; }
        public DbSet<CasosRecuperados> CasosRecuperados { get; set; }
        public DbSet<CasosCaptados> CasosCaptados { get; set; }
        public DbSet<SendNotification> SendNotifications { get; set; }
    }
}
