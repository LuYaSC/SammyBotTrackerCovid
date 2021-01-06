namespace TC.BCP.CredinetWeb.Core.Data.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableAgendaAddingCodigoSala : DbMigration
    {
        public override void Up()
        {
            AddColumn("doctor.CasosAgendas", "CodigoSala", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            DropColumn("doctor.CasosAgendas", "CodigoSala");
        }
    }
}
