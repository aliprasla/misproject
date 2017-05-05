namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStockPortfolioModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BankAccounts", "stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.StockQuotes", "Stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.BankAccounts", "AppUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.BankAccounts", new[] { "stock_StockID" });
            DropIndex("dbo.BankAccounts", new[] { "AppUser_Id" });
            DropIndex("dbo.StockQuotes", new[] { "Stock_StockID" });
            AddColumn("dbo.BankAccounts", "Gains", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.BankAccounts", "Fees", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.BankAccounts", "Bonuses", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.AspNetUsers", "StockPortfolio_BankAccountID", c => c.Int());
            AddColumn("dbo.Stocks", "StockPortfolio_BankAccountID", c => c.Int());
            AddColumn("dbo.Stocks", "StockQuote_StockQuoteId", c => c.Int());
            AlterColumn("dbo.BankAccounts", "CashBalance", c => c.Decimal(precision: 18, scale: 2));
            CreateIndex("dbo.AspNetUsers", "StockPortfolio_BankAccountID");
            CreateIndex("dbo.Stocks", "StockPortfolio_BankAccountID");
            CreateIndex("dbo.Stocks", "StockQuote_StockQuoteId");
            AddForeignKey("dbo.Stocks", "StockPortfolio_BankAccountID", "dbo.BankAccounts", "BankAccountID");
            AddForeignKey("dbo.Stocks", "StockQuote_StockQuoteId", "dbo.StockQuotes", "StockQuoteId");
            AddForeignKey("dbo.AspNetUsers", "StockPortfolio_BankAccountID", "dbo.BankAccounts", "BankAccountID");
            DropColumn("dbo.BankAccounts", "stock_StockID");
            DropColumn("dbo.BankAccounts", "AppUser_Id");
            DropColumn("dbo.StockQuotes", "Stock_StockID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StockQuotes", "Stock_StockID", c => c.Int());
            AddColumn("dbo.BankAccounts", "AppUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.BankAccounts", "stock_StockID", c => c.Int());
            DropForeignKey("dbo.AspNetUsers", "StockPortfolio_BankAccountID", "dbo.BankAccounts");
            DropForeignKey("dbo.Stocks", "StockQuote_StockQuoteId", "dbo.StockQuotes");
            DropForeignKey("dbo.Stocks", "StockPortfolio_BankAccountID", "dbo.BankAccounts");
            DropIndex("dbo.Stocks", new[] { "StockQuote_StockQuoteId" });
            DropIndex("dbo.Stocks", new[] { "StockPortfolio_BankAccountID" });
            DropIndex("dbo.AspNetUsers", new[] { "StockPortfolio_BankAccountID" });
            AlterColumn("dbo.BankAccounts", "CashBalance", c => c.Int());
            DropColumn("dbo.Stocks", "StockQuote_StockQuoteId");
            DropColumn("dbo.Stocks", "StockPortfolio_BankAccountID");
            DropColumn("dbo.AspNetUsers", "StockPortfolio_BankAccountID");
            DropColumn("dbo.BankAccounts", "Bonuses");
            DropColumn("dbo.BankAccounts", "Fees");
            DropColumn("dbo.BankAccounts", "Gains");
            CreateIndex("dbo.StockQuotes", "Stock_StockID");
            CreateIndex("dbo.BankAccounts", "AppUser_Id");
            CreateIndex("dbo.BankAccounts", "stock_StockID");
            AddForeignKey("dbo.BankAccounts", "AppUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.StockQuotes", "Stock_StockID", "dbo.Stocks", "StockID");
            AddForeignKey("dbo.BankAccounts", "stock_StockID", "dbo.Stocks", "StockID");
        }
    }
}
