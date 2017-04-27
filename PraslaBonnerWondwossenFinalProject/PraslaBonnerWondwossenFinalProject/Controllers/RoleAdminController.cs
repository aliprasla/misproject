using System;

using System.Collections.Generic;

using System.Linq;

using System.Web;

using System.Web.Mvc;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNet.Identity;

using System.Data.Entity;

using PraslaBonnerWondwossenFinalProject.Models;

using Microsoft.AspNet.Identity.EntityFramework;

using Microsoft.AspNet.Identity.Owin;



//Change this reference to your project name




namespace PraslaBonnerWondwossenFinalProject.Controllers

{

    public class RoleAdminController : Controller

    {
        private AppDbContext db = new AppDbContext();
        //

        // GET: /RoleAdmin/

        public ActionResult Index()

        {

            return View(RoleManager.Roles);

        }



        public ActionResult Create()

        {

            return View();

        }



        [HttpPost]

        public ActionResult Create([Required] string name)

        {

            if (ModelState.IsValid)

            {

                IdentityResult result = RoleManager.Create(new AppRole(name));



                if (result.Succeeded)

                {

                    return RedirectToAction("Index");

                }

                else

                {

                    AddErrorsFromResult(result);

                }

            }



            //if code gets this far, we need to show an error

            return View(name);

        }



        public ActionResult Edit(string id)

        {

            AppRole role = RoleManager.FindById(id);

            string[] memberIDs = role.Users.Select(x => x.UserId).ToArray();

            IEnumerable<AppUser> members = UserManager.Users.Where(x => memberIDs.Any(y => y == x.Id));

            IEnumerable<AppUser> nonMembers = UserManager.Users.Except(members);

            return View(new RoleEditModel { Role = role, Members = members, NonMembers = nonMembers });

        }



        [HttpPost]

        public ActionResult Edit(RoleModificationModel model)

        {

            IdentityResult result;

            if (ModelState.IsValid)

            {

                foreach (string userId in model.IdsToAdd ?? new string[] { })

                {

                    result = UserManager.AddToRole(userId, model.RoleName);

                    if (!result.Succeeded)

                    {

                        return View("Error", result.Errors);

                    }

                }

                //terminate employee
                foreach (string userId in model.IdsToTerminate ?? new string[] { })

                {
                    AppUser employee = db.Users.Find(userId);
                    employee.isActive = false;
                    result = UserManager.RemoveFromRole(userId, model.RoleName);

                    if (!result.Succeeded)

                    {

                        return View("Error", result.Errors);

                    }

                }



                foreach (string userId in model.IdsToDelete ?? new string[] { })

                {

                    result = UserManager.RemoveFromRole(userId, model.RoleName);

                    if (!result.Succeeded)

                    {

                        return View("Error", result.Errors);

                    }

                }

                return RedirectToAction("Index");

            }

            return View("Error", new string[] { "Role Not Found" });

        }

        //edit employee information
        public ActionResult EditEmployee(string Id)
        {
            AppUser employee = db.Users.Find(Id);
            return View(employee);    
         }

        [HttpPost]
        public ActionResult EditEmployee([Bind(Include = "Id,SSN,FName,LName,Middle,Email,PhoneNumber,Address,City,State,Zip,Birthday")] AppUser employee)
        {
            if (ModelState.IsValid)
            {

                //Find associated person
                AppUser employeeToChange = db.Users.Find(User.Identity.GetUserId());


                //update the rest of the fields
                employeeToChange.FName = employee.FName;
                employeeToChange.LName = employee.LName;
                employeeToChange.Middle = employee.Middle;
                employeeToChange.Address = employee.Address;
                employeeToChange.City = employee.City;
                employeeToChange.State = employee.State;
                employeeToChange.Zip = employee.Zip;
                employeeToChange.PhoneNumber = employee.PhoneNumber;
                employeeToChange.Email = employee.Email;
                employeeToChange.Birthday = employee.Birthday;
                employeeToChange.SSN = employee.SSN;
                db.Entry(employeeToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit");

            }
            return View(employee);
        }







        private void AddErrorsFromResult(IdentityResult result)

        {

            foreach (string error in result.Errors)

            {

                ModelState.AddModelError("", error);

            }

        }



        private AppUserManager UserManager

        {

            get

            {

                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();

            }

        }



        private AppRoleManager RoleManager

        {

            get

            {

                return HttpContext.GetOwinContext().GetUserManager<AppRoleManager>();

            }

        }

    }

}