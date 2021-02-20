namespace TC.BCP.CredinetWeb.Core.Data.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesCapturedCases : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("doctor.CapturedCases");
            AddPrimaryKey("doctor.CapturedCases", "CaseId");
            DropColumn("doctor.CapturedCases", "Id");
        }
        
        public override void Down()
        {
            AddColumn("doctor.CapturedCases", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("doctor.CapturedCases");
            AddPrimaryKey("doctor.CapturedCases", "Id");
        }
    }
}
