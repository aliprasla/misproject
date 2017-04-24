using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNet.Identity;

using Microsoft.Owin.Security;



namespace PraslaBonnerWondwossenFinalProject.Models

{

    public class IndexViewModel

    {

        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public bool BrowserRemembered { get; set; }

        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Display(Name = "Middle Initial")]
        public string Middle { get; set; }

        [Display(Name = "Last Name")]
        public string LName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }

        
    }



    public class SetPasswordViewModel

    {

        [Required]

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]

        [DataType(DataType.Password)]

        [Display(Name = "New password")]

        public string NewPassword { get; set; }



        [DataType(DataType.Password)]

        [Display(Name = "Confirm new password")]

        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]

        public string ConfirmPassword { get; set; }

    }



    public class ChangePasswordViewModel

    {

        [Required]

        [DataType(DataType.Password)]

        [Display(Name = "Current password")]

        public string OldPassword { get; set; }



        [Required]

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]

        [DataType(DataType.Password)]

        [Display(Name = "New password")]

        public string NewPassword { get; set; }



        [DataType(DataType.Password)]

        [Display(Name = "Confirm new password")]

        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]

        public string ConfirmPassword { get; set; }

    }

}