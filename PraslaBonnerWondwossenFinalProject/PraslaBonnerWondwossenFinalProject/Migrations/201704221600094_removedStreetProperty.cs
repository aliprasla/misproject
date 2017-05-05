namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedStreetProperty : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Payees", "Street");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payees", "Street", c => c.String(nullable: false));
        }
    }
}
