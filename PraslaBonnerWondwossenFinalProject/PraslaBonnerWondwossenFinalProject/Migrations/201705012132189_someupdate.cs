namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class someupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccounts", "isApproved", c => c.Boolean());
            AddColumn("dbo.BankAccounts", "isBalanced", c => c.Boolean());
            AddColumn("dbo.BankAccounts", "CashBalance", c => c.Int());
            AddColumn("dbo.BankAccounts", "AppUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.BankAccounts", "AppUser_Id");
            AddForeignKey("dbo.BankAccounts", "AppUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BankAccounts", "AppUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.BankAccounts", new[] { "AppUser_Id" });
            DropColumn("dbo.BankAccounts", "AppUser_Id");
            DropColumn("dbo.BankAccounts", "CashBalance");
            DropColumn("dbo.BankAccounts", "isBalanced");
            DropColumn("dbo.BankAccounts", "isApproved");
        }
    }
}
