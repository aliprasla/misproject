namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedSomethingOnStockPortfolioModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccounts", "CashBalance", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankAccounts", "CashBalance");
        }
    }
}
