namespace SBTC.Functions.Patients.DataMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableFormdiagnosisTexts : DbMigration
    {
        public override void Up()
        {
            AlterColumn("tc.Pacientes", "Direccion", c => c.String(nullable: false, unicode: false, storeType: "text"));
            AlterColumn("tc.Pacientes", "Celular", c => c.String(maxLength: 15, unicode: false));
            AlterColumn("tc.FormDiagInicials", "EnfermedadesPadece", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.FormDiagInicials", "MedicamentosPorEnfermedad", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.FormDiagInicials", "CigarrillosAlDia", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.FormDiagInicials", "HaceCuantoNoFuma", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.FormDiagInicials", "CantidadConsumoBebidas", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.FormDiagInicials", "EscalaTensionFamiliar", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.FormDiagInicials", "ComentariosTensionFamiliar", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.FormDiagInicials", "EstadoSaludActual", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.FormDiagInicials", "Temperatura", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.FormDiagInicials", "Pulso", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.FormDiagInicials", "FrecuenciaRespiratoria", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.FormDiagInicials", "MedicamentosConsumidos", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.FormDiagInicials", "VitaminasConsumidas", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.FormDiagInicials", "MedicinaNaturistaConsumida", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            AlterColumn("tc.FormDiagInicials", "MedicinaNaturistaConsumida", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("tc.FormDiagInicials", "VitaminasConsumidas", c => c.String(maxLength: 200, unicode: false));
            AlterColumn("tc.FormDiagInicials", "MedicamentosConsumidos", c => c.String(maxLength: 300, unicode: false));
            AlterColumn("tc.FormDiagInicials", "FrecuenciaRespiratoria", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("tc.FormDiagInicials", "Pulso", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("tc.FormDiagInicials", "Temperatura", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("tc.FormDiagInicials", "EstadoSaludActual", c => c.String(maxLength: 40, unicode: false));
            AlterColumn("tc.FormDiagInicials", "ComentariosTensionFamiliar", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("tc.FormDiagInicials", "EscalaTensionFamiliar", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("tc.FormDiagInicials", "CantidadConsumoBebidas", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("tc.FormDiagInicials", "HaceCuantoNoFuma", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("tc.FormDiagInicials", "CigarrillosAlDia", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("tc.FormDiagInicials", "MedicamentosPorEnfermedad", c => c.String(maxLength: 300, unicode: false));
            AlterColumn("tc.FormDiagInicials", "EnfermedadesPadece", c => c.String(maxLength: 350, unicode: false));
            AlterColumn("tc.Pacientes", "Celular", c => c.String(maxLength: 8, unicode: false));
            AlterColumn("tc.Pacientes", "Direccion", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
    }
}
