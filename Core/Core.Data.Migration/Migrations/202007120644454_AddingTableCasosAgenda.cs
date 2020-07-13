namespace TC.BCP.CredinetWeb.Core.Data.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingTableCasosAgenda : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "doctor.CasosAgendas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PacienteId = c.Int(nullable: false),
                        InternoId = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        DescripcionNivel = c.String(maxLength: 50),
                        NombrePaciente = c.String(maxLength: 60),
                        HoraInicio = c.String(maxLength: 10),
                        HoraFin = c.String(maxLength: 10),
                        UrlSala = c.String(unicode: false, storeType: "text"),
                        Finalizado = c.Boolean(nullable: false),
                        Inconcluso = c.Boolean(nullable: false),
                        Observaciones = c.String(unicode: false, storeType: "text"),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("doctor.P_Pacientes", t => t.PacienteId)
                .ForeignKey("doctor.Users", t => t.DoctorId)
                .ForeignKey("doctor.Users", t => t.InternoId)
                .Index(t => t.PacienteId)
                .Index(t => t.InternoId)
                .Index(t => t.DoctorId);
        }
        
        public override void Down()
        {
            DropForeignKey("doctor.CasosAgendas", "InternoId", "doctor.Users");
            DropForeignKey("doctor.CasosAgendas", "DoctorId", "doctor.Users");
            DropForeignKey("doctor.CasosAgendas", "PacienteId", "doctor.P_Pacientes");
            DropIndex("doctor.CasosAgendas", new[] { "DoctorId" });
            DropIndex("doctor.CasosAgendas", new[] { "InternoId" });
            DropIndex("doctor.CasosAgendas", new[] { "PacienteId" });
            DropTable("doctor.CasosAgendas");
        }
    }
}
