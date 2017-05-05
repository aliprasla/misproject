namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _427171 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "isFired");
            DropColumn("dbo.Disputes", "DisputeAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Disputes", "DisputeAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.AspNetUsers", "isFired", c => c.Boolean());
        }
    }
}
