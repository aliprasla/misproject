namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCashBalancceStockPortfoliioa : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BankAccounts", "CashBalance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BankAccounts", "CashBalance", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
