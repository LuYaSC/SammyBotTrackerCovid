namespace TC.BCP.CredinetWeb.Core.Data.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldMessageStatusNotifications : DbMigration
    {
        public override void Up()
        {
            AddColumn("doctor.SendNotifications", "UID", c => c.String(nullable: false, maxLength: 50));
            AddColumn("doctor.SendNotifications", "DateCreation", c => c.DateTime(nullable: false));
            AddColumn("doctor.SendNotifications", "MessageStatus", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            DropColumn("doctor.SendNotifications", "MessageStatus");
            DropColumn("doctor.SendNotifications", "DateCreation");
            DropColumn("doctor.SendNotifications", "UID");
        }
    }
}
