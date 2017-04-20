namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Disputes",
                c => new
                    {
                        DisputeID = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CustomerDescription = c.String(nullable: false),
                        ManagerDescription = c.String(),
                        AssignedManager_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DisputeID)
                .ForeignKey("dbo.AspNetUsers", t => t.AssignedManager_Id)
                .ForeignKey("dbo.Transactions", t => t.DisputeID)
                .Index(t => t.DisputeID)
                .Index(t => t.AssignedManager_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FName = c.String(nullable: false),
                        Middle = c.String(),
                        LName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        Zip = c.Int(nullable: false),
                        Phone = c.String(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        isActive = c.Boolean(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
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
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductNumber = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Name = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Customer_Id = c.String(maxLength: 128),
                        Person_Id = c.String(maxLength: 128),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.AspNetUsers", t => t.Customer_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Person_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.Person_Id)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        Customer_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TransactionID)
                .ForeignKey("dbo.AspNetUsers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Payees",
                c => new
                    {
                        PayeeID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        ZipCode = c.Int(nullable: false),
                        Phone = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PayeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Disputes", "DisputeID", "dbo.Transactions");
            DropForeignKey("dbo.Transactions", "Customer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "Person_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "Customer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Disputes", "AssignedManager_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Transactions", new[] { "Customer_Id" });
            DropIndex("dbo.Products", new[] { "AppUser_Id" });
            DropIndex("dbo.Products", new[] { "Person_Id" });
            DropIndex("dbo.Products", new[] { "Customer_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Disputes", new[] { "AssignedManager_Id" });
            DropIndex("dbo.Disputes", new[] { "DisputeID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.Payees");
            DropTable("dbo.Transactions");
            DropTable("dbo.Products");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Disputes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
