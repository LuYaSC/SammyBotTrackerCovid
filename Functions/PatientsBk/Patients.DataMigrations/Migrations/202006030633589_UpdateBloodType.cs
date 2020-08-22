namespace SBTC.Functions.Patients.DataMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBloodType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("tc.FormDiagInicials", "TipoSangre", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            AlterColumn("tc.FormDiagInicials", "TipoSangre", c => c.String(maxLength: 5, fixedLength: true, unicode: false));
        }
    }
}
