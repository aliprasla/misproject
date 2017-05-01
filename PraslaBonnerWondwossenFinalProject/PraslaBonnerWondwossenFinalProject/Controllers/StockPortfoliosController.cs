using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PraslaBonnerWondwossenFinalProject.Models;
using Microsoft.AspNet.Identity;

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
        public ActionResult Create([Bind(Include="StockPortfolioID")]StockPortfolio StockPortfolio)
        {
            StockPortfolio.Type = AccountTypes.Stock;
            StockPortfolio.Name = "Longorn Stock";
            StockPortfolio.isBalanced = false;
            StockPortfolio.isApproved = false;
            StockPortfolio.CashBalance = 0;
            StockPortfolio.Balance = 0;

            if(ModelState.IsValid)
            {
                var item = db.BankAccounts.OrderByDescending(i => i.AccountNumber).FirstOrDefault();
                StockPortfolio.AccountNumber = item.AccountNumber + 1;
            }

            //create a dispute for manager approval
            Dispute now = new Dispute();
            now.Status = Status.WaitingOnManager;
            now.CustomerDescription = "Customer " + User.Identity.Name + "has applied for a stock portfolio. Please approve or deny this deposit.";
            now.DisputeAmount = 0;
            db.Disputes.Add(now);

            AppUser current = db.Users.Find(User.Identity.GetUserId());
            StockPortfolio.Customer = current;
            current.StockPortfolios.Add(StockPortfolio);
            db.StockPortfolios.Add(StockPortfolio);
            
      




            return View();
        }
    }
}