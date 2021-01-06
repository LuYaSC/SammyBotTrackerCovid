namespace SBTC.Functions.Patients.DataMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeLengthExtensionPatient : DbMigration
    {
        public override void Up()
        {
            AlterColumn("tc.Pacientes", "Extension", c => c.String(maxLength: 2, fixedLength: true, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("tc.Pacientes", "Extension", c => c.String(maxLength: 3, fixedLength: true, unicode: false));
        }
    }
}
