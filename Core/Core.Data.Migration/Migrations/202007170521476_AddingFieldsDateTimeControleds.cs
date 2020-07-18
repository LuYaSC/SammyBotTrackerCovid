namespace TC.BCP.CredinetWeb.Core.Data.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingFieldsDateTimeControleds : DbMigration
    {
        public override void Up()
        {
            AddColumn("doctor.CasosGrupoRescates", "FechaPriorizacion", c => c.DateTime(nullable: false));
            AddColumn("doctor.CasosGrupoRescates", "FechaAtencion", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("doctor.CasosGrupoRescates", "FechaAtencion");
            DropColumn("doctor.CasosGrupoRescates", "FechaPriorizacion");
        }
    }
}
