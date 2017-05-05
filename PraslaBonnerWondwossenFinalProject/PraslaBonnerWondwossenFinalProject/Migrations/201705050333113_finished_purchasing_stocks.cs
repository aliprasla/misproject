namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finished_purchasing_stocks : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Sales");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SalesId = c.Int(nullable: false, identity: true),
                        Shares = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                        NetProfit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SharesLeft = c.Int(nullable: false),
                        PurchaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SalesId);
            
        }
    }
}
