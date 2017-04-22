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

            BankAccount product = db.BankAccounts.Find(id);

            if (product == null)

            {

                return HttpNotFound();

            }

            return View(product);

        }



        // GET: BankAccounts/Create

        public ActionResult Create()

        {

            return View();

        }



        // POST: BankAccounts/Create

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 

        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "ProductID,ProductNumber,Type,Name,Balance")] BankAccount product)

        {

            if (ModelState.IsValid)

            {

                db.BankAccounts.Add(product);

                db.SaveChanges();

                return RedirectToAction("Index");

            }



            return View(product);

        }



        // GET: BankAccounts/Edit/5

        public ActionResult Edit(int? id)

        {

            if (id == null)

            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            BankAccount product = db.BankAccounts.Find(id);

            if (product == null)

            {

                return HttpNotFound();

            }

            return View(product);

        }



        // POST: BankAccounts/Edit/5

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 

        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "ProductID,ProductNumber,Type,Name,Balance")] BankAccount product)

        {

            if (ModelState.IsValid)

            {

                db.Entry(product).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");

            }

            return View(product);

        }



        // GET: BankAccounts/Delete/5

        public ActionResult Delete(int? id)

        {

            if (id == null)

            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            BankAccount product = db.BankAccounts.Find(id);

            if (product == null)

            {

                return HttpNotFound();

            }

            return View(product);

        }



        // POST: BankAccounts/Delete/5

        [HttpPost, ActionName("Delete")]

        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)

        {

            BankAccount product = db.BankAccounts.Find(id);

            db.BankAccounts.Remove(product);

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