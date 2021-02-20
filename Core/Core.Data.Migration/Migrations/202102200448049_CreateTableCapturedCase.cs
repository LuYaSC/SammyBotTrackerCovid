namespace TC.BCP.CredinetWeb.Core.Data.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableCapturedCase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "doctor.CapturedCases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CaseId = c.Int(nullable: false),
                        BornDate = c.String(maxLength: 10),
                        IsInsurance = c.Boolean(nullable: false),
                        IsNewPatient = c.Boolean(nullable: false),
                        InsuranceName = c.String(maxLength: 60),
                        Departament = c.String(maxLength: 30),
                        Municipality = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("doctor.CasosCaptados", t => t.CaseId)
                .Index(t => t.CaseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("doctor.CapturedCases", "CaseId", "doctor.CasosCaptados");
            DropIndex("doctor.CapturedCases", new[] { "CaseId" });
            DropTable("doctor.CapturedCases");
        }
    }
}
