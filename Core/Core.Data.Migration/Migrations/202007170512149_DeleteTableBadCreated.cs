namespace TC.BCP.CredinetWeb.Core.Data.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteTableBadCreated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("doctor.CasosRescateGrupos", "CasoId", "doctor.CasosAgendas");
            DropIndex("doctor.CasosRescateGrupos", new[] { "CasoId" });
            DropTable("doctor.CasosRescateGrupos");
        }
        
        public override void Down()
        {
            CreateTable(
                "doctor.CasosRescateGrupos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CasoId = c.Int(nullable: false),
                        DireccionExplicita = c.String(unicode: false, storeType: "text"),
                        Observaciones = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("doctor.CasosRescateGrupos", "CasoId");
            AddForeignKey("doctor.CasosRescateGrupos", "CasoId", "doctor.CasosAgendas", "Id");
        }
    }
}
