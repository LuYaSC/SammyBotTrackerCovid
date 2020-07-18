namespace TC.BCP.CredinetWeb.Core.Data.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTablesForNewProcess : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "doctor.CasosCaptados",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InternoId = c.Int(nullable: false),
                        Observaciones = c.String(unicode: false, storeType: "text"),
                        NombrePaciente = c.String(maxLength: 60),
                        NumeroCelular = c.String(maxLength: 15),
                        RedSocial = c.String(maxLength: 30),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaFinalizacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("doctor.Users", t => t.InternoId)
                .Index(t => t.InternoId);
            
            CreateTable(
                "doctor.CasosRecuperados",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CasoId = c.Int(nullable: false),
                        InternoId = c.Int(nullable: false),
                        Observaciones = c.String(unicode: false, storeType: "text"),
                        FechaAtencion = c.DateTime(nullable: false),
                        FechaFinalizacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("doctor.CasosAgendas", t => t.CasoId)
                .ForeignKey("doctor.Users", t => t.InternoId)
                .Index(t => t.CasoId)
                .Index(t => t.InternoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("doctor.CasosRecuperados", "InternoId", "doctor.Users");
            DropForeignKey("doctor.CasosRecuperados", "CasoId", "doctor.CasosAgendas");
            DropForeignKey("doctor.CasosCaptados", "InternoId", "doctor.Users");
            DropIndex("doctor.CasosRecuperados", new[] { "InternoId" });
            DropIndex("doctor.CasosRecuperados", new[] { "CasoId" });
            DropIndex("doctor.CasosCaptados", new[] { "InternoId" });
            DropTable("doctor.CasosRecuperados");
            DropTable("doctor.CasosCaptados");
        }
    }
}
