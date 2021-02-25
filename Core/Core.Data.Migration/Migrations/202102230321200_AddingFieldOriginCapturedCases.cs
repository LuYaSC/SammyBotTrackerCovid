namespace TC.BCP.CredinetWeb.Core.Data.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingFieldOriginCapturedCases : DbMigration
    {
        public override void Up()
        {
            AddColumn("doctor.CapturedCases", "Origin", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("doctor.CapturedCases", "Origin");
        }
    }
}
