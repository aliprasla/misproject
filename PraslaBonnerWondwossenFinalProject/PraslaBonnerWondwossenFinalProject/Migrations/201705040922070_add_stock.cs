namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_stock : DbMigration
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
            
            //AddColumn("dbo.Stocks", "Fees", c => c.Int(nullable: false));
            //AddColumn("dbo.Payees", "AppUser_Id", c => c.String(maxLength: 128));
            //AlterColumn("dbo.Stocks", "Symbol", c => c.String(nullable: false));
            //CreateIndex("dbo.Payees", "AppUser_Id");
            //AddForeignKey("dbo.Payees", "AppUser_Id", "dbo.AspNetUsers", "Id");
            //DropColumn("dbo.Stocks", "Fee");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Stocks", "Fee", c => c.Int(nullable: false));
            //DropForeignKey("dbo.PurchasedStocks", "stockportfolio_BankAccountID", "dbo.BankAccounts");
            //DropForeignKey("dbo.PurchasedStocks", "stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.Payees", "AppUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PurchasedStocks", new[] { "stockportfolio_BankAccountID" });
            DropIndex("dbo.PurchasedStocks", new[] { "stock_StockID" });
            DropIndex("dbo.Payees", new[] { "AppUser_Id" });
            //AlterColumn("dbo.Stocks", "Symbol", c => c.String());
            DropColumn("dbo.Payees", "AppUser_Id");
            //DropColumn("dbo.Stocks", "Fees");
            //DropTable("dbo.PurchasedStocks");
        }
    }
}
