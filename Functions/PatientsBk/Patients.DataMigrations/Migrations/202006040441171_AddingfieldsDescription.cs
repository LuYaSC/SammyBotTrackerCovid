namespace SBTC.Functions.Patients.DataMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingfieldsDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("tc.FormDiagInicials", "medicamentosConsumidosDescripcion", c => c.String(unicode: false, storeType: "text"));
            AddColumn("tc.FormDiagInicials", "VitaminasConsumidasDescripcion", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            DropColumn("tc.FormDiagInicials", "VitaminasConsumidasDescripcion");
            DropColumn("tc.FormDiagInicials", "medicamentosConsumidosDescripcion");
        }
    }
}
