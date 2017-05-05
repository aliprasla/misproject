namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class securityStamp : DbMigration
    {
        public override void Up()
        {
            var SecurityStamp = Guid.NewGuid().ToString();
        }
        
        public override void Down()
        {
            var SecurityStamp = Guid.NewGuid().ToString();
        }
    }
}
