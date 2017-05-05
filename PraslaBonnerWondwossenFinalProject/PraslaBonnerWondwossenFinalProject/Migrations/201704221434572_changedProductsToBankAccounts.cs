namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedProductsToBankAccounts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Customer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "Person_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BankAccounts", "Customer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.BankAccounts", new[] { "Customer_Id" });
            DropIndex("dbo.Products", new[] { "Customer_Id" });
            DropIndex("dbo.Products", new[] { "Person_Id" });
            DropIndex("dbo.Products", new[] { "AppUser_Id" });
            AddColumn("dbo.BankAccounts", "ProductNumber", c => c.Int(nullable: false));
            AddColumn("dbo.BankAccounts", "Type", c => c.Int(nullable: false));
            AlterColumn("dbo.BankAccounts", "Name", c => c.String());
            AlterColumn("dbo.BankAccounts", "Customer_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.BankAccounts", "Customer_Id");
            AddForeignKey("dbo.BankAccounts", "Customer_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.BankAccounts", "AccountNo");
            DropTable("dbo.Products");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.ProductID);
            
            AddColumn("dbo.BankAccounts", "AccountNo", c => c.Int(nullable: false));
            DropForeignKey("dbo.BankAccounts", "Customer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.BankAccounts", new[] { "Customer_Id" });
            AlterColumn("dbo.BankAccounts", "Customer_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.BankAccounts", "Name", c => c.String(nullable: false));
            DropColumn("dbo.BankAccounts", "Type");
            DropColumn("dbo.BankAccounts", "ProductNumber");
            CreateIndex("dbo.Products", "AppUser_Id");
            CreateIndex("dbo.Products", "Person_Id");
            CreateIndex("dbo.Products", "Customer_Id");
            CreateIndex("dbo.BankAccounts", "Customer_Id");
            AddForeignKey("dbo.BankAccounts", "Customer_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "AppUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Products", "Person_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Products", "Customer_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
