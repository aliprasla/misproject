namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somechange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BankAccounts", "stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.StockQuotes", "Stock_StockID", "dbo.Stocks");
            DropIndex("dbo.BankAccounts", new[] { "stock_StockID" });
            DropIndex("dbo.Stocks", new[] { "StockType_StockTypeID" });
            DropIndex("dbo.StockQuotes", new[] { "Stock_StockID" });
            AddColumn("dbo.BankAccounts", "Value", c => c.Int());
            AddColumn("dbo.BankAccounts", "StockType_StockTypeID", c => c.Int());
            CreateIndex("dbo.BankAccounts", "StockType_StockTypeID");
            DropColumn("dbo.BankAccounts", "stock_StockID");
            DropTable("dbo.Stocks");
            DropTable("dbo.StockQuotes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StockQuotes",
                c => new
                    {
                        StockQuoteId = c.Int(nullable: false, identity: true),
                        Symbol = c.String(),
                        Name = c.String(),
                        PreviousClose = c.Double(nullable: false),
                        LastTradePrice = c.Double(nullable: false),
                        Volume = c.Double(nullable: false),
                        Stock_StockID = c.Int(),
                    })
                .PrimaryKey(t => t.StockQuoteId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockID = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        StockType_StockTypeID = c.Int(),
                    })
                .PrimaryKey(t => t.StockID);
            
            AddColumn("dbo.BankAccounts", "stock_StockID", c => c.Int());
            DropIndex("dbo.BankAccounts", new[] { "StockType_StockTypeID" });
            DropColumn("dbo.BankAccounts", "StockType_StockTypeID");
            DropColumn("dbo.BankAccounts", "Value");
            CreateIndex("dbo.StockQuotes", "Stock_StockID");
            CreateIndex("dbo.Stocks", "StockType_StockTypeID");
            CreateIndex("dbo.BankAccounts", "stock_StockID");
            AddForeignKey("dbo.StockQuotes", "Stock_StockID", "dbo.Stocks", "StockID");
            AddForeignKey("dbo.BankAccounts", "stock_StockID", "dbo.Stocks", "StockID");
        }
    }
}
