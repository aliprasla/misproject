using System;

using System.Collections.Generic;

using System.Linq;

using System.Web;

using System.Web.Mvc;

using PraslaBonnerWondwossenFinalProject.Models;

using PraslaBonnerWondwossenFinalProject.StockUtilities;

using Microsoft.AspNet.Identity;

using System.Net;


namespace PraslaBonnerWondwossenFinalProject.Controllers

{

    public class StockController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Home

        public ActionResult Index()

        {

            List<StockQuote> Quotes = new List<StockQuote>();

            StockQuote sq1 = GetQuote.GetStock("AAPL");
            Quotes.Add(sq1);


            StockQuote sq2 = GetQuote.GetStock("GOOG");
            Quotes.Add(sq2);


            StockQuote sq3 = GetQuote.GetStock("AMZN");
            Quotes.Add(sq3);


            StockQuote sq4 = GetQuote.GetStock("LUV");
            Quotes.Add(sq4);



            StockQuote sq5 = GetQuote.GetStock("TXN");
            Quotes.Add(sq5);



            StockQuote sq6 = GetQuote.GetStock("HSY");
            Quotes.Add(sq6);


            StockQuote sq7 = GetQuote.GetStock("V");
            Quotes.Add(sq7);


            StockQuote sq8 = GetQuote.GetStock("NKE");
            Quotes.Add(sq8);

            StockQuote sq9 = GetQuote.GetStock("VWO");
            Quotes.Add(sq9);

            StockQuote sq10 = GetQuote.GetStock("CORN");
            Quotes.Add(sq10);


            StockQuote sq11 = GetQuote.GetStock("OBMCX");
            Quotes.Add(sq11);


            StockQuote sq12 = GetQuote.GetStock("F");
            Quotes.Add(sq12);


            StockQuote sq13 = GetQuote.GetStock("BAC");
            Quotes.Add(sq13);



            StockQuote sq14 = GetQuote.GetStock("VNQ");
            Quotes.Add(sq14);



            StockQuote sq15 = GetQuote.GetStock("NDX");
            Quotes.Add(sq15);


            StockQuote sq16 = GetQuote.GetStock("KMX");
            Quotes.Add(sq16);


            StockQuote sq17 = GetQuote.GetStock("DIA");
            Quotes.Add(sq17);

            StockQuote sq18 = GetQuote.GetStock("SPY");
            Quotes.Add(sq18);

            StockQuote sq19 = GetQuote.GetStock("BEN");
            Quotes.Add(sq19);

            StockQuote sq20 = GetQuote.GetStock("PGSCX");
            Quotes.Add(sq20);

            return View(Quotes);

        }

        //Jessica --what to use instead of db?

        // GET: stocks/Create
        public ActionResult Purchase()
        {
            ViewBag.AllStocks = GetAllStocks();
            ViewBag.AllAccounts = GetAllAccounts();
            return View();
        }

        //POST: stocks/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Purchase([Bind(Include = "Id,Shares,Date")] PurchasedStock stock, Int32 StockID, Int32 BankAccountID)
        {
            //get customer
            AppUser customer = db.Users.Find(User.Identity.GetUserId());
            //get purchased stock
            Stock FoundStock = db.Stocks.Find(StockID);
            //get bank acount to get money from
            BankAccount Account = db.BankAccounts.Find(BankAccountID);

            stock.InitialPrice = Convert.ToDecimal(GetQuote.GetStock(FoundStock.Symbol, DateTime.Parse(Convert.ToString(stock.Date))).LastTradePrice);

            //cash Balance of 0
            if (FoundStock.Fees > customer.StockPortfolio.CashBalance)
            {
                return View("Error");
            }

            //check to see if customer selected a stock portfolio
            if (Account.Type==AccountTypes.Stock)
            {
                //if so, see if balance is adequate
                if((Convert.ToDecimal(stock.Shares * Convert.ToDecimal(GetQuote.GetStock(FoundStock.Symbol,DateTime.Parse(Convert.ToString(stock.Date))).LastTradePrice)))>customer.StockPortfolio.CashBalance)
                {
                    return View("Error");
                }
                else
                {
                    customer.StockPortfolio.CashBalance = customer.StockPortfolio.CashBalance - (Convert.ToDecimal(stock.Shares * Convert.ToDecimal(GetQuote.GetStock(FoundStock.Symbol, DateTime.Parse(Convert.ToString(stock.Date))).LastTradePrice)));
                }
                
            }
            else
            {
                if ((stock.Shares * Convert.ToDecimal(GetQuote.GetStock(FoundStock.Symbol, DateTime.Parse(Convert.ToString(stock.Date))).LastTradePrice))>Account.Balance)
                {
                    return View("Error");
                }
            }
            if (customer.StockPortfolio.purchasedstocks != null)
            {
                foreach (PurchasedStock item in customer.StockPortfolio.purchasedstocks)
                {
                    if (item.stock.StockID == StockID)
                    {
                        //add purchased shares to existing purchased share number
                        item.Shares = item.Shares + stock.Shares;
                        //add to total fees
                        customer.StockPortfolio.Fees = customer.StockPortfolio.Fees + FoundStock.Fees;
                        //if successful, redirect here, must put adequate spot

                        //create new transaction for Fees
                        Transaction TransactionFees1 = new Transaction();
                        TransactionFees1.Date = stock.Date;
                        TransactionFees1.Type = TransactionTypes.Fee;
                        TransactionFees1.Amount = FoundStock.Fees;
                        TransactionFees1.Description = "Fee: " + FoundStock.Name;
                        TransactionFees1.FromAccount = Account;
                        customer.Transactions.Add(TransactionFees1);
                        db.Transactions.Add(TransactionFees1);
                        db.SaveChanges();

                        //create transaction for withdrawl
                        Transaction TransactionWithdrawl1 = new Transaction();
                        TransactionWithdrawl1.Date = stock.Date;
                        TransactionWithdrawl1.Type = TransactionTypes.Withdrawal;
                        TransactionWithdrawl1.Amount = Convert.ToDecimal(stock.Shares * FoundStock.LastPrice);
                        TransactionWithdrawl1.Description = "Stock Purchase - Account " + Account.AccountNumber;
                        TransactionWithdrawl1.FromAccount = Account;
                        customer.Transactions.Add(TransactionWithdrawl1);
                        db.Transactions.Add(TransactionWithdrawl1);
                        db.SaveChanges();


                        return RedirectToAction("Index", "Customers");
                    }
                }
            }
            
            //stock is not present in the portfolio
            customer.StockPortfolio.Fees = customer.StockPortfolio.Fees + FoundStock.Fees;
            //assign stock to purchased stock
            stock.stock = FoundStock;
            //assign stockp
            stock.stockportfolio = customer.StockPortfolio;
            customer.StockPortfolio.purchasedstocks.Add(stock);

            //create new transaction for Fees
            Transaction TransactionFees = new Transaction();
            TransactionFees.Date = stock.Date;
            TransactionFees.Type = TransactionTypes.Fee;
            TransactionFees.Amount = FoundStock.Fees;
            TransactionFees.Description = "Fee: "+FoundStock.Name;
            TransactionFees.FromAccount = Account;
            customer.Transactions.Add(TransactionFees);
            db.Transactions.Add(TransactionFees);
            db.SaveChanges();

            //create transaction for withdrawl
            Transaction TransactionWithdrawl = new Transaction();
            TransactionWithdrawl.Date = stock.Date;
            TransactionWithdrawl.Type = TransactionTypes.Withdrawal;
            TransactionWithdrawl.Amount = Convert.ToDecimal(stock.Shares * FoundStock.LastPrice);
            TransactionWithdrawl.Description = "Stock Purchase - Account " + Account.AccountNumber;
            TransactionWithdrawl.FromAccount = Account;
            customer.Transactions.Add(TransactionWithdrawl);
            db.Transactions.Add(TransactionWithdrawl);
            db.SaveChanges();

            return RedirectToAction("Index","Customers");

        }

    

        public SelectList GetAllStocks()
        {
            var query = from q in db.Stocks
                        orderby q.Symbol
                        select q;

            List<Stock> allStocks = query.ToList();


            SelectList allStockslist = new SelectList(allStocks, "StockID", "display");

            return allStockslist;
        }

        public SelectList GetAllAccounts()
        {
            AppUser customer = db.Users.Find(User.Identity.GetUserId());
            List<BankAccount> allAccounts = customer.BankAccounts;

            SelectList allAccountsList = new SelectList(allAccounts, "BankAccountID", "Name");

            return allAccountsList;
        }
    }

}