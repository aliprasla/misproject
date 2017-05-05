namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wtf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
    "dbo.PurchasedStocks",
    c => new
    {
        PurchasedStockId = c.Int(nullable: false, identity: true),
        Shares = c.Int(nullable: false),
        InitialPrice = c.Double(nullable: false),
        TotalFees = c.Decimal(nullable: false, precision: 18, scale: 2),
        Date = c.DateTime(nullable: false),
        stock_StockID = c.Int(),
        stockportfolio_BankAccountID = c.Int(),
    })
    .PrimaryKey(t => t.PurchasedStockId)
    .ForeignKey("dbo.Stocks", t => t.stock_StockID)
    .ForeignKey("dbo.BankAccounts", t => t.stockportfolio_BankAccountID)
    .Index(t => t.stock_StockID)
    .Index(t => t.stockportfolio_BankAccountID);
        }
        
        public override void Down()
        {

        }
    }
}
