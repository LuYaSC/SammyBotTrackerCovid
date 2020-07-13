namespace TC.BCP.CredinetWeb.Core.Data.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableAgendas : DbMigration
    {
        public override void Up()
        {
            AlterColumn("doctor.CasosAgendas", "DescripcionNivel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("doctor.CasosAgendas", "DescripcionNivel", c => c.String(maxLength: 50));
        }
    }
}
