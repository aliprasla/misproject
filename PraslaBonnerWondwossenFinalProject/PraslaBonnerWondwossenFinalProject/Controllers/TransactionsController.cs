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
    public class TransactionsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Transactions
        [Authorize(Roles = "Customer,Manager,Employee")]
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Dispute);
            AppUser current = db.Users.Find(User.Identity.GetUserId());
            
            if (User.IsInRole("Customer"))
            {
                transactions = transactions.Where(t => t.Customer.Email == current.Email);
            }
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.TransactionID = new SelectList(db.Disputes, "DisputeID", "CustomerDescription");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransactionID,Date,Type,Amount,Description")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TransactionID = new SelectList(db.Disputes, "DisputeID", "CustomerDescription", transaction.TransactionID);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.TransactionID = new SelectList(db.Disputes, "DisputeID", "CustomerDescription", transaction.TransactionID);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionID,Date,Type,Amount,Description")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TransactionID = new SelectList(db.Disputes, "DisputeID", "CustomerDescription", transaction.TransactionID);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }
        [Authorize(Roles = "Customer")]
        public ActionResult Deposit() {

            AppUser current = db.Users.Find(User.Identity.GetUserId());
            ViewBag.AccountList = new SelectList(current.BankAccounts, "BankAccountID", "NameNo");
            ViewBag.Message = "";
            return View();
        }
        [HttpPost]
        public ActionResult Deposit([Bind(Include = "Amount,Description")] Transaction transaction,int BankAccountID)
        {
            if (ModelState.IsValid)
            {
                AppUser current = db.Users.Find(User.Identity.GetUserId());
                if (transaction.Amount < 0)
                {
                    //Negative Deposit amount Validation
                    ViewBag.AccountList = new SelectList(current.BankAccounts, "BankAccountID", "NameNo");
                    ViewBag.Message = "Deposit Amount Must Be a Positive Number";
                    return View();
                }
                else {
                    transaction.Customer = current;
                    transaction.Type = TransactionTypes.Deposit;
                    if (transaction.Amount > 5000)
                    {
                        transaction.Description = transaction.Description + ". Pending Manager Approval. Original Amount = $" + Convert.ToString(transaction.Amount);
                        transaction.Amount = 0;
                        transaction.ToAccount = current.BankAccounts.Find(x => x.BankAccountID == BankAccountID);
                        //Create dispute
                        Dispute now = new Dispute()
                        {
                            Status = Status.WaitingOnManager,
                            CustomerDescription = "Customer " + User.Identity.Name + "has submitted a deposit of " + String.Format("{0:C}", Convert.ToString(transaction.Amount)) + ". Please approve or deny this deposit.",
                            DisputeAmount = transaction.Amount,
                            Transaction = transaction
                            //TODO: Assign to Manager

                        };
                        db.Disputes.Add(now);
                    }
                    else {
                        current.BankAccounts.Find(x => x.BankAccountID == BankAccountID).Balance += transaction.Amount;
                        transaction.ToAccount = current.BankAccounts.Find(x => x.BankAccountID == BankAccountID);
                    }
                    transaction.Date = DateTime.Now;
                    db.Transactions.Add(transaction);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Customers");
                }
            }
            return View();

        }



        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
