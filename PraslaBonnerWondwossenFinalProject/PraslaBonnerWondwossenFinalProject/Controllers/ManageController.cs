using System;

using System.Linq;

using System.Threading.Tasks;

using System.Web;

using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using Microsoft.AspNet.Identity.Owin;

using Microsoft.Owin.Security;

using PraslaBonnerWondwossenFinalProject.Models;



namespace PraslaBonnerWondwossenFinalProject.Controllers

{

    [Authorize]

    public class ManageController : Controller

    {
        private AppDbContext db = new AppDbContext();

        private ApplicationSignInManager _signInManager;

        private AppUserManager _userManager;



        public ManageController()

        {

        }



        public ManageController(AppUserManager userManager, ApplicationSignInManager signInManager)

        {

            UserManager = userManager;

            SignInManager = signInManager;

        }



        public ApplicationSignInManager SignInManager

        {

            get

            {

                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

            }

            private set

            {

                _signInManager = value;

            }

        }



        public AppUserManager UserManager

        {

            get

            {

                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();

            }

            private set

            {

                _userManager = value;

            }

        }



        //

        // GET: /Manage/Index

        public ActionResult Index(ManageMessageId? message)

        {
            /*
            ViewBag.StatusMessage =

                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."

                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."

                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."

                : message == ManageMessageId.Error ? "An error has occurred."

                : message == ManageMessageId.AddPhoneNumberSuccess ? "Your PhoneNumber number was added."

                : message == ManageMessageId.RemovePhoneNumberSuccess ? "Your PhoneNumber number was removed."

                : "";



            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            var model = new IndexViewModel

            {

                HasPassword = HasPassword(),

                PhoneNumber = UserManager.FindById(userId).PhoneNumber,

                Logins = await UserManager.GetLoginsAsync(userId),

                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId),

                FName = user.FName,
                Middle = user.Middle,
                LName = user.LName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                City = user.City,
                State = user.State,
                Zip = user.Zip

            }; */

            return RedirectToAction("Edit", "Customers");

        }

        // Get: /Manage/ManagerChangePassword
        public ActionResult ManagerChangePassword(string Id)
        {
            return View();
        }

        //Post /Manage/ManagerChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ManagerChangePassword(ManagerChangePasswordViewModel model, string Id)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            UserManager.RemovePassword(Id);
            var result = await UserManager.AddPasswordAsync(Id, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(Id);
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("EditEmployee","RoleAdmin", new {Id = Id });
            }
            AddErrors(result);
            return View(model);
        }



        //

        // GET: /Manage/ChangePassword

        public ActionResult ChangePassword()

        {
            return View();
        }



        //

        // POST: /Manage/ChangePassword

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)

        {

            if (!ModelState.IsValid)

            {

                return View(model);

            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (result.Succeeded)

            {

                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                if (user != null)

                {

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                }

                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });

            }

            AddErrors(result);

            return View(model);

        }



        ///



    

        // GET: /Manage/SetPassword

        public ActionResult SetPassword()

        {

            return View();

        }



        //

        // POST: /Manage/SetPassword

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)

        {

            if (ModelState.IsValid)

            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
      
                    if (user != null)

                    {

                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    }

                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });

                }

                AddErrors(result);

            }



            // If we got this far, something failed, redisplay form

            return View(model);

        }



        ////EmployeeChangePassword

        //public ActionResult EmployeeChangePassword(string Id)

        //{

        //    return View(Id);

        //}


        //[HttpPost]

        //[ValidateAntiForgeryToken]

        //public async Task<ActionResult> EmployeeChangePassword(EmployeeChangePasswordViewModel model, string Id)

        //{

        //   if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AppUser customer = db.Users.Find(id);
        //    if (customer==null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View("Edit",customer);

        //}



        protected override void Dispose(bool disposing)

        {

            if (disposing && _userManager != null)

            {

                _userManager.Dispose();

                _userManager = null;

            }



            base.Dispose(disposing);

        }



        #region Helpers



        private IAuthenticationManager AuthenticationManager

        {

            get

            {

                return HttpContext.GetOwinContext().Authentication;

            }

        }



        private void AddErrors(IdentityResult result)

        {

            foreach (var error in result.Errors)

            {

                ModelState.AddModelError("", error);

            }

        }



        private bool HasPassword()

        {

            var user = UserManager.FindById(User.Identity.GetUserId());

            if (user != null)

            {

                return user.PasswordHash != null;

            }

            return false;

        }



        private bool HasPhoneNumber()

        {

            var user = UserManager.FindById(User.Identity.GetUserId());

            if (user != null)

            {

                return user.PhoneNumber != null;

            }

            return false;

        }



        public enum ManageMessageId

        {

            AddPhoneNumberSuccess,

            ChangePasswordSuccess,

            SetTwoFactorSuccess,

            SetPasswordSuccess,

            RemoveLoginSuccess,

            RemovePhoneNumberSuccess,

            Error

        }



        #endregion

    }

}