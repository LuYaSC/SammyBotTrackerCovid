namespace TC.BCP.CredinetWeb.Core.Data.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableBrigadas : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("doctor.CasosAgendas", t => t.CasoId)
                .Index(t => t.CasoId);
            
            AddColumn("doctor.CasosAgendas", "EnviadoBrigada", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("doctor.CasosRescateGrupos", "CasoId", "doctor.CasosAgendas");
            DropIndex("doctor.CasosRescateGrupos", new[] { "CasoId" });
            DropColumn("doctor.CasosAgendas", "EnviadoBrigada");
            DropTable("doctor.CasosRescateGrupos");
        }
    }
}
