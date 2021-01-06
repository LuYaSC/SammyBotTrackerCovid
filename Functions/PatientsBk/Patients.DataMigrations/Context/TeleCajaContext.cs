namespace SBTC.Functions.Patients.Migrations.Context
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using SBTC.Functions.Patients.Data;
    using SBTC.Functions.Patients.Data.Attributes;
    using SBTC.Functions.Patients.DataMigrations;
    using SBTC.Functions.Patients.Migration;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Threading.Tasks;

    public class TeleCajaContext : IdentityDbContext
    {
        public TeleCajaContext() : base("TeleCajaContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("tc");
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            Precision.ConfigureModelBuilder(modelBuilder);
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
            F_FormularioDiagInicialMap.Map(modelBuilder);
            TipoRiesgoMedicoMap.Map(modelBuilder);
            TipoRiesgoLaboralMap.Map(modelBuilder);
            EnfermedadesPacienteMap.Map(modelBuilder);
            MedicamentosPacienteMap.Map(modelBuilder);
            TensionFamiliarPacienteMap.Map(modelBuilder);
            MedicamenteAutomedicacionPacienteMap.Map(modelBuilder);
            VitaminasPacienteMap.Map(modelBuilder);
            MedicinaNaturalPacienteMap.Map(modelBuilder);
            PacienteMap.Map(modelBuilder);
            HistoriaClinicaMap.Map(modelBuilder);
            FormDiagInicialMap.Map(modelBuilder);
            MedicoMap.Map(modelBuilder);
        }

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
        public DbSet<F_FormularioDiagInicial> F_FormularioDiagInicial { get; set; }
        public TipoRiesgoMedico TipoRiesgoMedico { get; set; }
        public TipoRiesgoLaboral TipoRiesgoLaboral { get; set; }
        public EnfermedadesPaciente EnfermedadesPaciente { get; set; }
        public MedicamentosPaciente MedicamentosPaciente { get; set; }
        public TensionFamiliarPaciente TensionFamiliarPaciente { get; set; }
        public MedicamenteAutomedicacionPaciente MedicamenteAutomedicacionPaciente { get; set; }
        public VitaminasPaciente VitaminasPaciente { get; set; }
        public MedicinaNaturalPaciente MedicinaNaturalPaciente { get; set; }
        public Pacientes Pacientes { get; set; }
        public HistoriaClinicas HistoriaClinicas { get; set; }
        public FormDiagInicials FormDiagInicials { get; set; }
        public Medicos Medicos { get; set; }
    }
}
