using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PraslaBonnerWondwossenFinalProject.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace PraslaBonnerWondwossenFinalProject.Controllers
{
    public class EmployeesController : Controller
    {
        private AppDbContext db = new AppDbContext();
        
        public ActionResult Index()
        {
            return View();
        }

        // GET: Employees
        public ActionResult List()
        {
            List<AppUser> appUsers = new List<AppUser>();
            appUsers = db.Users.ToList();
            List<AppUser> employees = new List<AppUser>();
            AppUserManager man = new AppUserManager(new UserStore<AppUser>(db));
            foreach (var item in appUsers)
            {
                if (man.IsInRole(item.Id, "Employee"))
                {
                    employees.Add(item);
                }
            }
            //bool to see if uses is in a certain role: User.IsInRole("Customer")
            return View(employees);
            //return View(db.Users.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.Users.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FName,Middle,LName,Address,City,State,Zip,PhoneNumber,Birthday,SSN,isActive,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(appUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appUser);
        }

        // GET: Employees/Edit/5
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

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "Id,Address,City,State,Zip,PhoneNumber")] AppUser person)
        {
            if (ModelState.IsValid)
            {

                //Find associated person
                AppUser personToChange = db.Users.Find(User.Identity.GetUserId());


                //update the rest of the fields
              
                personToChange.Address = person.Address;
                personToChange.City = person.City;
                personToChange.State = person.State;
                personToChange.Zip = person.Zip;
                personToChange.PhoneNumber = person.PhoneNumber;
                db.Entry(personToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(person);
        }



    }
}
