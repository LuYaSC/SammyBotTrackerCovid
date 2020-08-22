namespace TC.BCP.CredinetWeb.Core.Data.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddinTablesforSammyBot : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "doctor.BOL110",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NroEscalafon = c.String(nullable: false, maxLength: 50, unicode: false),
                        CI = c.String(nullable: false, maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "doctor.Parametros",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreParametro = c.String(nullable: false, maxLength: 50, unicode: false),
                        Valor = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "doctor.E_Enfermedades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Grupo = c.Int(nullable: false),
                        NombreEnfermedad = c.String(nullable: false, maxLength: 50, unicode: false),
                        NombreCientifico = c.String(nullable: false, maxLength: 10, unicode: false),
                        Descripcion = c.String(maxLength: 500, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("doctor.E_GrupoEnfermedades", t => t.Id_Grupo)
                .Index(t => t.Id_Grupo);
            
            CreateTable(
                "doctor.E_GrupoEnfermedades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreGrupo = c.String(nullable: false, maxLength: 50, unicode: false),
                        Descripcion = c.String(nullable: false, maxLength: 500, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "doctor.E_Factores",
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
                .ForeignKey("doctor.E_FactoresPatologicos", t => t.IdCategoriaFactor)
                .Index(t => t.IdCategoriaFactor);
            
            CreateTable(
                "doctor.E_FactoresPatologicos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreCategoria = c.String(nullable: false, maxLength: 50, unicode: false),
                        Descripcion = c.String(maxLength: 500, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "doctor.E_RelacionFactorEnfermedad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdEnfermedad = c.Int(nullable: false),
                        IdFactor = c.Int(nullable: false),
                        ScoreSeveridad = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("doctor.E_Enfermedades", t => t.IdEnfermedad)
                .ForeignKey("doctor.E_Factores", t => t.IdFactor)
                .Index(t => t.IdEnfermedad)
                .Index(t => t.IdFactor);
            
            CreateTable(
                "doctor.FormDiagInicials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdHistoriaClinica = c.Int(nullable: false),
                        MedicoAsignado = c.String(),
                        RiesgoParaMedicos = c.String(),
                        RiesgoParaMedicosDescripcion = c.String(),
                        FechaTomaMuestra = c.DateTime(nullable: false),
                        EstaInternado = c.String(),
                        EnfermedadesPadece = c.String(),
                        EnfermedadesPadeceDescripcion = c.String(),
                        MedicamentosPorEnfermedad = c.String(),
                        MedicamentosPorEnfermedadDescripcion = c.String(),
                        EstaEmbarazada = c.String(),
                        FechaUltimaMenstruacion = c.String(),
                        Peso = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Talla = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IndiceMasaCorporal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DisponeDomicilio = c.String(),
                        DisponePersonaAyudaCama = c.String(),
                        DisponePersonaAyudaHablar = c.String(),
                        DisponeAyudaComida = c.String(),
                        DisponeAyudaLimpieza = c.String(),
                        Fuma = c.String(),
                        CigarrillosAlDia = c.String(),
                        HaceCuantoNoFuma = c.String(),
                        BebidasAlcoholicas = c.String(),
                        CantidadConsumoBebidas = c.String(),
                        Estupefacientes = c.String(),
                        Sedentarismo = c.String(),
                        CarenciaEconomica = c.String(),
                        TensionFamiliar = c.String(),
                        EscalaTensionFamiliar = c.String(),
                        ComentariosTensionFamiliar = c.String(),
                        EstadoSaludActual = c.String(),
                        RangoEstadoSalud = c.Int(nullable: false),
                        Tos = c.String(),
                        DolorGarganta = c.String(),
                        DolorCabeza = c.String(),
                        Fiebre = c.String(),
                        Temperatura = c.String(),
                        Pulso = c.String(),
                        DificultadRespirar = c.String(),
                        FrecuenciaRespiratoria = c.String(),
                        DificultadTerminarFrases = c.String(),
                        MedicamentosConsumidos = c.String(),
                        medicamentosConsumidosDescripcion = c.String(),
                        VitaminasConsumidas = c.String(),
                        VitaminasConsumidasDescripcion = c.String(),
                        UsoMedicinaNaturista = c.String(),
                        MedicinaNaturistaConsumida = c.String(),
                        MedicinaNaturistaConsumidaDescripcion = c.String(),
                        DeseaRecibirSuero = c.String(),
                        DeseaDonarSangre = c.String(),
                        TipoSangre = c.String(),
                        PerdioPeso = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("doctor.HistoriaClinicas", t => t.IdHistoriaClinica)
                .Index(t => t.IdHistoriaClinica);
            
            CreateTable(
                "doctor.HistoriaClinicas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroHistoria = c.String(),
                        IdPaciente = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("doctor.Pacientes", t => t.IdPaciente)
                .Index(t => t.IdPaciente);
            
            CreateTable(
                "doctor.Pacientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Matricula = c.String(),
                        Documento = c.Int(nullable: false),
                        TipoDocumento = c.String(),
                        Extension = c.String(),
                        Complemento = c.String(),
                        Nombre = c.String(),
                        ApellidoPaterno = c.String(),
                        ApellidoMaterno = c.String(),
                        Ocupacion = c.String(),
                        OcupacionDescripcion = c.String(),
                        AreaLaboral = c.String(),
                        EmpresaTrabaja = c.String(),
                        Direccion = c.String(),
                        Email = c.String(),
                        FechaNacimiento = c.DateTime(nullable: false),
                        Edad = c.Int(nullable: false),
                        Genero = c.String(),
                        Telefono = c.Int(nullable: false),
                        Celular = c.String(),
                        TimeStampCreacion = c.Int(nullable: false),
                        TimeStampModificacion = c.Int(nullable: false),
                        UsuarioCreacion = c.String(),
                        UsuarioModificacion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "doctor.G_GeoRegistro",
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
                .ForeignKey("doctor.G_TipoGeoreferenciacion", t => t.TipoGeo)
                .Index(t => t.TipoGeo);
            
            CreateTable(
                "doctor.G_TipoGeoreferenciacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoUbicacion = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "doctor.G_GeoRegistro_Personas",
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
                "doctor.NivelesAlertas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NivelAlerta = c.String(nullable: false, maxLength: 50, unicode: false),
                        PorcentajeMinimo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PorcentajeMaximo = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "doctor.P_Controles",
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
                .ForeignKey("doctor.P_Pacientes", t => t.Id_Paciente)
                .Index(t => t.Id_Paciente);
            
            CreateTable(
                "doctor.P_Pacientes",
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
                "doctor.P_TablaControl",
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
                .ForeignKey("doctor.E_Factores", t => t.IdControl)
                .ForeignKey("doctor.P_Controles", t => t.IdControl)
                .Index(t => t.IdControl);
            
        }
        
        public override void Down()
        {
            DropForeignKey("doctor.P_TablaControl", "IdControl", "doctor.P_Controles");
            DropForeignKey("doctor.P_TablaControl", "IdControl", "doctor.E_Factores");
            DropForeignKey("doctor.P_Controles", "Id_Paciente", "doctor.P_Pacientes");
            DropForeignKey("doctor.G_GeoRegistro", "TipoGeo", "doctor.G_TipoGeoreferenciacion");
            DropForeignKey("doctor.FormDiagInicials", "IdHistoriaClinica", "doctor.HistoriaClinicas");
            DropForeignKey("doctor.HistoriaClinicas", "IdPaciente", "doctor.Pacientes");
            DropForeignKey("doctor.E_RelacionFactorEnfermedad", "IdFactor", "doctor.E_Factores");
            DropForeignKey("doctor.E_RelacionFactorEnfermedad", "IdEnfermedad", "doctor.E_Enfermedades");
            DropForeignKey("doctor.E_Factores", "IdCategoriaFactor", "doctor.E_FactoresPatologicos");
            DropForeignKey("doctor.E_Enfermedades", "Id_Grupo", "doctor.E_GrupoEnfermedades");
            DropIndex("doctor.P_TablaControl", new[] { "IdControl" });
            DropIndex("doctor.P_Controles", new[] { "Id_Paciente" });
            DropIndex("doctor.G_GeoRegistro", new[] { "TipoGeo" });
            DropIndex("doctor.HistoriaClinicas", new[] { "IdPaciente" });
            DropIndex("doctor.FormDiagInicials", new[] { "IdHistoriaClinica" });
            DropIndex("doctor.E_RelacionFactorEnfermedad", new[] { "IdFactor" });
            DropIndex("doctor.E_RelacionFactorEnfermedad", new[] { "IdEnfermedad" });
            DropIndex("doctor.E_Factores", new[] { "IdCategoriaFactor" });
            DropIndex("doctor.E_Enfermedades", new[] { "Id_Grupo" });
            DropTable("doctor.P_TablaControl");
            DropTable("doctor.P_Pacientes");
            DropTable("doctor.P_Controles");
            DropTable("doctor.NivelesAlertas");
            DropTable("doctor.G_GeoRegistro_Personas");
            DropTable("doctor.G_TipoGeoreferenciacion");
            DropTable("doctor.G_GeoRegistro");
            DropTable("doctor.Pacientes");
            DropTable("doctor.HistoriaClinicas");
            DropTable("doctor.FormDiagInicials");
            DropTable("doctor.E_RelacionFactorEnfermedad");
            DropTable("doctor.E_FactoresPatologicos");
            DropTable("doctor.E_Factores");
            DropTable("doctor.E_GrupoEnfermedades");
            DropTable("doctor.E_Enfermedades");
            DropTable("doctor.Parametros");
            DropTable("doctor.BOL110");
        }
    }
}
