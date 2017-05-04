using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PraslaBonnerWondwossenFinalProject.Models;

namespace PraslaBonnerWondwossenFinalProject.Controllers
{
    public class ManagersController : Controller
    {
        private AppDbContext db = new AppDbContext();
        // GET: Managers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddStock()
        {
            return View();
        }

        [HttpPost]
        //add dropdown for type and let manager pick the type
        public ActionResult AddStock([Bind(Include = "StockID,Symbol,Fees,Type")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Stocks.Add(stock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("AddStock");
        }

    }
}