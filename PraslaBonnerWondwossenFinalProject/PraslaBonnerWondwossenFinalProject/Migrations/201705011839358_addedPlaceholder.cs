namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPlaceholder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccounts", "Transaction_TransactionID", c => c.Int());
            CreateIndex("dbo.BankAccounts", "Transaction_TransactionID");
            AddForeignKey("dbo.BankAccounts", "Transaction_TransactionID", "dbo.Transactions", "TransactionID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BankAccounts", "Transaction_TransactionID", "dbo.Transactions");
            DropIndex("dbo.BankAccounts", new[] { "Transaction_TransactionID" });
            DropColumn("dbo.BankAccounts", "Transaction_TransactionID");
        }
    }
}
