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
    public class PayeesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Payees
        public ActionResult Index()
        {
            return View(db.Payees.ToList());
        }

        // GET: Payees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payee payee = db.Payees.Find(id);
            if (payee == null)
            {
                return HttpNotFound();
            }
            return View(payee);
        }

        // GET: Payees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PayeeID,Name,Address,City,State,ZipCode,PhoneNumber,Type")] Payee payee)
        {
            if (ModelState.IsValid)
            {
                db.Payees.Add(payee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(payee);
        }

        // GET: Payees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payee payee = db.Payees.Find(id);
            if (payee == null)
            {
                return HttpNotFound();
            }
            return View(payee);
        }

        // POST: Payees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PayeeID,Name,Address,City,State,ZipCode,PhoneNumber,Type")] Payee payee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payee);
        }

        // GET: Payees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payee payee = db.Payees.Find(id);
            if (payee == null)
            {
                return HttpNotFound();
            }
            return View(payee);
        }

        // POST: Payees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payee payee = db.Payees.Find(id);
            db.Payees.Remove(payee);
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
        public ActionResult AddPayee() {
            AppUser current = db.Users.Find(User.Identity.GetUserId());
            var payees = from c in db.Payees select c;
            List<Payee> outter = new List<Payee>();
            foreach( var item in payees.ToList())
            {
                if (current.Payees.Contains(item))
                {
                    continue;
                }
                else {

                    outter.Add(item);
                }
            }
            if (current.Payees.Count() == 0)
            {
                ViewBag.Message = "You must select Payees to Add to your account";
            }
            ViewBag.AllPayees = new SelectList(outter, "Name", "PayeeID");
            return View();

        }
        [HttpPost]
        public ActionResult AddPayee(int SelectedPayee) {
            AppUser current = db.Users.Find(User.Identity.GetUserId());
            
            var payees = from c in db.Payees select c;
            List<Payee> outter = new List<Payee>();
            foreach (var item in payees.ToList())
            {
                if (current.Payees.Contains(item))
                {
                    continue;
                }
                else
                {

                    outter.Add(item);
                }
            }
            Payee selected = outter[SelectedPayee];
            current.Payees.Add(selected);
            db.SaveChanges();
            return RedirectToAction("Payment");
        }
        [HttpGet]
        public ActionResult Payment() {
            AppUser current = db.Users.Find(User.Identity.GetUserId());
            if (current.Payees.Count() == 0)
            {

                return RedirectToAction("AddPayee");
            }
            else {
                List<BankAccount> outter = new List<BankAccount>();
                foreach(BankAccount account in current.BankAccounts)
                {
                    if (account.Type == AccountTypes.Savings || account.Type == AccountTypes.Checking) {
                        outter.Add(account);
                    }

                }
                ViewBag.Message = "";
                ViewBag.Accounts = new SelectList(outter, "NameNo", "BankAccountID");
                return View();
            }
        }

        [HttpPost]
        public ActionResult Payment(String Date, int SelectedAccount,int SelectedPayee, decimal Amount, string description) {
            AppUser current = db.Users.Find(User.Identity.GetUserId());
            BankAccount currentBank = db.BankAccounts.Find(SelectedAccount);
            List<BankAccount> outter = new List<BankAccount>();
            foreach (BankAccount account in current.BankAccounts)
            {
                if (account.Type == AccountTypes.Savings || account.Type == AccountTypes.Checking)
                {
                    outter.Add(account);
                }
            }
            if (Amount < 0)
            {
                ViewBag.Message = "You must enter a positive number for payment";
                ViewBag.Accounts = new SelectList(outter, "NameNo", "BankAccountID");
                return View();
            }
            else if (DateTime.Parse(Date) < DateTime.Now)
            {

                ViewBag.Message = "You cannot make a Payment in the past";
                ViewBag.Accounts = new SelectList(outter, "NameNo", "BankAccountID");
                return View();
            }
            //if overdraft
            else if (Amount > currentBank.Balance && Amount < currentBank.Balance + 50)
            {
                currentBank.Balance -= 30;
                Transaction overdraftfee = new Transaction() {
                        Date = DateTime.Now,
                        Type = TransactionTypes.Fee,
                        Amount = 30,
                        Description = "Overdraft Fee",
                        Customer = current,
                        FromAccount = currentBank};
                currentBank.Transactions.Add(overdraftfee);
                db.Transactions.Add(overdraftfee);
                db.SaveChanges();

            }else if (Amount > currentBank.Balance + 50) {
                ViewBag.Message = "You have exceed the overdraft limit with this transaction";
                ViewBag.Accounts = new SelectList(outter, "NameNo", "BankAccountID");
                return View();
            }
            //regular
            currentBank.Balance -= Amount;
            Transaction trans = new Transaction()
            {
                Date = DateTime.Parse(Date),
                Type = TransactionTypes.Withdrawal,
                Amount = Amount,
                Description = description,
                Customer = current,
                FromAccount = currentBank
            };
            currentBank.Transactions.Add(trans);
            db.Transactions.Add(trans);
            return RedirectToAction("Index", "Customers");
            }
            
        }
    }

