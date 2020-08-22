namespace TC.BCP.CredinetWeb.Core.Data.Migration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitalDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "doctor.Audiences",
                c => new
                    {
                        ClientId = c.String(nullable: false, maxLength: 32),
                        Base64Secret = c.String(nullable: false, maxLength: 80),
                        Name = c.String(nullable: false, maxLength: 100),
                        RefreshTokenLifeTime = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "doctor.Audits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Action = c.Int(nullable: false),
                        AuditGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("doctor.AuditGroups", t => t.AuditGroupId)
                .Index(t => t.AuditGroupId);
            
            CreateTable(
                "doctor.AuditGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Entity = c.String(nullable: false),
                        UserCreation = c.Int(nullable: false),
                        UserModification = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("doctor.Users", t => t.UserCreation)
                .ForeignKey("doctor.Users", t => t.UserModification)
                .Index(t => t.UserCreation)
                .Index(t => t.UserModification);
            
            CreateTable(
                "doctor.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 16, fixedLength: true),
                        AvailableDays = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(),
                        DateLastPasswordChange = c.DateTime(),
                        State = c.String(maxLength: 1, fixedLength: true),
                        Email = c.String(maxLength: 50, fixedLength: true),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(maxLength: 12, fixedLength: true),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "doctor.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("doctor.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "doctor.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("doctor.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "doctor.UserRoles",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("doctor.Roles", t => t.RoleId)
                .ForeignKey("doctor.Users", t => t.UserId)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "doctor.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "doctor.UserDetails",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 25),
                        FirstLastName = c.String(nullable: false, maxLength: 25),
                        SecondLastName = c.String(nullable: false, maxLength: 25),
                        UserCreation = c.String(nullable: false, maxLength: 6),
                        UserModification = c.String(maxLength: 6),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("doctor.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "doctor.Parameters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Groups = c.String(nullable: false, maxLength: 6),
                        Code = c.String(nullable: false, maxLength: 6),
                        Value = c.String(nullable: false, maxLength: 8),
                        Description = c.String(nullable: false, maxLength: 80),
                        UserCreation = c.String(nullable: false, maxLength: 6),
                        UserModification = c.String(maxLength: 6),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "doctor.RefreshTokens",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Subject = c.String(nullable: false, maxLength: 50),
                        ClientId = c.String(nullable: false, maxLength: 50),
                        IssuedUtc = c.DateTime(nullable: false),
                        ExpiresUtc = c.DateTime(nullable: false),
                        ProtectedTicket = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "doctor.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Action = c.String(maxLength: 1, fixedLength: true),
                        CardNumber = c.String(nullable: false, maxLength: 16),
                        Password = c.String(nullable: false),
                        Ip = c.String(nullable: false, maxLength: 15),
                        UserCreation = c.String(nullable: false, maxLength: 6),
                        UserModification = c.String(maxLength: 6),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("doctor.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "doctor.UserCardHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardNumber = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        UserCreation = c.String(nullable: false, maxLength: 6),
                        UserModification = c.String(maxLength: 6),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("doctor.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("doctor.UserCardHistories", "UserId", "doctor.Users");
            DropForeignKey("doctor.Sessions", "UserId", "doctor.Users");
            DropForeignKey("doctor.AuditGroups", "UserModification", "doctor.Users");
            DropForeignKey("doctor.AuditGroups", "UserCreation", "doctor.Users");
            DropForeignKey("doctor.UserDetails", "Id", "doctor.Users");
            DropForeignKey("doctor.UserRoles", "UserId", "doctor.Users");
            DropForeignKey("doctor.UserRoles", "RoleId", "doctor.Roles");
            DropForeignKey("doctor.AspNetUserLogins", "UserId", "doctor.Users");
            DropForeignKey("doctor.AspNetUserClaims", "UserId", "doctor.Users");
            DropForeignKey("doctor.Audits", "AuditGroupId", "doctor.AuditGroups");
            DropIndex("doctor.UserCardHistories", new[] { "UserId" });
            DropIndex("doctor.Sessions", new[] { "UserId" });
            DropIndex("doctor.UserDetails", new[] { "Id" });
            DropIndex("doctor.Roles", "RoleNameIndex");
            DropIndex("doctor.UserRoles", new[] { "UserId" });
            DropIndex("doctor.UserRoles", new[] { "RoleId" });
            DropIndex("doctor.AspNetUserLogins", new[] { "UserId" });
            DropIndex("doctor.AspNetUserClaims", new[] { "UserId" });
            DropIndex("doctor.Users", "UserNameIndex");
            DropIndex("doctor.AuditGroups", new[] { "UserModification" });
            DropIndex("doctor.AuditGroups", new[] { "UserCreation" });
            DropIndex("doctor.Audits", new[] { "AuditGroupId" });
            DropTable("doctor.UserCardHistories");
            DropTable("doctor.Sessions");
            DropTable("doctor.RefreshTokens");
            DropTable("doctor.Parameters");
            DropTable("doctor.UserDetails");
            DropTable("doctor.Roles");
            DropTable("doctor.UserRoles");
            DropTable("doctor.AspNetUserLogins");
            DropTable("doctor.AspNetUserClaims");
            DropTable("doctor.Users");
            DropTable("doctor.AuditGroups");
            DropTable("doctor.Audits");
            DropTable("doctor.Audiences");
        }
    }
}
