namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStockTableRelationships : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BankAccounts", "StockType_StockTypeID", "dbo.StockTypes");
            DropIndex("dbo.BankAccounts", new[] { "StockType_StockTypeID" });
            DropColumn("dbo.BankAccounts", "Value");
            DropColumn("dbo.BankAccounts", "StockType_StockTypeID");
            DropTable("dbo.StockTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StockTypes",
                c => new
                    {
                        StockTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StockTypeID);
            
            AddColumn("dbo.BankAccounts", "StockType_StockTypeID", c => c.Int());
            AddColumn("dbo.BankAccounts", "Value", c => c.Int());
            CreateIndex("dbo.BankAccounts", "StockType_StockTypeID");
            AddForeignKey("dbo.BankAccounts", "StockType_StockTypeID", "dbo.StockTypes", "StockTypeID");
        }
    }
}
