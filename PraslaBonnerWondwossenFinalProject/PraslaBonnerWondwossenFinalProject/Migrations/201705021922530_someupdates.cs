namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class someupdates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccounts", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUsers", "StockPortfolio_BankAccountID", c => c.Int());
            AlterColumn("dbo.BankAccounts", "CashBalance", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.BankAccounts", "Gains", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.BankAccounts", "Fees", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.BankAccounts", "Bonuses", c => c.Decimal(precision: 18, scale: 2));
            CreateIndex("dbo.AspNetUsers", "StockPortfolio_BankAccountID");
            AddForeignKey("dbo.AspNetUsers", "StockPortfolio_BankAccountID", "dbo.BankAccounts", "BankAccountID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "StockPortfolio_BankAccountID", "dbo.BankAccounts");
            DropIndex("dbo.AspNetUsers", new[] { "StockPortfolio_BankAccountID" });
            AlterColumn("dbo.BankAccounts", "Bonuses", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BankAccounts", "Fees", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BankAccounts", "Gains", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BankAccounts", "CashBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.AspNetUsers", "StockPortfolio_BankAccountID");
            DropColumn("dbo.BankAccounts", "Discriminator");
        }
    }
}
