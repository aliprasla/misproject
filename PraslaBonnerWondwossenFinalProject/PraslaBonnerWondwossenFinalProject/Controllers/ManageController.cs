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

                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."

                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."

                : "";



            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            var model = new IndexViewModel

            {

                HasPassword = HasPassword(),

                PhoneNumber = UserManager.FindById(userId).Phone,

                Logins = await UserManager.GetLoginsAsync(userId),

                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId),

                FName = user.FName,
                Middle = user.Middle,
                LName = user.LName,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                City = user.City,
                State = user.State,
                Zip = user.Zip

            }; */

            return RedirectToAction("Edit", "Customers");

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



        //





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

            AddPhoneSuccess,

            ChangePasswordSuccess,

            SetTwoFactorSuccess,

            SetPasswordSuccess,

            RemoveLoginSuccess,

            RemovePhoneSuccess,

            Error

        }



        #endregion

    }

}