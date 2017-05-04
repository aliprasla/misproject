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
using System.Web.Routing;

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
            transactions = transactions.OrderBy(c => c.Date);
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
            //Similar Transactions:
            BankAccount fromAccount = transaction.FromAccount;
            BankAccount toAccount = transaction.ToAccount;
            var query = (from c in db.Transactions select c);
            query = query.Where(c => c.Type == transaction.Type);
            if (transaction.Type == TransactionTypes.Deposit)
            {
                query = query.Where(c => c.ToAccount.BankAccountID.Equals(toAccount.BankAccountID));
            }
            else if (transaction.Type == TransactionTypes.Withdrawal)
            {
                query = query.Where(c => c.FromAccount.BankAccountID == fromAccount.BankAccountID);

            }
            else if (transaction.Type == TransactionTypes.Transfer)
            {
                query = query.Where(c => c.ToAccount.BankAccountID == toAccount.BankAccountID || c.FromAccount.BankAccountID == fromAccount.BankAccountID);
            }
            else if (transaction.Type == TransactionTypes.Fee) {
                query = query.Where(c => c.FromAccount.BankAccountID == fromAccount.BankAccountID);
            }
            
            //List <Transaction> queryList = query.ToList();
            //List<Transaction> transList = new List<Transaction>();
            ViewBag.SimilarTransactions = query.Take(5).ToList();
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
                    //IRA Validation
                    if (currentB.Type == AccountTypes.IRA || currentD.Type == AccountTypes.IRA)
                    {
                        if (currentB.Type == AccountTypes.IRA)
                        {
                            //if age < 65, you cannot withdraw more than 3000 bucks
                            if (current.Age < 65 && transaction.Amount > 3000)
                            {
                                //TODO: if time,figure out why whole viewbag not showing
                                ViewBag.AccountList = new SelectList(current.BankAccounts, "BankAccountID", "NameNo");
                                ViewBag.Message = "Unqualified transaction. Max withdrawal: $3000";
                                return View();
                            }
                            else if (current.Age < 65)
                            {
                                transaction.FromAccount = currentB;
                                transaction.Date = DateTime.Now;
                                transaction.Customer = currentB.Customer;
                                transaction.Type = TransactionTypes.Withdrawal;
                                transaction.ToAccount = currentD;
                                currentB.Balance -= transaction.Amount;
                                db.Transactions.Add(transaction);
                                db.SaveChanges();


                                Transaction trans = db.Transactions.Where(c => c.Description == transaction.Description && c.Customer.Id == transaction.Customer.Id && c.Amount == transaction.Amount).ToList().First();


                                Transaction fee = new Transaction()
                                {
                                    Date = transaction.Date,
                                    Type = TransactionTypes.Fee,
                                    Amount = 30,
                                    Description = "Unqualified IRA withdrawal fee",
                                    Customer = current,
                                    FromAccount = currentB
                                };
                                currentB.Transactions.Add(fee);
                                currentD.Transactions.Add(transaction);
                                currentB.Balance -= fee.Amount;
                                db.Transactions.Add(fee);
                                db.SaveChanges();


                                return RedirectToAction("RedirectedWithdrawal", new { transactionID = trans.TransactionID });
                            }
                        }
                        else {
                            decimal maxContributionAllow = maxContributionAmount(currentD);
                            if (transaction.Amount > maxContributionAllow)
                            {
                                transaction.Amount = maxContributionAllow;
                                transaction.ToAccount = currentD;
                                transaction.Date = DateTime.Now;
                                currentD.Balance += transaction.Amount;
                                db.Transactions.Add(transaction);
                                db.SaveChanges();
                                Transaction trans = db.Transactions.Where(c => c.Description == transaction.Description && c.Customer.Id == transaction.Customer.Id && c.Amount == transaction.Amount).ToList().First();
                                return RedirectToAction("RedirectedIRA", new { transactionID = trans.TransactionID });
                            }

                        }

                    }
                    //TODO: Add Account Status Validation -> Active, Inactive -> Transfer
                    transaction.Customer = current;
                    transaction.Date = DateTime.Now;
                    transaction.FromAccount = currentB;
                    transaction.ToAccount = currentD;
                    transaction.Type = TransactionTypes.Transfer;
                    currentB.Balance -= transaction.Amount;
                    currentD.Balance += transaction.Amount;
                    //adds transaction to bank account
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
            //Pass through text list of IRA's name and NO
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
                AppUser current = db.Users.Find(User.Identity.GetUserId());
                BankAccount currentB = db.BankAccounts.Find(BankAccountID);
                if (currentB.Balance < transaction.Amount)
                {
                    
                    ViewBag.AccountList = new SelectList(current.BankAccounts, "BankAccountID", "NameNo");
                    ViewBag.Message = "Insufficient Funds in this account for this transaction";
                    return View();
                }
                else if (transaction.Amount < 0)
                {
                    ViewBag.AccountList = new SelectList(current.BankAccounts, "BankAccountID", "NameNo");
                    ViewBag.Message = "Withdrawal Amount must be a positive number";
                    return View();

                }
                //TODO: Add Account Status Validation -> Active, Inaction -> Withdrawal
                else
                {
                    //IRA Validation
                    if (currentB.Type == AccountTypes.IRA){

                        //if age < 65, you cannot withdraw more than 3000 bucks
                        if (current.Age < 65 && transaction.Amount > 3000)
                        {
                            //TODO: if time,figure out why whole viewbag not showing
                            ViewBag.AccountList = new SelectList(current.BankAccounts, "BankAccountID", "NameNo");
                            ViewBag.Message = "Unqualified transaction. Max withdrawal: $3000";
                            return View();
                        }
                        else if (current.Age < 65){
                            transaction.FromAccount = currentB;
                            transaction.Date = DateTime.Now;
                            transaction.Customer = currentB.Customer;
                            transaction.Type = TransactionTypes.Withdrawal;
                            currentB.Balance -= transaction.Amount;
                            db.Transactions.Add(transaction);
                            db.SaveChanges();
                            Transaction trans = db.Transactions.Where(c => c.Description == transaction.Description && c.Customer.Id == transaction.Customer.Id && c.Amount == transaction.Amount).ToList().First();


                            Transaction fee = new Transaction()
                            {
                                Date = transaction.Date,
                                Type = TransactionTypes.Fee,
                                Amount = 30,
                                Description = "Unqualified IRA withdrawal fee",
                                Customer = current,
                                FromAccount = currentB
                            };
                            currentB.Transactions.Add(fee);
                            currentB.Balance -= fee.Amount;
                            db.Transactions.Add(fee);
                            db.SaveChanges();


                            return RedirectToAction("RedirectedWithdrawal", new { transactionID = trans.TransactionID });
                        }


                    }
                    transaction.Customer = currentB.Customer;
                    transaction.Type = TransactionTypes.Withdrawal;
                    transaction.Date = DateTime.Now;
                    transaction.FromAccount = currentB;
                    currentB.Balance -= transaction.Amount;
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
            if (5000- outter < 0) {
                return 0;
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
        public ActionResult Deposit([Bind(Include = "TransactionID,Amount,Description")] Transaction transaction,int BankAccountID)
        {
            if (ModelState.IsValid)
            {
                AppUser currentUser = db.Users.Find(User.Identity.GetUserId());
                BankAccount currentBank = currentUser.BankAccounts.Where(c => c.BankAccountID == BankAccountID).First();
                if (transaction.Amount < 0)
                {
                    //Negative Deposit amount Validation
                    ViewBag.AccountList = new SelectList(currentUser.BankAccounts, "BankAccountID", "NameNo");
                    ViewBag.Message = "Deposit Amount Must Be a Positive Number";
                    return View();
                }
                //TODO: Add Account Status Validation -> Active, Inaction -> Deposit
                else
                {

                    transaction.Customer = currentUser;
                    transaction.Type = TransactionTypes.Deposit;

                    //IRA Validation
                    if (currentBank.Type == AccountTypes.IRA)
                    {
                        decimal maxContributionAllow = maxContributionAmount(currentBank);
                        if (transaction.Amount > maxContributionAllow)
                        {
                            transaction.Amount = maxContributionAllow;
                            transaction.ToAccount = currentBank;
                            transaction.Date = DateTime.Now;
                            currentBank.Balance += transaction.Amount;
                            db.Transactions.Add(transaction);
                            db.SaveChanges(); 
                            Transaction trans = db.Transactions.Where(c => c.Description == transaction.Description && c.Customer.Id == transaction.Customer.Id && c.Amount == transaction.Amount).ToList().First();
                            return RedirectToAction("RedirectedIRA",new { transactionID = trans.TransactionID });
                        }
                    }



                    if (transaction.Amount > 5000)
                    {
                        transaction.Description = transaction.Description + ". Pending Manager Approval. Original Amount = $" + Convert.ToString(transaction.Amount);
                        transaction.ToAccount = currentUser.BankAccounts.Find(x => x.BankAccountID == BankAccountID);
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

                        transaction.Date = DateTime.Now;


                        currentBank.Transactions.Add(transaction);
                        
                        db.Transactions.Add(transaction);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Customers");
                    }
                }
            }
            return View();

        }
        [HttpGet]
        public ActionResult RedirectedIRA(int? transactionID)
        {
            if (transactionID == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(transactionID);
            return View(transaction);
        }
        [HttpPost]
        public ActionResult RedirectedIRA([Bind(Include = "TransactionID,Date,Type,Amount,Description")] Transaction transaction)
        {
            return RedirectToAction("Index","Customers");
        }
        [HttpGet]
        public ActionResult RedirectedWithdrawal(int? transactionID)
        {
            if (transactionID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(transactionID);
            return View(transaction);
        }


        [HttpPost]
        public ActionResult RedirectedWithdrawal([Bind(Include = "TransactionID,Date,Type,Amount,Description")] Transaction transaction)
        {
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Subtract(String  type, int id) {

            if (type == "Withdrawal") {
                db.Transactions.Find(id).Amount -= 30;
                db.Transactions.Find(id).FromAccount.Balance += 30;
            }
            db.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }



        public ActionResult Back(String type, int? id) {
            //Note: The Hacks.
            if (type == "Deposit")
            {
                (from y in db.Transactions orderby y.Date descending select y).FirstOrDefault().ToAccount.Balance -= (from y in db.Transactions orderby y.Date descending select y).FirstOrDefault().Amount;
            }
            else if (type == "Withdrawal")
            {

                //Step 1: Reset Accounts
                db.Transactions.Find(id).FromAccount.Balance += 30;
                db.Transactions.Find(id).FromAccount.Balance += db.Transactions.Find(id).Amount;

                //Step 2: Remove Added Transactions
                //2a. Remove Fee - 
                Transaction from = db.Transactions.Find(id);
                var query = db.Transactions.Where(c => c.FromAccount.BankAccountID == from.FromAccount.BankAccountID);
                query = query.Where(c => c.Amount == 30);
                query = query.OrderByDescending(c => c.Date);
                db.Transactions.Remove(query.ToList().FirstOrDefault());

                
            }
            else if (type == "Transfer")
            {
                   //TODO: IRA TRANSFERS BACK

            }
            else {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            db.Transactions.Remove(db.Transactions.Find(id));
            db.SaveChanges();
            return RedirectToAction("Deposit");
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
        
        
        
        /*
        public ActionResult Search(string? SearchString) {



        }
        */





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
