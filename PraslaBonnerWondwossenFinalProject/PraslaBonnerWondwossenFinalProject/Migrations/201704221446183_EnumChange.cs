namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnumChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccounts", "AccountNumber", c => c.Int(nullable: false));
            DropColumn("dbo.BankAccounts", "ProductNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BankAccounts", "ProductNumber", c => c.Int(nullable: false));
            DropColumn("dbo.BankAccounts", "AccountNumber");
        }
    }
}
