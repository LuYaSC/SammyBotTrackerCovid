namespace TC.BCP.CredinetWeb.Core.Data.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateComponenetCapturedCase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("doctor.CapturedCases", "CaseId", "doctor.CasosCaptados");
            AddForeignKey("doctor.CapturedCases", "CaseId", "doctor.CasosAgendas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("doctor.CapturedCases", "CaseId", "doctor.CasosAgendas");
            AddForeignKey("doctor.CapturedCases", "CaseId", "doctor.CasosCaptados", "Id");
        }
    }
}
