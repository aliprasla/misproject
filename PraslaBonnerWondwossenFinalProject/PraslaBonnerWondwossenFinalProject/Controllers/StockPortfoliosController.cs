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

        public ActionResult Error()
        {
            return View();
        }
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


            return View(StockPortfolio);
        }

        public ActionResult Create()
        {
            AppUser person = db.Users.Find(User.Identity.GetUserId());
            if (person.isActive == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("InactiveAccountError", "Account");
            }
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "BankAccountID")]StockPortfolio StockPortfolio)
        {

            StockPortfolio.Type = AccountTypes.Stock;
            StockPortfolio.Name = "Longorn Stock";
            StockPortfolio.CashBalance = 0;
            StockPortfolio.Customer = db.Users.Find(User.Identity.GetUserId());
            /*
            StockPortfolio.isBalanced = false;
            StockPortfolio.isApproved = false;
            StockPortfolio.CashBalance = 0;
            StockPortfolio.Gains = 0;
            StockPortfolio.Fees = 0;
            StockPortfolio.Bonuses = 0;
            */
            db.Users.Find(User.Identity.GetUserId()).BankAccounts.Add(StockPortfolio);
            db.SaveChanges();

            AppUser user = db.Users.Find(User.Identity.GetUserId());
            if (user.StockPortfolio == null)
            {
                StockPortfolio.Type = AccountTypes.Stock;
                StockPortfolio.Name = "Longorn Stock";
                StockPortfolio.CashBalance = 0;
                StockPortfolio.Customer = db.Users.Find(User.Identity.GetUserId());
                StockPortfolio.isBalanced = false;
                StockPortfolio.isApproved = false;
                StockPortfolio.Gains = 0;
                StockPortfolio.Fees = 0;
                StockPortfolio.Bonuses = 0;
                //StockPortfolio.AccountNumber = Convert.ToInt32((10000000000 + StockPortfolio.BankAccountID));
                db.Users.Find(User.Identity.GetUserId()).BankAccounts.Add(StockPortfolio);
                db.Users.Find(User.Identity.GetUserId()).StockPortfolio = StockPortfolio;


                db.SaveChanges();


                if (ModelState.IsValid)
                {
                    /*
                    var item = db.BankAccounts.OrderByDescending(i => i.AccountNumber).FirstOrDefault();
                    StockPortfolio.AccountNumber = item.AccountNumber + 1;
    =======
    >>>>>>> 32ff7531dd0ca2904ae2ea5d127e8ec56164c429



    <<<<<<< HEAD
                    AppUser current = db.Users.Find(User.Identity.GetUserId());
                    StockPortfolio.Customer = current;
                    current.StockPortfolio = (StockPortfolio);
                    db.StockPortfolios.Add(StockPortfolio);
                    db.SaveChanges();
                    */
                    return RedirectToAction("Index", "Customers");

                    if (ModelState.IsValid)
                    {
                        var item = db.BankAccounts.OrderByDescending(i => i.AccountNumber).FirstOrDefault();
                        StockPortfolio.AccountNumber = item.AccountNumber + 1;



                        //TODO:create a dispute for manager approval
                        //Dispute now = new Dispute();
                        //now.Status = Status.WaitingOnManager;
                        //now.CustomerDescription = "Customer " + User.Identity.Name + "has applied for a stock portfolio. Please approve or deny this deposit.";
                        //now.DisputeAmount = 0;
                        //db.Disputes.Add(now);




                        db.SaveChanges();


                        return RedirectToAction("Index", "Customers");

                    }
                }
                else
                {
                    return RedirectToAction("Error");
                }


                return View();


            }

        }
    }
}