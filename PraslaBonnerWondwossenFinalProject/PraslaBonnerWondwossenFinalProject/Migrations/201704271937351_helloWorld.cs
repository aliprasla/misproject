namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class helloWorld : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payees", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String(nullable: false));
            DropColumn("dbo.AspNetUsers", "Phone");
            DropColumn("dbo.Payees", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payees", "Phone", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            DropColumn("dbo.Payees", "PhoneNumber");
        }
    }
}
