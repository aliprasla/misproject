namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedTOandFROMAccountToTransactions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "FromAccount_BankAccountID", c => c.Int());
            AddColumn("dbo.Transactions", "ToAccount_BankAccountID", c => c.Int());
            CreateIndex("dbo.Transactions", "FromAccount_BankAccountID");
            CreateIndex("dbo.Transactions", "ToAccount_BankAccountID");
            AddForeignKey("dbo.Transactions", "FromAccount_BankAccountID", "dbo.BankAccounts", "BankAccountID");
            AddForeignKey("dbo.Transactions", "ToAccount_BankAccountID", "dbo.BankAccounts", "BankAccountID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "ToAccount_BankAccountID", "dbo.BankAccounts");
            DropForeignKey("dbo.Transactions", "FromAccount_BankAccountID", "dbo.BankAccounts");
            DropIndex("dbo.Transactions", new[] { "ToAccount_BankAccountID" });
            DropIndex("dbo.Transactions", new[] { "FromAccount_BankAccountID" });
            DropColumn("dbo.Transactions", "ToAccount_BankAccountID");
            DropColumn("dbo.Transactions", "FromAccount_BankAccountID");
        }
    }
}
