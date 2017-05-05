namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class portfolio_details : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "Name", c => c.String());
            AddColumn("dbo.Sales", "Fees", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sales", "Fees");
            DropColumn("dbo.Sales", "Name");
        }
    }
}
