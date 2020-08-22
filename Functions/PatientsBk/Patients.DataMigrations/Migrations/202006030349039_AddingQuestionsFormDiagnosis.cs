namespace SBTC.Functions.Patients.DataMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingQuestionsFormDiagnosis : DbMigration
    {
        public override void Up()
        {
            AddColumn("tc.Pacientes", "Edad", c => c.Int(nullable: false));
            AddColumn("tc.FormDiagInicials", "RiesgoParaMedicosDescripcion", c => c.String(unicode: false, storeType: "text"));
            AddColumn("tc.FormDiagInicials", "EnfermedadesPadeceDescripcion", c => c.String(unicode: false, storeType: "text"));
            AddColumn("tc.FormDiagInicials", "MedicamentosPorEnfermedadDescripcion", c => c.String(unicode: false, storeType: "text"));
            AddColumn("tc.FormDiagInicials", "MedicinaNaturistaConsumidaDescripcion", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.Pacientes", "Matricula", c => c.String(maxLength: 20, unicode: false));
            AlterColumn("tc.Pacientes", "TipoDocumento", c => c.String(maxLength: 15, unicode: false));
            AlterColumn("tc.Pacientes", "Extension", c => c.String(maxLength: 3, fixedLength: true, unicode: false));
            AlterColumn("tc.Pacientes", "Nombre", c => c.String(maxLength: 40, unicode: false));
            AlterColumn("tc.Pacientes", "ApellidoPaterno", c => c.String(maxLength: 30, unicode: false));
            AlterColumn("tc.Pacientes", "Ocupacion", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("tc.Pacientes", "AreaLaboral", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.Pacientes", "Genero", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("tc.Pacientes", "Genero", c => c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false));
            AlterColumn("tc.Pacientes", "AreaLaboral", c => c.String(nullable: false, unicode: false, storeType: "text"));
            AlterColumn("tc.Pacientes", "Ocupacion", c => c.String(nullable: false, maxLength: 25, unicode: false));
            AlterColumn("tc.Pacientes", "ApellidoPaterno", c => c.String(nullable: false, maxLength: 30, unicode: false));
            AlterColumn("tc.Pacientes", "Nombre", c => c.String(nullable: false, maxLength: 40, unicode: false));
            AlterColumn("tc.Pacientes", "Extension", c => c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false));
            AlterColumn("tc.Pacientes", "TipoDocumento", c => c.String(nullable: false, maxLength: 15, unicode: false));
            AlterColumn("tc.Pacientes", "Matricula", c => c.String(nullable: false, maxLength: 20, unicode: false));
            DropColumn("tc.FormDiagInicials", "MedicinaNaturistaConsumidaDescripcion");
            DropColumn("tc.FormDiagInicials", "MedicamentosPorEnfermedadDescripcion");
            DropColumn("tc.FormDiagInicials", "EnfermedadesPadeceDescripcion");
            DropColumn("tc.FormDiagInicials", "RiesgoParaMedicosDescripcion");
            DropColumn("tc.Pacientes", "Edad");
        }
    }
}
