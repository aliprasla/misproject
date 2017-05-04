namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPayeeBack : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payees", "BankAccount_BankAccountID", c => c.Int());
            CreateIndex("dbo.Payees", "BankAccount_BankAccountID");
            AddForeignKey("dbo.Payees", "BankAccount_BankAccountID", "dbo.BankAccounts", "BankAccountID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payees", "BankAccount_BankAccountID", "dbo.BankAccounts");
            DropIndex("dbo.Payees", new[] { "BankAccount_BankAccountID" });
            DropColumn("dbo.Payees", "BankAccount_BankAccountID");
        }
    }
}
