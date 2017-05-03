namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idk1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stocks", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Stocks", "Fees", c => c.Int(nullable: false));
            AddColumn("dbo.StockQuotes", "StockType_StockTypeID", c => c.Int());
            CreateIndex("dbo.StockQuotes", "StockType_StockTypeID");
            AddForeignKey("dbo.StockQuotes", "StockType_StockTypeID", "dbo.StockTypes", "StockTypeID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockQuotes", "StockType_StockTypeID", "dbo.StockTypes");
            DropIndex("dbo.StockQuotes", new[] { "StockType_StockTypeID" });
            DropColumn("dbo.StockQuotes", "StockType_StockTypeID");
            DropColumn("dbo.Stocks", "Fees");
            DropColumn("dbo.Stocks", "Type");
        }
    }
}
