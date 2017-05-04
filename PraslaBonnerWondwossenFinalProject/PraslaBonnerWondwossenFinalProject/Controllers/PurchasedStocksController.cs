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
    public class PurchasedStocksController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: PurchasedStocks
        public ActionResult Index()
        {
            AppUser customer = db.Users.Find(User.Identity.GetUserId());
            return View(customer.StockPortfolio.purchasedstocks.ToList());
        }

        // GET: PurchasedStocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchasedStock purchasedStock = db.PurchasedStocks.Find(id);
            if (purchasedStock == null)
            {
                return HttpNotFound();
            }
            return View(purchasedStock);
        }

        // GET: PurchasedStocks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PurchasedStocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PurchasedStockId,Shares,InitialPrice,TotalFees")] PurchasedStock purchasedStock)
        {
            if (ModelState.IsValid)
            {
                db.PurchasedStocks.Add(purchasedStock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(purchasedStock);
        }

        // GET: PurchasedStocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchasedStock purchasedStock = db.PurchasedStocks.Find(id);
            if (purchasedStock == null)
            {
                return HttpNotFound();
            }
            return View(purchasedStock);
        }

        // POST: PurchasedStocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PurchasedStockId,Shares,InitialPrice,TotalFees")] PurchasedStock purchasedStock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchasedStock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchasedStock);
        }

        // GET: PurchasedStocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchasedStock purchasedStock = db.PurchasedStocks.Find(id);
            if (purchasedStock == null)
            {
                return HttpNotFound();
            }
            return View(purchasedStock);
        }

        // POST: PurchasedStocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchasedStock purchasedStock = db.PurchasedStocks.Find(id);
            db.PurchasedStocks.Remove(purchasedStock);
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
