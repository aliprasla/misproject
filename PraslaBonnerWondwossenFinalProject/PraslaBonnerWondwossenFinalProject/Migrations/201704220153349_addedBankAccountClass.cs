namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedBankAccountClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        BankAccountID = c.Int(nullable: false, identity: true),
                        AccountNo = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Customer_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.BankAccountID)
                .ForeignKey("dbo.AspNetUsers", t => t.Customer_Id, cascadeDelete: true)
                .Index(t => t.Customer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BankAccounts", "Customer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.BankAccounts", new[] { "Customer_Id" });
            DropTable("dbo.BankAccounts");
        }
    }
}
