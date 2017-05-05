namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class altered_disputes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "isFired", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "isFired");
        }
    }
}
