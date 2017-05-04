namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class justdesserts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccounts", "Gains", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankAccounts", "Gains");
        }
    }
}
