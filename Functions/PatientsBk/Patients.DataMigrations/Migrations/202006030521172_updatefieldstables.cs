namespace SBTC.Functions.Patients.DataMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatefieldstables : DbMigration
    {
        public override void Up()
        {
            AddColumn("tc.Pacientes", "OcupacionDescripcion", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.Pacientes", "Matricula", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.Pacientes", "TipoDocumento", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.Pacientes", "Nombre", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.Pacientes", "ApellidoPaterno", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.Pacientes", "ApellidoMaterno", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.Pacientes", "Ocupacion", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.Pacientes", "Direccion", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.Pacientes", "Email", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("tc.Pacientes", "Celular", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            AlterColumn("tc.Pacientes", "Celular", c => c.String(maxLength: 15, unicode: false));
            AlterColumn("tc.Pacientes", "Email", c => c.String(maxLength: 40, unicode: false));
            AlterColumn("tc.Pacientes", "Direccion", c => c.String(nullable: false, unicode: false, storeType: "text"));
            AlterColumn("tc.Pacientes", "Ocupacion", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("tc.Pacientes", "ApellidoMaterno", c => c.String(maxLength: 30, unicode: false));
            AlterColumn("tc.Pacientes", "ApellidoPaterno", c => c.String(maxLength: 30, unicode: false));
            AlterColumn("tc.Pacientes", "Nombre", c => c.String(maxLength: 40, unicode: false));
            AlterColumn("tc.Pacientes", "TipoDocumento", c => c.String(maxLength: 15, unicode: false));
            AlterColumn("tc.Pacientes", "Matricula", c => c.String(maxLength: 20, unicode: false));
            DropColumn("tc.Pacientes", "OcupacionDescripcion");
        }
    }
}
