namespace TC.BCP.CredinetWeb.Core.Data.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDateTimeFieldsTablesNewProcess : DbMigration
    {
        public override void Up()
        {
            AddColumn("doctor.CasosAgendas", "CasoRecuperado", c => c.Boolean(nullable: false));
            AddColumn("doctor.CasosAgendas", "RecetaMedica", c => c.String(unicode: false, storeType: "text"));
            AddColumn("doctor.CasosAgendas", "FechaEmisionBrigada", c => c.DateTime());
            AlterColumn("doctor.CasosCaptados", "FechaFinalizacion", c => c.DateTime());
            AlterColumn("doctor.CasosRecuperados", "FechaFinalizacion", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("doctor.CasosRecuperados", "FechaFinalizacion", c => c.DateTime(nullable: false));
            AlterColumn("doctor.CasosCaptados", "FechaFinalizacion", c => c.DateTime(nullable: false));
            DropColumn("doctor.CasosAgendas", "FechaEmisionBrigada");
            DropColumn("doctor.CasosAgendas", "RecetaMedica");
            DropColumn("doctor.CasosAgendas", "CasoRecuperado");
        }
    }
}
