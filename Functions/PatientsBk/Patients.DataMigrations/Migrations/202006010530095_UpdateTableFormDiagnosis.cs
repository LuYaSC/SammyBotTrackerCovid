namespace SBTC.Functions.Patients.DataMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableFormDiagnosis : DbMigration
    {
        public override void Up()
        {
            AddColumn("tc.FormDiagInicials", "TipoSangre", c => c.String(maxLength: 5, fixedLength: true, unicode: false));
            AddColumn("tc.FormDiagInicials", "PerdioPeso", c => c.String(maxLength: 2, fixedLength: true, unicode: false));
            AlterColumn("tc.FormDiagInicials", "MedicoAsignado", c => c.String(maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("tc.FormDiagInicials", "MedicoAsignado", c => c.String());
            DropColumn("tc.FormDiagInicials", "PerdioPeso");
            DropColumn("tc.FormDiagInicials", "TipoSangre");
        }
    }
}
