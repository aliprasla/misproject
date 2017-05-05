namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class purchased_stocks : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BankAccounts", "Gains");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BankAccounts", "Gains", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
