namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sales",
                c => new
                {
                    SalesId = c.Int(nullable: true, identity: true),
                    Shares = c.Int(nullable: true),
                    date = c.DateTime(nullable: true),
                    NetProfit = c.Decimal(nullable: true, precision: 18, scale: 2),
                    SharesLeft = c.Int(nullable: true),
                    PurchaseId = c.Int(nullable: true),
                })
                .PrimaryKey(t => t.SalesId);
        }
        
        public override void Down()
        {
            DropTable("dbo.Sales");
        }
    }
}
