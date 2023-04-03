namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        Image = c.String(),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.FriendOfUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        FriendId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.FriendId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.FriendId);
            
            CreateTable(
                "dbo.FriendRequest",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        RequestSenderId = c.String(nullable: false, maxLength: 128),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RequestSenderId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RequestSenderId);
            
            CreateTable(
                "dbo.PostComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Comment = c.String(nullable: false, unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Post", t => t.PostId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.PostId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, unicode: false, storeType: "text"),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Time = c.DateTime(nullable: false),
                        Visibility = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PostReact",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        React = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Post", t => t.PostId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.PostId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.__MigrationHistory",
                c => new
                    {
                        MigrationId = c.String(nullable: false, maxLength: 150),
                        ContextKey = c.String(nullable: false, maxLength: 300),
                        Model = c.Binary(nullable: false),
                        ProductVersion = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => new { t.MigrationId, t.ContextKey });
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Post", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostReact", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostComment", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostReact", "PostId", "dbo.Post");
            DropForeignKey("dbo.PostComment", "PostId", "dbo.Post");
            DropForeignKey("dbo.FriendRequest", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequest", "RequestSenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendOfUser", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendOfUser", "FriendId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.PostReact", new[] { "UserId" });
            DropIndex("dbo.PostReact", new[] { "PostId" });
            DropIndex("dbo.Post", new[] { "UserId" });
            DropIndex("dbo.PostComment", new[] { "UserId" });
            DropIndex("dbo.PostComment", new[] { "PostId" });
            DropIndex("dbo.FriendRequest", new[] { "RequestSenderId" });
            DropIndex("dbo.FriendRequest", new[] { "UserId" });
            DropIndex("dbo.FriendOfUser", new[] { "FriendId" });
            DropIndex("dbo.FriendOfUser", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.__MigrationHistory");
            DropTable("dbo.PostReact");
            DropTable("dbo.Post");
            DropTable("dbo.PostComment");
            DropTable("dbo.FriendRequest");
            DropTable("dbo.FriendOfUser");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetRoles");
        }
    }
}
