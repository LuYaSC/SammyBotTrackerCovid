namespace TC.BCP.CredinetWeb.Core.Data.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingFieldRecetaMedicaTableRecovers : DbMigration
    {
        public override void Up()
        {
            AddColumn("doctor.CasosRecuperados", "RecetaMedica", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            DropColumn("doctor.CasosRecuperados", "RecetaMedica");
        }
    }
}
