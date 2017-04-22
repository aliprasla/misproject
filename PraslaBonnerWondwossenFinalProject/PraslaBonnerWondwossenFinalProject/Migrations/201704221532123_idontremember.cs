namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idontremember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payees", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Payees", "Phone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payees", "Phone", c => c.Int(nullable: false));
            DropColumn("dbo.Payees", "Address");
        }
    }
}
