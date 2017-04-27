namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Disputes", "DisputeAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Disputes", "DisputeAmount");
        }
    }
}