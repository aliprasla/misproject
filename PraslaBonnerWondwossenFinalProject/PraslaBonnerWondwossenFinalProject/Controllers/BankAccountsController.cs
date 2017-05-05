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
using Microsoft.AspNet.Identity.EntityFramework;

namespace PraslaBonnerWondwossenFinalProject.Controllers
{
    public class BankAccountsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: BankAccounts
        public ActionResult Index()
        {
            return View(db.BankAccounts.ToList());
        }

        // GET: BankAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }

            //View All Transactions associated with account
            

            bankAccount.Transactions = db.Transactions.Where(c => c.ToAccount.BankAccountID == bankAccount.BankAccountID || c.FromAccount.BankAccountID == bankAccount.BankAccountID).ToList();
            ViewBag.Selected = bankAccount.Transactions.Count();
            ViewBag.All = bankAccount.Transactions.Count();
            ViewBag.TransactionTypes = GetAllTransactionTypes();
            ViewBag.Transactions = bankAccount.Transactions;
            List<Tuple<string,string>> accountInfo = new List<Tuple<string,string>>();
            foreach (var item in bankAccount.Transactions) {
                try
                {
                    accountInfo.Add(new Tuple<string, string>(item.ToAccount.NameNo, item.FromAccount.NameNo));
                }
                catch {
                    try
                    {
                        accountInfo.Add(new Tuple<string, string>(item.ToAccount.NameNo, " "));
                    }
                    catch {
                        accountInfo.Add(new Tuple<string, string>(" ", item.FromAccount.NameNo));
                    }
                }
            }
            ViewBag.accountInfo = accountInfo;
            return View(bankAccount);
        }

        [HttpPost]
        public ActionResult Details(int? id,String SearchString, String SelectedType, String SelectedRange, String RangeStringBeg, String RangeStringEnd, String TransactionID, String Dates, String DateString) {
            var query = (from t in db.Transactions select t);
            AppUser current = db.Users.Find(User.Identity.GetUserId());
            if (User.IsInRole("Customer")) {
                query = query.Where(t => t.Customer.Id == current.Id);
            }
            if(SearchString != ""){
                query = query.Where(t => t.Description.Contains(SearchString));
            }
            if (SelectedType != "All") {
                var currentType = (TransactionTypes)Enum.Parse(typeof(TransactionTypes), SelectedType);
                query = query.Where(t => t.Type == currentType);
            }
            //At least one has value
            if ((SelectedRange != "All" ) || (RangeStringBeg != "") || ( RangeStringEnd != "")) {

                //if both custom and Selected have values
                if ((SelectedRange != "All" ) && ((RangeStringBeg != "" ) && (RangeStringEnd != "" )))
                {
                    decimal beg = Convert.ToDecimal(RangeStringBeg);
                    decimal end = Convert.ToDecimal(RangeStringEnd);
                    query = query.Where(c => c.Amount >= beg && c.Amount <= end);
                }
                //Range not filled out
                else if ((RangeStringBeg != "" && RangeStringEnd == "") || (RangeStringBeg == "" && RangeStringEnd != ""))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else if (SelectedRange != "All")
                {
                    int beg = 0;
                    int end = 0;
                    //Which Range Selected
                    if (SelectedRange == "0-100")
                    {
                        beg = 0;
                        end = 100;
                    }
                    else if (SelectedRange == "100-200")
                    {
                        beg = 100;
                        end = 200;
                    }
                    else if (SelectedRange == "200-300")
                    {
                        beg = 200;
                        end = 300;
                    }
                    else if (SelectedRange == "300+")
                    {
                        beg = 300;
                        end = 999999999;
                    }
                    query = query.Where(c => c.Amount >= beg && c.Amount <= end);
                }
                else {
                    try
                    {
                        Decimal beg = Convert.ToDecimal(RangeStringBeg);
                        Decimal end = Convert.ToDecimal(RangeStringEnd);
                        query = query.Where(c => c.Amount >= beg && c.Amount <= end);
                    }
                    catch {

                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                }

            }
            if (TransactionID != "")
            {

                try
                {
                    int SearchId = Convert.ToInt32(TransactionID);
                    query = query.Where(c => c.TransactionID == SearchId);
                }
                catch  {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            //Dates
            //One of the data parameters has value
            if (Dates != "All"  || DateString != "" ) {
                if (Dates != "All")
                {

                    try
                    {
                        int sp = Convert.ToInt32(Dates);
                        DateTime comp = DateTime.Now.AddDays(-sp);
                        query = query.Where(c => c.Date >= comp);
                    }
                    catch 
                    {

                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                        DateTime comp = DateTime.Now.AddDays(-Convert.ToInt32(DateString));
                        query = query.Where(c => c.Date >= comp);

                }
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            List<Transaction> trans = query.ToList();
            ViewBag.Transactions = query.ToList();
            ViewBag.Selected = query.ToList().Count();
            String userId = User.Identity.GetUserId();
            ViewBag.All = db.Transactions.Where(c => c.Customer.Id == userId).Count();
            ViewBag.TransactionTypes = GetAllTransactionTypes();
            List<Tuple<string, string>> accountInfo = new List<Tuple<string, string>>();
            foreach (var item in trans)
            {
                try
                {
                    accountInfo.Add(new Tuple<string, string>(item.ToAccount.NameNo, item.FromAccount.NameNo));
                }
                catch
                {
                    try
                    {
                        accountInfo.Add(new Tuple<string, string>(item.ToAccount.NameNo, " "));
                    }
                    catch
                    {
                        try
                        {
                            accountInfo.Add(new Tuple<string, string>(" ", item.FromAccount.NameNo));
                        }
                        catch {
                            accountInfo.Add(new Tuple<string, string>(" ", " "));
                        }
                    }
                }
            }
            ViewBag.accountInfo = accountInfo;
            return View(db.BankAccounts.Find(id));

        }

        public SelectList GetAllTransactionTypes() {
            IEnumerable<TransactionTypes> list = Enum.GetValues(typeof(TransactionTypes)).Cast<TransactionTypes>();
            List<string> allTypes = new List<string>();
            allTypes.Add("All");
            foreach (var item in list) {
                allTypes.Add(item.ToString());
            }
            return new SelectList(allTypes, "All");

        }



        // GET: BankAccounts/Create
        [Authorize(Roles = "Customer")]
        public ActionResult Create()
        {
            AppUser person = db.Users.Find(User.Identity.GetUserId());
            if (person.isActive == true)
            {
                ViewBag.Age = person.Age;
                ViewBag.Message = "";
                return View();
            }
            else
            {
                return RedirectToAction("InactiveAccountError", "Account");
            }

        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BankAccountID,Type,Balance")] BankAccount bankAccount)
        {
                ViewBag.Message = "";
                if (ModelState.IsValid)
                {
                    AppUser current = db.Users.Find(User.Identity.GetUserId());
                    db.BankAccounts.Include(c => c.Transactions);
                    db.Transactions.Include(c => c.ToAccount);
                    db.Transactions.Include(c => c.FromAccount);
                    if ((int)bankAccount.Type == 2)
                    {
                    //IRA validation - if age < 70 no contributions allowed
                        int age = current.Age;
                        if (age > 70)
                        {
                            ViewBag.Message = "A contribution to an IRA account cannot be made as you are above the age requirements";
                            return View();
                        //contribution limit is 5000.
                         } else if (bankAccount.Balance > 5000) {
                            ViewBag.Message = "The maximum contribution for a year is $5000. Please contribute a lower amount.";
                            return View();
                         }
                    }
                    //assigns account number
                    var item = db.BankAccounts.OrderByDescending(i => i.AccountNumber).FirstOrDefault();
                    bankAccount.AccountNumber = item.AccountNumber + 1;
                    //default names
                    if ((int)bankAccount.Type == 0)
                    {
                        bankAccount.Name = "Longhorn Savings";
                    }
                    else if ((int)bankAccount.Type == 1)
                    {
                        bankAccount.Name = "Longhorn Checking";
                    }
                    else if ((int)bankAccount.Type == 2)
                    {
                        bankAccount.Name = "Longhorn IRA";
                    }
                    else if ((int)bankAccount.Type == 3)
                    {
                        bankAccount.Name = "Longhorn Stock";
                    }
                    Decimal originalDepo = bankAccount.Balance;
                    Dispute now = null;
                    String transactionDescrip;
                if (bankAccount.Balance > 5000)
                {
                    bankAccount.Balance = 0;
                    //create dispute
                    now = new Dispute()
                    {
                        Status = Status.WaitingOnManager,
                        CustomerDescription = "Customer " + User.Identity.Name + "has submitted a deposit of " + String.Format("{0:C}", Convert.ToString(originalDepo)) + ". Please approve or deny this deposit.",
                        DisputeAmount = originalDepo
                        //TODO: Assign to Manager

                    };
                    transactionDescrip = "Account Opening Initial Deposit of $" + Convert.ToString(originalDepo) + " Waiting on Manager approval.";
                }
                else if (bankAccount.Type == AccountTypes.Stock) {
                    bankAccount.Balance = 0;
                    now = new Dispute()
                    {
                        Status = Status.WaitingOnManager,
                        CustomerDescription = "Customer " + User.Identity.Name + "has submitted an application for a stock portfolio. Waiting on manager approval.",
                        DisputeAmount = originalDepo
                    };
                    transactionDescrip = "Stock Account opening Initial Deposit : " + Convert.ToString(originalDepo) + ". Needs Manager Approval."
                    }
                else
                {
                    transactionDescrip = "Initial Deposit of $" + Convert.ToString(originalDepo) + "";
                }   
                    //create transaction
                    Transaction deposit = new Transaction()
                    {
                        Date = DateTime.Now,
                        Type = TransactionTypes.Deposit,
                        Amount = bankAccount.Balance,
                        Description = transactionDescrip,
                        Customer = current,
                        Dispute = now,
                        ToAccount = bankAccount
                    };
                //TODO: URGENT - Initial Depost not beeing added to bankAccount

                //adds objects to databases 

                    bankAccount.Transactions.Add(deposit);

                    bankAccount.Customer = current;
                    current.BankAccounts.Add(bankAccount);
                
                    db.BankAccounts.Add(bankAccount);
                    db.Transactions.Add(deposit);
                    if (now != null)
                    {
                        db.Disputes.Add(now);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index", "Customers");
                }

                return View(bankAccount);
            
        }

        // GET: BankAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BankAccountID,Name")] BankAccount bankAccount)
        {
            AppUser person = db.Users.Find(User.Identity.GetUserId());
            if (person.isActive == true)
            {
                if (ModelState.IsValid)
                {
                    BankAccount current = db.BankAccounts.Find(bankAccount.BankAccountID);
                    current.Name = bankAccount.Name;
                    db.SaveChanges();
                    return RedirectToAction("Index","Customers");
                }
                return View(bankAccount);
            }
            else
            {
                
                return RedirectToAction("InactiveAccountError", "Account");
            }
        }

        // GET: BankAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BankAccount bankAccount = db.BankAccounts.Find(id);
            db.BankAccounts.Remove(bankAccount);
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
