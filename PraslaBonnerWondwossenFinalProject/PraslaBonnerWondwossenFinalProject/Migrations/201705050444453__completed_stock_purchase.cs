namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _completed_stock_purchase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PayeeAppUsers", "Payee_PayeeID", "dbo.Payees");
            DropForeignKey("dbo.PayeeAppUsers", "AppUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PayeeAppUsers", new[] { "Payee_PayeeID" });
            DropIndex("dbo.PayeeAppUsers", new[] { "AppUser_Id" });
            AddColumn("dbo.Payees", "AppUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Payees", "AppUser_Id");
            AddForeignKey("dbo.Payees", "AppUser_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.BankAccounts", "isBalanced");
            DropTable("dbo.PayeeAppUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PayeeAppUsers",
                c => new
                    {
                        Payee_PayeeID = c.Int(nullable: false),
                        AppUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Payee_PayeeID, t.AppUser_Id });
            
            AddColumn("dbo.BankAccounts", "isBalanced", c => c.Boolean());
            DropForeignKey("dbo.Payees", "AppUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Payees", new[] { "AppUser_Id" });
            DropColumn("dbo.Payees", "AppUser_Id");
            CreateIndex("dbo.PayeeAppUsers", "AppUser_Id");
            CreateIndex("dbo.PayeeAppUsers", "Payee_PayeeID");
            AddForeignKey("dbo.PayeeAppUsers", "AppUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PayeeAppUsers", "Payee_PayeeID", "dbo.Payees", "PayeeID", cascadeDelete: true);
        }
    }
}
