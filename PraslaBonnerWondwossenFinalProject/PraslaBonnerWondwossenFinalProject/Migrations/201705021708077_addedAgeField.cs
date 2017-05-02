namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedAgeField : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Stocks", "StockPortfolio_BankAccountID", "dbo.BankAccounts");
            DropForeignKey("dbo.Stocks", "StockQuote_StockQuoteId", "dbo.StockQuotes");
            DropForeignKey("dbo.AspNetUsers", "StockPortfolio_BankAccountID", "dbo.BankAccounts");
            DropIndex("dbo.AspNetUsers", new[] { "StockPortfolio_BankAccountID" });
            DropIndex("dbo.Stocks", new[] { "StockPortfolio_BankAccountID" });
            DropIndex("dbo.Stocks", new[] { "StockQuote_StockQuoteId" });
            AddColumn("dbo.BankAccounts", "stock_StockID", c => c.Int());
            AddColumn("dbo.BankAccounts", "AppUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.StockQuotes", "Stock_StockID", c => c.Int());
            AlterColumn("dbo.BankAccounts", "CashBalance", c => c.Int());
            CreateIndex("dbo.BankAccounts", "stock_StockID");
            CreateIndex("dbo.BankAccounts", "AppUser_Id");
            CreateIndex("dbo.StockQuotes", "Stock_StockID");
            AddForeignKey("dbo.BankAccounts", "stock_StockID", "dbo.Stocks", "StockID");
            AddForeignKey("dbo.StockQuotes", "Stock_StockID", "dbo.Stocks", "StockID");
            AddForeignKey("dbo.BankAccounts", "AppUser_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.BankAccounts", "Gains");
            DropColumn("dbo.BankAccounts", "Fees");
            DropColumn("dbo.BankAccounts", "Bonuses");
            DropColumn("dbo.AspNetUsers", "StockPortfolio_BankAccountID");
            DropColumn("dbo.Stocks", "StockPortfolio_BankAccountID");
            DropColumn("dbo.Stocks", "StockQuote_StockQuoteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stocks", "StockQuote_StockQuoteId", c => c.Int());
            AddColumn("dbo.Stocks", "StockPortfolio_BankAccountID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "StockPortfolio_BankAccountID", c => c.Int());
            AddColumn("dbo.BankAccounts", "Bonuses", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.BankAccounts", "Fees", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.BankAccounts", "Gains", c => c.Decimal(precision: 18, scale: 2));
            DropForeignKey("dbo.BankAccounts", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.StockQuotes", "Stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.BankAccounts", "stock_StockID", "dbo.Stocks");
            DropIndex("dbo.StockQuotes", new[] { "Stock_StockID" });
            DropIndex("dbo.BankAccounts", new[] { "AppUser_Id" });
            DropIndex("dbo.BankAccounts", new[] { "stock_StockID" });
            AlterColumn("dbo.BankAccounts", "CashBalance", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.StockQuotes", "Stock_StockID");
            DropColumn("dbo.BankAccounts", "AppUser_Id");
            DropColumn("dbo.BankAccounts", "stock_StockID");
            CreateIndex("dbo.Stocks", "StockQuote_StockQuoteId");
            CreateIndex("dbo.Stocks", "StockPortfolio_BankAccountID");
            CreateIndex("dbo.AspNetUsers", "StockPortfolio_BankAccountID");
            AddForeignKey("dbo.AspNetUsers", "StockPortfolio_BankAccountID", "dbo.BankAccounts", "BankAccountID");
            AddForeignKey("dbo.Stocks", "StockQuote_StockQuoteId", "dbo.StockQuotes", "StockQuoteId");
            AddForeignKey("dbo.Stocks", "StockPortfolio_BankAccountID", "dbo.BankAccounts", "BankAccountID");
        }
    }
}
