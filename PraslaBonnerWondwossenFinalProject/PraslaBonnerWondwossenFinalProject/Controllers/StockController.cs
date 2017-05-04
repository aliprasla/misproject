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
        public ActionResult Purchase(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockQuote stockquote = db.StockQuotes.Find(Id);
            if (stockquote == null)
            {
                return HttpNotFound();
            }
            ViewBag.AllStocks = GetAllStocks();
            return View();
        }

        //POST: stocks/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Purchase([Bind(Include = "Id,Shares")] PurchasedStock stock, Int32 StockID)
        {
            AppUser customer = db.Users.Find(User.Identity.GetUserId());
            Stock FoundStock = db.Stocks.Find(StockID);

            // iterate through purchases to see it this stock already exist

            foreach (PurchasedStock item in customer.StockPortfolio.purchasedstocks)
            {
                if (item.stock.StockID == StockID)
                {
                    //add purchased shares to existing purchased share number
                    item.Shares = item.Shares + stock.Shares;
                    //add to total fees
                    customer.StockPortfolio.Fees = customer.StockPortfolio.Fees + FoundStock.Fees;
                    //if successful, redirect here, must put adequate spot
                    return RedirectToAction("Customer","Index");
                }
            }
            //stock is not present in the portfolio
            customer.StockPortfolio.Fees = customer.StockPortfolio.Fees + FoundStock.Fees;
            stock.InitialPrice = FoundStock.LastPrice;
            //assign stock to purchased stock
            stock.stock = FoundStock;
            //assign stockp
            stock.stockportfolio = customer.StockPortfolio;

            //create new transaction
            Transaction Transaction = new Transaction();


            return View();
        }

        public SelectList GetAllStocks()
        {
            var query = from q in db.Stocks
                        orderby q.Name
                        select q;

            List<Stock> allStocks = query.ToList();


            SelectList allStockslist = new SelectList(allStocks, "StockID", "display");

            return allStockslist;
        }

    }

}