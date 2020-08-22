namespace SBTC.Functions.Patients.DataMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialBd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "tc.BOL110",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NroEscalafon = c.String(nullable: false, maxLength: 50, unicode: false),
                        CI = c.String(nullable: false, maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tc.Parametros",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreParametro = c.String(nullable: false, maxLength: 50, unicode: false),
                        Valor = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tc.E_Enfermedades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Grupo = c.Int(nullable: false),
                        NombreEnfermedad = c.String(nullable: false, maxLength: 50, unicode: false),
                        NombreCientifico = c.String(nullable: false, maxLength: 10, unicode: false),
                        Descripcion = c.String(maxLength: 500, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("tc.E_GrupoEnfermedades", t => t.Id_Grupo)
                .Index(t => t.Id_Grupo);
            
            CreateTable(
                "tc.E_GrupoEnfermedades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreGrupo = c.String(nullable: false, maxLength: 50, unicode: false),
                        Descripcion = c.String(nullable: false, maxLength: 500, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tc.E_Factores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdCategoriaFactor = c.Int(nullable: false),
                        Factor = c.String(nullable: false, maxLength: 50, unicode: false),
                        Descripcion = c.String(maxLength: 500),
                        Cunatificable = c.Boolean(nullable: false),
                        RespuestaAbierta = c.Boolean(nullable: false),
                        PreguntaTest = c.String(maxLength: 500),
                        NivelImportancia = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("tc.E_FactoresPatologicos", t => t.IdCategoriaFactor)
                .Index(t => t.IdCategoriaFactor);
            
            CreateTable(
                "tc.E_FactoresPatologicos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreCategoria = c.String(nullable: false, maxLength: 50, unicode: false),
                        Descripcion = c.String(maxLength: 500, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tc.E_RelacionFactorEnfermedad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdEnfermedad = c.Int(nullable: false),
                        IdFactor = c.Int(nullable: false),
                        ScoreSeveridad = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("tc.E_Enfermedades", t => t.IdEnfermedad)
                .ForeignKey("tc.E_Factores", t => t.IdFactor)
                .Index(t => t.IdEnfermedad)
                .Index(t => t.IdFactor);
            
            CreateTable(
                "tc.F_FormularioDiagInicial",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroSeccion = c.Int(nullable: false),
                        NombreSeccion = c.String(unicode: false, storeType: "text"),
                        Enunciado = c.String(unicode: false, storeType: "text"),
                        Tipo = c.String(unicode: false, storeType: "text"),
                        Opciones = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tc.G_GeoRegistro",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoGeo = c.Int(nullable: false),
                        IdControl = c.Int(nullable: false),
                        Latitud = c.Single(nullable: false),
                        Longitud = c.Single(nullable: false),
                        FechaRegistro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("tc.G_TipoGeoreferenciacion", t => t.TipoGeo)
                .Index(t => t.TipoGeo);
            
            CreateTable(
                "tc.G_TipoGeoreferenciacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoUbicacion = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tc.G_GeoRegistro_Personas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CI = c.String(nullable: false, maxLength: 12, unicode: false),
                        Latitud = c.Single(nullable: false),
                        Longitud = c.Single(nullable: false),
                        FechaRegistro = c.DateTime(nullable: false),
                        QR = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tc.NivelesAlertas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NivelAlerta = c.String(nullable: false, maxLength: 50, unicode: false),
                        PorcentajeMinimo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PorcentajeMaximo = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tc.P_Controles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Paciente = c.Int(nullable: false),
                        ControlFinalizado = c.Boolean(nullable: false),
                        ControlCancelado = c.Boolean(nullable: false),
                        FechaControl = c.DateTime(nullable: false),
                        Atendido = c.Boolean(nullable: false),
                        FechaAtendido = c.DateTime(),
                        Observaciones = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("tc.P_Pacientes", t => t.Id_Paciente)
                .Index(t => t.Id_Paciente);
            
            CreateTable(
                "tc.P_Pacientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroContacto = c.String(nullable: false, maxLength: 50, unicode: false),
                        CI = c.String(maxLength: 20, unicode: false),
                        UsuarioWpp = c.Int(nullable: false),
                        FechaRegistro = c.DateTime(nullable: false),
                        FechaActualizacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tc.P_TablaControl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdControl = c.Int(nullable: false),
                        IdFactor = c.Int(nullable: false),
                        FactorPresente = c.Boolean(nullable: false),
                        ValorFactor = c.Int(),
                        RespuestaAlternativa = c.String(unicode: false, storeType: "text"),
                        FechaRegistro = c.DateTime(),
                        Confirmado = c.Boolean(nullable: false),
                        FechaConfirmacion = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("tc.E_Factores", t => t.IdControl)
                .ForeignKey("tc.P_Controles", t => t.IdControl)
                .Index(t => t.IdControl);
            
            CreateTable(
                "tc.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "tc.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("tc.AspNetRoles", t => t.RoleId)
                .ForeignKey("tc.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "tc.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "tc.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("tc.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "tc.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("tc.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "tc.TipoRiesgoMedicoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 250),
                        Observaciones = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tc.TipoRiesgoLaborals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 500),
                        Observaciones = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tc.EnfermedadesPacientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 250),
                        Observaciones = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tc.MedicamentosPacientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 250),
                        Observaciones = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tc.TensionFamiliarPacientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 250),
                        Observaciones = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tc.MedicamenteAutomedicacionPacientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 250),
                        Observaciones = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tc.VitaminasPacientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 250),
                        Observaciones = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tc.MedicinaNaturalPacientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 250),
                        Observaciones = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tc.Pacientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Matricula = c.String(nullable: false, maxLength: 20, unicode: false),
                        Documento = c.Int(nullable: false),
                        TipoDocumento = c.String(nullable: false, maxLength: 15, unicode: false),
                        Extension = c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false),
                        Complemento = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        Nombre = c.String(nullable: false, maxLength: 40, unicode: false),
                        ApellidoPaterno = c.String(nullable: false, maxLength: 30, unicode: false),
                        ApellidoMaterno = c.String(maxLength: 30, unicode: false),
                        Ocupacion = c.String(nullable: false, maxLength: 25, unicode: false),
                        AreaLaboral = c.String(nullable: false, unicode: false, storeType: "text"),
                        Direccion = c.String(nullable: false, maxLength: 100, unicode: false),
                        Email = c.String(maxLength: 40, unicode: false),
                        FechaNacimiento = c.DateTime(nullable: false),
                        Sexo = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        Telefono = c.Int(nullable: false),
                        Celular = c.String(maxLength: 8, unicode: false),
                        TimeStampCreacion = c.Int(nullable: false),
                        TimeStampModificacion = c.Int(nullable: false),
                        UsuarioCreacion = c.String(maxLength: 15, fixedLength: true, unicode: false),
                        UsuarioModificacion = c.String(maxLength: 15, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "tc.HistoriaClinicas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroHistoria = c.String(nullable: false, maxLength: 25, unicode: false),
                        IdPaciente = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("tc.Pacientes", t => t.IdPaciente)
                .Index(t => t.IdPaciente);
            
            CreateTable(
                "tc.FormDiagInicials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdHistoriaClinica = c.Int(nullable: false),
                        MedicoAsignado = c.String(),
                        RiesgoParaMedicos = c.String(maxLength: 300, unicode: false),
                        FechaTomaMuestra = c.DateTime(nullable: false),
                        EstaInternado = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        EnfermedadesPadece = c.String(maxLength: 350, unicode: false),
                        MedicamentosPorEnfermedad = c.String(maxLength: 300, unicode: false),
                        EstaEmbarazada = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        FechaUltimaMenstruacion = c.String(),
                        Peso = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Talla = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IndiceMasaCorporal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DisponeDomicilio = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        DisponePersonaAyudaCama = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        DisponePersonaAyudaHablar = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        DisponeAyudaComida = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        DisponeAyudaLimpieza = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        Fuma = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        CigarrillosAlDia = c.String(maxLength: 100, unicode: false),
                        HaceCuantoNoFuma = c.String(maxLength: 100, unicode: false),
                        BebidasAlcoholicas = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        CantidadConsumoBebidas = c.String(maxLength: 100, unicode: false),
                        Estupefacientes = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        Sedentarismo = c.String(maxLength: 100, fixedLength: true, unicode: false),
                        CarenciaEconomica = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        TensionFamiliar = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        EscalaTensionFamiliar = c.String(maxLength: 100, unicode: false),
                        ComentariosTensionFamiliar = c.String(maxLength: 100, unicode: false),
                        EstadoSaludActual = c.String(maxLength: 40, unicode: false),
                        RangoEstadoSalud = c.Int(nullable: false),
                        Tos = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        DolorGarganta = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        DolorCabeza = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        Fiebre = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        Temperatura = c.String(maxLength: 100, unicode: false),
                        Pulso = c.String(maxLength: 100, unicode: false),
                        DificultadRespirar = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        FrecuenciaRespiratoria = c.String(maxLength: 100, unicode: false),
                        DificultadTerminarFrases = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        MedicamentosConsumidos = c.String(maxLength: 300, unicode: false),
                        VitaminasConsumidas = c.String(maxLength: 200, unicode: false),
                        UsoMedicinaNaturista = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        MedicinaNaturistaConsumida = c.String(maxLength: 100, unicode: false),
                        DeseaRecibirSuero = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        DeseaDonarSangre = c.String(maxLength: 2, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("tc.HistoriaClinicas", t => t.IdHistoriaClinica)
                .Index(t => t.IdHistoriaClinica);
            
            CreateTable(
                "tc.Medicos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 40, unicode: false),
                        ApellidoPaterno = c.String(nullable: false, maxLength: 30, unicode: false),
                        ApellidoMaterno = c.String(maxLength: 30, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("tc.FormDiagInicials", "IdHistoriaClinica", "tc.HistoriaClinicas");
            DropForeignKey("tc.HistoriaClinicas", "IdPaciente", "tc.Pacientes");
            DropForeignKey("tc.AspNetUserRoles", "UserId", "tc.AspNetUsers");
            DropForeignKey("tc.AspNetUserLogins", "UserId", "tc.AspNetUsers");
            DropForeignKey("tc.AspNetUserClaims", "UserId", "tc.AspNetUsers");
            DropForeignKey("tc.AspNetUserRoles", "RoleId", "tc.AspNetRoles");
            DropForeignKey("tc.P_TablaControl", "IdControl", "tc.P_Controles");
            DropForeignKey("tc.P_TablaControl", "IdControl", "tc.E_Factores");
            DropForeignKey("tc.P_Controles", "Id_Paciente", "tc.P_Pacientes");
            DropForeignKey("tc.G_GeoRegistro", "TipoGeo", "tc.G_TipoGeoreferenciacion");
            DropForeignKey("tc.E_RelacionFactorEnfermedad", "IdFactor", "tc.E_Factores");
            DropForeignKey("tc.E_RelacionFactorEnfermedad", "IdEnfermedad", "tc.E_Enfermedades");
            DropForeignKey("tc.E_Factores", "IdCategoriaFactor", "tc.E_FactoresPatologicos");
            DropForeignKey("tc.E_Enfermedades", "Id_Grupo", "tc.E_GrupoEnfermedades");
            DropIndex("tc.FormDiagInicials", new[] { "IdHistoriaClinica" });
            DropIndex("tc.HistoriaClinicas", new[] { "IdPaciente" });
            DropIndex("tc.AspNetUserLogins", new[] { "UserId" });
            DropIndex("tc.AspNetUserClaims", new[] { "UserId" });
            DropIndex("tc.AspNetUsers", "UserNameIndex");
            DropIndex("tc.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("tc.AspNetUserRoles", new[] { "UserId" });
            DropIndex("tc.AspNetRoles", "RoleNameIndex");
            DropIndex("tc.P_TablaControl", new[] { "IdControl" });
            DropIndex("tc.P_Controles", new[] { "Id_Paciente" });
            DropIndex("tc.G_GeoRegistro", new[] { "TipoGeo" });
            DropIndex("tc.E_RelacionFactorEnfermedad", new[] { "IdFactor" });
            DropIndex("tc.E_RelacionFactorEnfermedad", new[] { "IdEnfermedad" });
            DropIndex("tc.E_Factores", new[] { "IdCategoriaFactor" });
            DropIndex("tc.E_Enfermedades", new[] { "Id_Grupo" });
            DropTable("tc.Medicos");
            DropTable("tc.FormDiagInicials");
            DropTable("tc.HistoriaClinicas");
            DropTable("tc.Pacientes");
            DropTable("tc.MedicinaNaturalPacientes");
            DropTable("tc.VitaminasPacientes");
            DropTable("tc.MedicamenteAutomedicacionPacientes");
            DropTable("tc.TensionFamiliarPacientes");
            DropTable("tc.MedicamentosPacientes");
            DropTable("tc.EnfermedadesPacientes");
            DropTable("tc.TipoRiesgoLaborals");
            DropTable("tc.TipoRiesgoMedicoes");
            DropTable("tc.AspNetUserLogins");
            DropTable("tc.AspNetUserClaims");
            DropTable("tc.AspNetUsers");
            DropTable("tc.AspNetUserRoles");
            DropTable("tc.AspNetRoles");
            DropTable("tc.P_TablaControl");
            DropTable("tc.P_Pacientes");
            DropTable("tc.P_Controles");
            DropTable("tc.NivelesAlertas");
            DropTable("tc.G_GeoRegistro_Personas");
            DropTable("tc.G_TipoGeoreferenciacion");
            DropTable("tc.G_GeoRegistro");
            DropTable("tc.F_FormularioDiagInicial");
            DropTable("tc.E_RelacionFactorEnfermedad");
            DropTable("tc.E_FactoresPatologicos");
            DropTable("tc.E_Factores");
            DropTable("tc.E_GrupoEnfermedades");
            DropTable("tc.E_Enfermedades");
            DropTable("tc.Parametros");
            DropTable("tc.BOL110");
        }
    }
}
