using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PraslaBonnerWondwossenFinalProject.Models;
using Microsoft.AspNet.Identity;

namespace PraslaBonnerWondwossenFinalProject.Controllers
{
    public class SalesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Sales
        public ActionResult Index()
        {
            return View(db.Sales.ToList());
        }

       

        // GET: Sales/Create
        public ActionResult Create(int? id)
        { 
        if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(id);
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalesId,Shares,date,NetProfit,SharesLeft,PurchaseId")] Sales sales, int? id)
        {
            

            sales.PurchaseId = Convert.ToInt32(id);

            AppUser customer = db.Users.Find(User.Identity.GetUserId());
            PurchasedStock purchasedstock = customer.StockPortfolio.purchasedstocks.Find(c => c.PurchasedStockId == sales.PurchaseId);
            sales.NetProfit = Convert.ToDecimal(purchasedstock.InitialPrice * purchasedstock.Shares) + (sales.Shares * purchasedstock.stock.LastPrice);
            sales.SharesLeft = purchasedstock.Shares - sales.Shares;

            if (sales.date < purchasedstock.Date) { return View("Error"); }
            if (sales.SharesLeft < 0) { return View("Error"); }
            if (ModelState.IsValid)
            {
                db.Sales.Add(sales);
                db.SaveChanges();
                return RedirectToAction("Details",sales.SalesId);
            }

            return View(sales);
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        [HttpPost]
        public ActionResult Details(([Bind(Include = "SalesId,Shares,date,NetProfit,SharesLeft,PurchaseId")] Sales sales)
        {
            AppUser customer = db.Users.Find(User.Identity.GetUserId());
            PurchasedStock purchasedstock = customer.StockPortfolio.purchasedstocks.Find(c => c.PurchasedStockId == sales.PurchaseId);
            purchasedstock.Shares = sales.SharesLeft;
            purchasedstock.TotalFees += sales.Fees;
            customer.StockPortfolio.CashBalance += sales.NetProfit;

            if (sales.SharesLeft == 0) {
                db.PurchasedStocks.Remove(purchasedstock);
                db.SaveChanges();
            }

            return RedirectToAction("Index")
        }




        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesId,Shares,date,NetProfit,SharesLeft,PurchaseId")] Sales sales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sales);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sales sales = db.Sales.Find(id);
            db.Sales.Remove(sales);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
