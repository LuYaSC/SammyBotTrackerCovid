namespace TC.BCP.CredinetWeb.Core.Data.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableRescueGroups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "doctor.CasosGrupoRescates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CasoId = c.Int(nullable: false),
                        DireccionExplicita = c.String(unicode: false, storeType: "text"),
                        Observaciones = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("doctor.CasosAgendas", t => t.CasoId)
                .Index(t => t.CasoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("doctor.CasosGrupoRescates", "CasoId", "doctor.CasosAgendas");
            DropIndex("doctor.CasosGrupoRescates", new[] { "CasoId" });
            DropTable("doctor.CasosGrupoRescates");
        }
    }
}
