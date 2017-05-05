namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class something : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.BankAccounts", new[] { "StockType_StockTypeID" });
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockID = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        StockType_StockTypeID = c.Int(),
                    })
                .PrimaryKey(t => t.StockID)
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
            
            AddColumn("dbo.BankAccounts", "stock_StockID", c => c.Int());
            CreateIndex("dbo.BankAccounts", "stock_StockID");
            AddForeignKey("dbo.BankAccounts", "stock_StockID", "dbo.Stocks", "StockID");
            DropColumn("dbo.BankAccounts", "Value");
            DropColumn("dbo.BankAccounts", "StockType_StockTypeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BankAccounts", "StockType_StockTypeID", c => c.Int());
            AddColumn("dbo.BankAccounts", "Value", c => c.Int());
            DropForeignKey("dbo.StockQuotes", "Stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.BankAccounts", "stock_StockID", "dbo.Stocks");
            DropIndex("dbo.StockQuotes", new[] { "Stock_StockID" });
            DropIndex("dbo.Stocks", new[] { "StockType_StockTypeID" });
            DropIndex("dbo.BankAccounts", new[] { "stock_StockID" });
            DropColumn("dbo.BankAccounts", "stock_StockID");
            DropTable("dbo.StockQuotes");
            DropTable("dbo.Stocks");
            CreateIndex("dbo.BankAccounts", "StockType_StockTypeID");
        }
    }
}
