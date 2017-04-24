using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System;



namespace PraslaBonnerWondwossenFinalProject.Models

{



    public class LoginViewModel

    {

        [Required]

        [Display(Name = "Email")]

        [EmailAddress]

        public string Email { get; set; }



        [Required]

        [DataType(DataType.Password)]

        [Display(Name = "Password")]

        public string Password { get; set; }



        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

    }



    public class RegisterViewModel

    {

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }



        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }



        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }



        //Add any fields that you need for creating a new user

        //For example, first name

        [Required]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Display(Name = "Middle Initial")]
        public string Middle { get; set; }


        [Display(Name = "Last Name")]
        [Required]
        public string LName { get; set; }


        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public Int32 Zip { get; set; }



        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Birthday { get; set; }

        public bool? isActive { get; set; }

    }



    public class ResetPasswordViewModel

    {

        [Required]

        [EmailAddress]

        [Display(Name = "Email")]

        public string Email { get; set; }



        [Required]

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]

        [DataType(DataType.Password)]

        [Display(Name = "Password")]

        public string Password { get; set; }



        [DataType(DataType.Password)]

        [Display(Name = "Confirm password")]

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]

        public string ConfirmPassword { get; set; }



        public string Code { get; set; }

    }



    public class ForgotPasswordViewModel

    {

        [Required]

        [EmailAddress]

        [Display(Name = "Email")]

        public string Email { get; set; }

        [Required]
        [Display(Name = "Year of Birth")]
        public int YearOfBirth { get; set; }

    }

}