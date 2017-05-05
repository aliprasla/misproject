namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccounts", "sale", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankAccounts", "sale");
        }
    }
}
