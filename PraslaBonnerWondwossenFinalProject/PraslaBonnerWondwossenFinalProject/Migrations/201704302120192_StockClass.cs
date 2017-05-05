namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccounts", "Value", c => c.Int());
            AddColumn("dbo.BankAccounts", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankAccounts", "Discriminator");
            DropColumn("dbo.BankAccounts", "Value");
        }
    }
}
