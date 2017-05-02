using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PraslaBonnerWondwossenFinalProject.Models;
using Microsoft.AspNet.Identity;
using System.Net;

namespace PraslaBonnerWondwossenFinalProject.Controllers
{
    public class StockPortfoliosController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: StockPortfolios
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details()
        {
            AppUser customer = db.Users.Find(User.Identity.GetUserId());
            if (customer == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockPortfolio StockPortfolio = customer.StockPortfolio;
            if (StockPortfolio == null)
            {
                return HttpNotFound();
            }
            //View All Transactions associated with account

            return View(StockPortfolio);
        }

        public ActionResult Create()
        {
            AppUser person = db.Users.Find(User.Identity.GetUserId());
            if (person.isActive == true || person.isActive == false || person.isActive == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("InactiveAccountError", "Account");
            }
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "BankAccountID")]BankAccount StockPortfolio)
        {
            StockPortfolio.Type = AccountTypes.Stock;
            StockPortfolio.Name = "Longorn Stock";
            StockPortfolio.Balance = 0;
            StockPortfolio.Customer = db.Users.Find(User.Identity.GetUserId());
            StockPortfolio.isBalanced = false;
            StockPortfolio.isApproved = false;
            StockPortfolio.CashBalance = 0;
            StockPortfolio.Gains = 0;
            StockPortfolio.Fees = 0;
            StockPortfolio.Bonuses = 0;
            db.Users.Find(User.Identity.GetUserId()).BankAccounts.Add(StockPortfolio);
            db.SaveChanges();

            
            

            if (ModelState.IsValid)
            {
                var item = db.BankAccounts.OrderByDescending(i => i.AccountNumber).FirstOrDefault();
                StockPortfolio.AccountNumber = item.AccountNumber + 1;


                //create a dispute for manager approval
                Dispute now = new Dispute();
                now.Status = Status.WaitingOnManager;
                now.CustomerDescription = "Customer " + User.Identity.Name + "has applied for a stock portfolio. Please approve or deny this deposit.";
                now.DisputeAmount = 0;
                db.Disputes.Add(now);

                AppUser current = db.Users.Find(User.Identity.GetUserId());
                StockPortfolio.Customer = current;
                current.StockPortfolio = (StockPortfolio);
                db.StockPortfolios.Add(StockPortfolio);
                db.SaveChanges();

                return RedirectToAction("Index","Customers");

            }



            return View();


        }
    
    }
}