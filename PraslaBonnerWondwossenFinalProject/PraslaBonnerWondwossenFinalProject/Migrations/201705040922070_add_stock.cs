namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_stock : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PurchasedStocks", new[] { "Stock_StockID" });
            DropIndex("dbo.PurchasedStocks", new[] { "StockPortfolio_BankAccountID" });
            AddColumn("dbo.PurchasedStocks", "Shares", c => c.Int(nullable: false));
            AddColumn("dbo.PurchasedStocks", "InitialPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Stocks", "Symbol", c => c.String(nullable: false));
            CreateIndex("dbo.PurchasedStocks", "stock_StockID");
            CreateIndex("dbo.PurchasedStocks", "stockportfolio_BankAccountID");
            DropColumn("dbo.PurchasedStocks", "Amount");
            DropColumn("dbo.PurchasedStocks", "PurchasedPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchasedStocks", "PurchasedPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchasedStocks", "Amount", c => c.Int(nullable: false));
            DropIndex("dbo.PurchasedStocks", new[] { "stockportfolio_BankAccountID" });
            DropIndex("dbo.PurchasedStocks", new[] { "stock_StockID" });
            AlterColumn("dbo.Stocks", "Symbol", c => c.String());
            DropColumn("dbo.PurchasedStocks", "InitialPrice");
            DropColumn("dbo.PurchasedStocks", "Shares");
            CreateIndex("dbo.PurchasedStocks", "StockPortfolio_BankAccountID");
            CreateIndex("dbo.PurchasedStocks", "Stock_StockID");
        }
    }
}
