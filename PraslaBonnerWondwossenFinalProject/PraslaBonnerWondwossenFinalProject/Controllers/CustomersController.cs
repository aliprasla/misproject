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
using System.Web.Security;

namespace PraslaBonnerWondwossenFinalProject.Controllers
{
    public class CustomersController : Controller
    {
        private AppDbContext db = new AppDbContext();
        [Authorize(Roles = "Customer")]
        public ActionResult Index()
        {
            AppUser AppUser = db.Users.Find(User.Identity.GetUserId());
            if (AppUser.BankAccounts.Count() == 0)
            {
                ViewBag.hasAccounts = false;
            }
            else {
                ViewBag.hasAccounts = true;
            }
            return View(AppUser);
        }

        // GET: Customers
        public ActionResult List()
        {
            return View(Roles.GetUsersInRole("Customer"));
            //return View(db.Users.ToList());
        }

        // GET: persons/Edit/5
        [Authorize(Roles = "Customer,Manager,Employee")]
        public ActionResult Edit()
        {
            string id = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (user.Id != User.Identity.GetUserId() && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }
            return View(user);
        }

        // POST: persons/Edit/5


            //Customers/Edits allows users to update their own profiles
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FName,LName,Middle,Email,Phone,Address,City,State,Zip,Birthday")] AppUser person)
        {
            if (ModelState.IsValid)
            {
                //Find associated person
                AppUser personToChange = db.Users.Find(User.Identity.GetUserId());


                //update the rest of the fields
                personToChange.FName = person.FName;
                personToChange.LName = person.LName;
                personToChange.Middle = person.Middle;
                personToChange.Address = person.Address;
                personToChange.City = person.City;
                personToChange.State = person.State;
                personToChange.Zip = person.Zip;
                personToChange.PhoneNumber = person.PhoneNumber;
                personToChange.Email = person.Email;
                personToChange.Birthday = person.Birthday;
                db.Entry(personToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }
        public ActionResult Details(string Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser user = db.Users.Find(Id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


    }
}