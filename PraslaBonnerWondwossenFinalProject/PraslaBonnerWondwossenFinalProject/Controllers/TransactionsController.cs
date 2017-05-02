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
            ViewBag.Selected = transactions.Count();
            ViewBag.All = transactions.Count();
            return View(transactions.ToList());
        }
        [HttpPost]
        public ActionResult Index(string SearchString) {
            var transactions = db.Transactions.Include(t => t.Dispute);
            AppUser current = db.Users.Find(User.Identity.GetUserId());
            ViewBag.All = transactions.Count();
            if (User.IsInRole("Customer"))
            {
                transactions = transactions.Where(t => t.Customer.Email == current.Email);
            }
            if (SearchString == null || SearchString == "")
            {
                ViewBag.Selected = transactions.Count();
                
                return View(transactions.ToList());
            }
            else {
                try
                {
                    Decimal test = Convert.ToDecimal(SearchString);
                    transactions = transactions.Where(c => c.Amount == test);
                    ViewBag.Selected = transactions.Count();
                }
                catch {
                    transactions = transactions.Where(c => c.FromAccount.Name.Contains(SearchString) || c.ToAccount.Name.Contains(SearchString) || c.Description.Contains(SearchString) || c.Type.ToString().Contains(SearchString));
                    ViewBag.Selected = transactions.Count();
                }
                return View(transactions.ToList());
            }
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
        public ActionResult TransferFunds() {
            AppUser current = db.Users.Find(User.Identity.GetUserId());
            ViewBag.AccountList = new SelectList(current.BankAccounts, "BankAccountID", "NameNo");
            ViewBag.Message = "";
            return View();
        }
        [HttpPost]
        public ActionResult TransferFunds([Bind(Include = "Amount,Description")] Transaction transaction, int FBankAccountID, int TBankAccountID)
        {
            if (ModelState.IsValid)
            {
                BankAccount currentB = db.BankAccounts.Find(FBankAccountID);
                AppUser current = db.Users.Find(User.Identity.GetUserId());
                BankAccount currentD = db.BankAccounts.Find(TBankAccountID);
                if (currentB.Balance < transaction.Amount)
                {
                    
                    ViewBag.AccountList = new SelectList(current.BankAccounts, "BankAccountID", "NameNo");
                    ViewBag.Message = "Insufficient Funds in this account for this transaction";
                    return View();
                }
                else if (transaction.Amount < 0)
                {

                    ViewBag.AccountList = new SelectList(current.BankAccounts, "BankAccountID", "NameNo");
                    ViewBag.Message = "Transfer Amount must be a positive number";
                    return View();

                }
                else
                {
                    //TODO: Add Account Status Validation -> Active, Inactive -> Transfer
                    transaction.Customer = current;
                    transaction.Date = DateTime.Now;
                    transaction.FromAccount = currentB;
                    transaction.ToAccount = currentD;
                    transaction.Type = TransactionTypes.Transfer;
                    currentB.Balance -= transaction.Amount;
                    currentD.Balance += transaction.Amount;
                    db.BankAccounts.Where(c => c.BankAccountID == currentB.BankAccountID).ToList().First().Transactions.Add(transaction);
                    //db.BankAccounts.Where(c => c.BankAccountID == currentD.BankAccountID).ToList().First().Transactions.Add(transaction);                    
                    db.SaveChanges();

                }

            }

            return RedirectToAction("Index","Customers");
        }



        [Authorize(Roles = "Customer")]
        public ActionResult Withdraw()
        {

            AppUser current = db.Users.Find(User.Identity.GetUserId());
            ViewBag.AccountList = new SelectList(current.BankAccounts, "BankAccountID", "NameNo");
            ViewBag.Message = "";
            return View();
        }
        [HttpPost]
        public ActionResult Withdraw([Bind(Include = "Amount,Description")] Transaction transaction, int BankAccountID)
        {
            if (ModelState.IsValid) {

                //amount validation
                BankAccount currentB = db.BankAccounts.Find(BankAccountID);
                if (currentB.Balance < transaction.Amount)
                {
                    AppUser current = db.Users.Find(User.Identity.GetUserId());
                    ViewBag.AccountList = new SelectList(current.BankAccounts, "BankAccountID", "NameNo");
                    ViewBag.Message = "Insufficient Funds in this account for this transaction";
                    return View();
                }
                else if (transaction.Amount < 0)
                {
                    AppUser current = db.Users.Find(User.Identity.GetUserId());
                    ViewBag.AccountList = new SelectList(current.BankAccounts, "BankAccountID", "NameNo");
                    ViewBag.Message = "Withdrawal Amount must be a positive number";
                    return View();

                }
                //TODO: Add Account Status Validation -> Active, Inaction -> Withdrawal
                else
                {
                    //IRA Validation
                    if (currentB.Type == AccountTypes.IRA){
                        
                    }
                    transaction.Customer = currentB.Customer;
                    transaction.Type = TransactionTypes.Withdrawal;
                    transaction.Date = DateTime.Now;
                    transaction.FromAccount = currentB;
                    currentB.Balance -= transaction.Amount;
                    transaction.FromAccount = currentB;
                    db.Transactions.Add(transaction);
                    currentB.Transactions.Add(transaction);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Customers");
                }
            }
            return View();
        }
        public decimal maxContributionAmount(BankAccount bankAccount) {
            //getsAllContributions for this account
            var allTransactions = db.Transactions.Where(c => c.ToAccount.BankAccountID == bankAccount.BankAccountID);
            List<Transaction> allContributions = allTransactions.ToList();
            decimal outter = 0;
            foreach (var item in allContributions) {
                outter += item.Amount;
            }
            return (5000 - outter);
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
                BankAccount currentBank = current.BankAccounts.Where(c => c.BankAccountID == BankAccountID).First();
                if (transaction.Amount < 0)
                {
                    //Negative Deposit amount Validation
                    ViewBag.AccountList = new SelectList(current.BankAccounts, "BankAccountID", "NameNo");
                    ViewBag.Message = "Deposit Amount Must Be a Positive Number";
                    return View();
                }
                //TODO: Add Account Status Validation -> Active, Inaction -> Deposit
                else
                {

                    transaction.Customer = current;
                    transaction.Type = TransactionTypes.Deposit;

                    //IRA Validation
                    if (currentBank.Type == AccountTypes.IRA) {
                        decimal maxContributionAllow = maxContributionAmount(currentBank);
                        if (transaction.Amount > maxContributionAllow) {
                            transaction.Amount = maxContributionAllow;
                            transaction.ToAccount = currentBank;
                            currentBank.Balance += transaction.Amount;
                            return View(transaction);
                        }
                    }
                    if (transaction.Amount > 5000)
                    {
                        transaction.Description = transaction.Description + ". Pending Manager Approval. Original Amount = $" + Convert.ToString(transaction.Amount);
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
                        transaction.Amount = 0;
                    }
                    else {
                        currentBank.Balance += transaction.Amount;
                        transaction.ToAccount = currentBank;
                    }
                    transaction.Date = DateTime.Now;
                    currentBank.Transactions.Add(transaction);
                    db.Transactions.Add(transaction);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Customers");
                }
            }
            return View();

        }

        public ActionResult RedirectedIRA(Transaction transaction)
        {
            return View(transaction);
        }
        [HttpPost]
        public ActionResult RedirectedIRA()
        {
            return RedirectToAction("Index","Customers");
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
