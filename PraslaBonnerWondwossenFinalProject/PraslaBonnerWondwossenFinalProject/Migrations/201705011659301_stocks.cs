namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stocks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccounts", "isApproved", c => c.Boolean());
            AddColumn("dbo.BankAccounts", "isBalanced", c => c.Boolean());
            AddColumn("dbo.BankAccounts", "CashBalance", c => c.Int());
            DropColumn("dbo.BankAccounts", "Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BankAccounts", "Value", c => c.Int());
            DropColumn("dbo.BankAccounts", "CashBalance");
            DropColumn("dbo.BankAccounts", "isBalanced");
            DropColumn("dbo.BankAccounts", "isApproved");
        }
    }
}
