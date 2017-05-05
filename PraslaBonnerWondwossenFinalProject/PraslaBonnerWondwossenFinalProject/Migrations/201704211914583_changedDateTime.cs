namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Birthday", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Birthday", c => c.DateTime(nullable: false));
        }
    }
}
