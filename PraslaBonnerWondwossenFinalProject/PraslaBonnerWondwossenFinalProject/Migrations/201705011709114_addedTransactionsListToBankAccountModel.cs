namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedTransactionsListToBankAccountModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccounts", "Value", c => c.Int());
            AddColumn("dbo.Transactions", "BankAccount_BankAccountID", c => c.Int());
            CreateIndex("dbo.Transactions", "BankAccount_BankAccountID");
            AddForeignKey("dbo.Transactions", "BankAccount_BankAccountID", "dbo.BankAccounts", "BankAccountID");
            DropColumn("dbo.BankAccounts", "isApproved");
            DropColumn("dbo.BankAccounts", "isBalanced");
            DropColumn("dbo.BankAccounts", "CashBalance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BankAccounts", "CashBalance", c => c.Int());
            AddColumn("dbo.BankAccounts", "isBalanced", c => c.Boolean());
            AddColumn("dbo.BankAccounts", "isApproved", c => c.Boolean());
            DropForeignKey("dbo.Transactions", "BankAccount_BankAccountID", "dbo.BankAccounts");
            DropIndex("dbo.Transactions", new[] { "BankAccount_BankAccountID" });
            DropColumn("dbo.Transactions", "BankAccount_BankAccountID");
            DropColumn("dbo.BankAccounts", "Value");
        }
    }
}
