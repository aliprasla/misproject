namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreStockRelationships : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockID = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        StockType_StockTypeID = c.Int(),
                    })
                .PrimaryKey(t => t.StockID)
                .ForeignKey("dbo.StockTypes", t => t.StockType_StockTypeID)
                .Index(t => t.StockType_StockTypeID);
            
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
                .PrimaryKey(t => t.StockQuoteId)
                .ForeignKey("dbo.Stocks", t => t.Stock_StockID)
                .Index(t => t.Stock_StockID);
            
            CreateTable(
                "dbo.StockTypes",
                c => new
                    {
                        StockTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StockTypeID);
            
            AddColumn("dbo.BankAccounts", "stock_StockID", c => c.Int());
            CreateIndex("dbo.BankAccounts", "stock_StockID");
            AddForeignKey("dbo.BankAccounts", "stock_StockID", "dbo.Stocks", "StockID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stocks", "StockType_StockTypeID", "dbo.StockTypes");
            DropForeignKey("dbo.StockQuotes", "Stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.BankAccounts", "stock_StockID", "dbo.Stocks");
            DropIndex("dbo.StockQuotes", new[] { "Stock_StockID" });
            DropIndex("dbo.Stocks", new[] { "StockType_StockTypeID" });
            DropIndex("dbo.BankAccounts", new[] { "stock_StockID" });
            DropColumn("dbo.BankAccounts", "stock_StockID");
            DropTable("dbo.StockTypes");
            DropTable("dbo.StockQuotes");
            DropTable("dbo.Stocks");
        }
    }
}
