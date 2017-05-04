namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class something54 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PurchasedStocks", "Stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.PurchasedStocks", "StockPortfolio_BankAccountID", "dbo.BankAccounts");
            DropIndex("dbo.PurchasedStocks", new[] { "Stock_StockID" });
            DropIndex("dbo.PurchasedStocks", new[] { "StockPortfolio_BankAccountID" });
            AddColumn("dbo.Stocks", "Fee", c => c.Int(nullable: false));
            DropColumn("dbo.Stocks", "Fees");
            DropTable("dbo.PurchasedStocks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PurchasedStocks",
                c => new
                    {
                        PurchasedStockID = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        TotalFees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchasedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock_StockID = c.Int(),
                        StockPortfolio_BankAccountID = c.Int(),
                    })
                .PrimaryKey(t => t.PurchasedStockID);
            
            AddColumn("dbo.Stocks", "Fees", c => c.Int(nullable: false));
            DropColumn("dbo.Stocks", "Fee");
            CreateIndex("dbo.PurchasedStocks", "StockPortfolio_BankAccountID");
            CreateIndex("dbo.PurchasedStocks", "Stock_StockID");
            AddForeignKey("dbo.PurchasedStocks", "StockPortfolio_BankAccountID", "dbo.BankAccounts", "BankAccountID");
            AddForeignKey("dbo.PurchasedStocks", "Stock_StockID", "dbo.Stocks", "StockID");
        }
    }
}
