namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class post : DbMigration
    {
        public override void Up()
        {
            /*
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            */
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        BankAccountID = c.Int(nullable: false, identity: true),
                        AccountNumber = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Name = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        isApproved = c.Boolean(),
                        Fees = c.Decimal(precision: 18, scale: 2),
                        Bonuses = c.Decimal(precision: 18, scale: 2),
                        CashBalance = c.Decimal(precision: 18, scale: 2),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Customer_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BankAccountID)
                .ForeignKey("dbo.AspNetUsers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            /*
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FName = c.String(nullable: false),
                        Middle = c.String(),
                        LName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        Zip = c.Int(nullable: false),
                        Birthday = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        SSN = c.String(),
                        isActive = c.Boolean(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        StockPortfolio_BankAccountID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankAccounts", t => t.StockPortfolio_BankAccountID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.StockPortfolio_BankAccountID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Disputes",
                c => new
                    {
                        DisputeID = c.Int(nullable: false),
                        DisputeAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        CustomerDescription = c.String(nullable: false),
                        ManagerDescription = c.String(),
                        AssignedManager_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DisputeID)
                .ForeignKey("dbo.AspNetUsers", t => t.AssignedManager_Id)
                .ForeignKey("dbo.Transactions", t => t.DisputeID)
                .Index(t => t.DisputeID)
                .Index(t => t.AssignedManager_Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        Customer_Id = c.String(maxLength: 128),
                        FromAccount_BankAccountID = c.Int(),
                        ToAccount_BankAccountID = c.Int(),
                        BankAccount_BankAccountID = c.Int(),
                    })
                .PrimaryKey(t => t.TransactionID)
                .ForeignKey("dbo.AspNetUsers", t => t.Customer_Id)
                .ForeignKey("dbo.BankAccounts", t => t.FromAccount_BankAccountID)
                .ForeignKey("dbo.BankAccounts", t => t.ToAccount_BankAccountID)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccount_BankAccountID)
                .Index(t => t.Customer_Id)
                .Index(t => t.FromAccount_BankAccountID)
                .Index(t => t.ToAccount_BankAccountID)
                .Index(t => t.BankAccount_BankAccountID);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            */
            CreateTable(
                "dbo.Payees",
                c => new
                    {
                        PayeeID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        ZipCode = c.Int(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                        BankAccount_BankAccountID = c.Int(),
                    })
                .PrimaryKey(t => t.PayeeID)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccount_BankAccountID)
                .Index(t => t.BankAccount_BankAccountID);
           /*
            CreateTable(
                "dbo.PurchasedStocks",
                c => new
                    {
                        PurchasedStockId = c.Int(nullable: false, identity: true),
                        Shares = c.Int(nullable: false),
                        InitialPrice = c.Double(nullable: false),
                        TotalFees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        stock_StockID = c.Int(),
                        stockportfolio_BankAccountID = c.Int(),
                    })
                .PrimaryKey(t => t.PurchasedStockId)
                .ForeignKey("dbo.Stocks", t => t.stock_StockID)
                .ForeignKey("dbo.BankAccounts", t => t.stockportfolio_BankAccountID)
                .Index(t => t.stock_StockID)
                .Index(t => t.stockportfolio_BankAccountID);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockID = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Fees = c.Int(nullable: false),
                        Symbol = c.String(nullable: false),
                        StockPortfolio_BankAccountID = c.Int(),
                        StockQuote_StockQuoteId = c.Int(),
                        StockType_StockTypeID = c.Int(),
                    })
                .PrimaryKey(t => t.StockID)
                .ForeignKey("dbo.BankAccounts", t => t.StockPortfolio_BankAccountID)
                .ForeignKey("dbo.StockQuotes", t => t.StockQuote_StockQuoteId)
                .ForeignKey("dbo.StockTypes", t => t.StockType_StockTypeID)
                .Index(t => t.StockPortfolio_BankAccountID)
                .Index(t => t.StockQuote_StockQuoteId)
                .Index(t => t.StockType_StockTypeID);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SalesId = c.Int(nullable: false, identity: true),
                        Shares = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                        NetProfit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SharesLeft = c.Int(nullable: false),
                        PurchaseId = c.Int(nullable: false),
                        Name = c.String(),
                        Fees = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SalesId);
            
            CreateTable(
                "dbo.StockQuotes",
                c => new
                    {
                        StockQuoteId = c.Int(nullable: false, identity: true),
                        Symbol = c.String(),
                        Name = c.String(),
                        PreviousClose = c.Double(nullable: false),
                        LastTradePrice = c.Double(nullable: false),
                        Volume = c.Double(nullable: false),
                        StockType_StockTypeID = c.Int(),
                    })
                .PrimaryKey(t => t.StockQuoteId)
                .ForeignKey("dbo.StockTypes", t => t.StockType_StockTypeID)
                .Index(t => t.StockType_StockTypeID);
            
            CreateTable(
                "dbo.StockTypes",
                c => new
                    {
                        StockTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StockTypeID);
            */
            CreateTable(
                "dbo.PayeeAppUsers",
                c => new
                    {
                        Payee_PayeeID = c.Int(nullable: false),
                        AppUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Payee_PayeeID, t.AppUser_Id })
                .ForeignKey("dbo.Payees", t => t.Payee_PayeeID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id, cascadeDelete: true)
                .Index(t => t.Payee_PayeeID)
                .Index(t => t.AppUser_Id);
            
        }
        
        public override void Down()
        {
            /*
            DropForeignKey("dbo.StockQuotes", "StockType_StockTypeID", "dbo.StockTypes");
            DropForeignKey("dbo.Stocks", "StockType_StockTypeID", "dbo.StockTypes");
            DropForeignKey("dbo.Stocks", "StockQuote_StockQuoteId", "dbo.StockQuotes");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Transactions", "BankAccount_BankAccountID", "dbo.BankAccounts");
            DropForeignKey("dbo.Payees", "BankAccount_BankAccountID", "dbo.BankAccounts");
            DropForeignKey("dbo.AspNetUsers", "StockPortfolio_BankAccountID", "dbo.BankAccounts");
            DropForeignKey("dbo.Stocks", "StockPortfolio_BankAccountID", "dbo.BankAccounts");
            DropForeignKey("dbo.PurchasedStocks", "stockportfolio_BankAccountID", "dbo.BankAccounts");
            DropForeignKey("dbo.PurchasedStocks", "stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PayeeAppUsers", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PayeeAppUsers", "Payee_PayeeID", "dbo.Payees");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Disputes", "DisputeID", "dbo.Transactions");
            DropForeignKey("dbo.Transactions", "ToAccount_BankAccountID", "dbo.BankAccounts");
            DropForeignKey("dbo.Transactions", "FromAccount_BankAccountID", "dbo.BankAccounts");
            DropForeignKey("dbo.Transactions", "Customer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Disputes", "AssignedManager_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BankAccounts", "Customer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PayeeAppUsers", new[] { "AppUser_Id" });
            DropIndex("dbo.PayeeAppUsers", new[] { "Payee_PayeeID" });
            DropIndex("dbo.StockQuotes", new[] { "StockType_StockTypeID" });
            DropIndex("dbo.Stocks", new[] { "StockType_StockTypeID" });
            DropIndex("dbo.Stocks", new[] { "StockQuote_StockQuoteId" });
            DropIndex("dbo.Stocks", new[] { "StockPortfolio_BankAccountID" });
            DropIndex("dbo.PurchasedStocks", new[] { "stockportfolio_BankAccountID" });
            DropIndex("dbo.PurchasedStocks", new[] { "stock_StockID" });
            DropIndex("dbo.Payees", new[] { "BankAccount_BankAccountID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Transactions", new[] { "BankAccount_BankAccountID" });
            DropIndex("dbo.Transactions", new[] { "ToAccount_BankAccountID" });
            DropIndex("dbo.Transactions", new[] { "FromAccount_BankAccountID" });
            DropIndex("dbo.Transactions", new[] { "Customer_Id" });
            DropIndex("dbo.Disputes", new[] { "AssignedManager_Id" });
            DropIndex("dbo.Disputes", new[] { "DisputeID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "StockPortfolio_BankAccountID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.BankAccounts", new[] { "Customer_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.PayeeAppUsers");
            DropTable("dbo.StockTypes");
            DropTable("dbo.StockQuotes");
            DropTable("dbo.Sales");
            DropTable("dbo.Stocks");
            DropTable("dbo.PurchasedStocks");
            DropTable("dbo.Payees");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Transactions");
            DropTable("dbo.Disputes");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.BankAccounts");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            */
        }
    }
}
