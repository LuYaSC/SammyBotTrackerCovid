namespace TC.BCP.CredinetWeb.Core.Data.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTablesNewProcess : DbMigration
    {
        public override void Up()
        {
            AddColumn("doctor.CasosRecuperados", "Url", c => c.String(unicode: false, storeType: "text"));
            AddColumn("doctor.CasosRecuperados", "CodigoSala", c => c.String(unicode: false, storeType: "text"));
            AddColumn("doctor.CasosRecuperados", "Finalizado", c => c.Boolean(nullable: false));
            AddColumn("doctor.CasosRecuperados", "Inconcluso", c => c.Boolean(nullable: false));
            AddColumn("doctor.CasosRecuperados", "EnvioBrigada", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("doctor.CasosRecuperados", "EnvioBrigada");
            DropColumn("doctor.CasosRecuperados", "Inconcluso");
            DropColumn("doctor.CasosRecuperados", "Finalizado");
            DropColumn("doctor.CasosRecuperados", "CodigoSala");
            DropColumn("doctor.CasosRecuperados", "Url");
        }
    }
}
