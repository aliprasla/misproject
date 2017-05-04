using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PraslaBonnerWondwossenFinalProject.Models;
using Microsoft.AspNet.Identity;



namespace PraslaBonnerWondwossenFinalProject.Controllers
{
    public class SellController : Controller
    {
        private AppDbContext db = new AppDbContext();
        // GET: Sell
        public ActionResult Sale(int? id)
        {
            AppUser customer = db.Users.Find(User.Identity.GetUserId());
            PurchasedStock purchasedstock = customer.StockPortfolio.purchasedstocks.Find(c=>c.PurchasedStockId==id);

            return View(purchasedstock);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sale(DateTime SaleDate, Int32 SharesSold, PurchasedStock purchasedstock, Decimal NetProfit, String Name, Int32 SharesLeft, Int32 Fees)
        {
            AppUser customer = db.Users.Find(User.Identity.GetUserId());
            if (purchasedstock.Shares<=SharesSold) { return View("Error"); }
            if (SaleDate<purchasedstock.Date) { return View("Error"); }

            NetProfit = ((Convert.ToDecimal(SharesSold) * Convert.ToDecimal(purchasedstock.stock.LastPrice)) - (Convert.ToDecimal(purchasedstock.Shares) * Convert.ToDecimal(purchasedstock.InitialPrice)));

            //update number of shares left over
            purchasedstock.Shares -= SharesSold;


            Name = purchasedstock.stock.Name;
            SharesLeft = purchasedstock.Shares;
            Fees = purchasedstock.stock.Fees;

            //update fee for this sale
            customer.StockPortfolio.Fees += purchasedstock.stock.Fees;

            //if sold all stocks, delete this purchase
            if (purchasedstock.Shares == 0)
            {
                db.PurchasedStocks.Remove(purchasedstock);
                db.SaveChanges();
            }

            //add the net income from this sale to gains
            customer.StockPortfolio.Gains += NetProfit;

            //TODO:add transaction

            return View("SaleSummary");
        }
        public ActionResult SaleSummary()
        {
            return View();
        }
    }

    
}