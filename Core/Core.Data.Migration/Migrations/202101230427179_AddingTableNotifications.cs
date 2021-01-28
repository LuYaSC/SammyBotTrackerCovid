namespace TC.BCP.CredinetWeb.Core.Data.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingTableNotifications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "doctor.SendNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        SendPhone = c.String(nullable: false, maxLength: 15),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("doctor.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            AlterColumn("doctor.Parameters", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("doctor.SendNotifications", "UserId", "doctor.Users");
            DropIndex("doctor.SendNotifications", new[] { "UserId" });
            AlterColumn("doctor.Parameters", "Description", c => c.String(nullable: false, maxLength: 80));
            DropTable("doctor.SendNotifications");
        }
    }
}
