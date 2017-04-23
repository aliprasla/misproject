
using System;

using System.Collections.Generic;

using System.Linq;

using System.Web;

using System.ComponentModel.DataAnnotations;




namespace PraslaBonnerWondwossenFinalProject.Models

{

    public enum AccountTypes { Savings, Checking, IRA, Stock }

    public class BankAccount



    {

        //primary key

        public Int32 BankAccountID { get; set; }

     
        //we'll work on the AccountNumber thing in the controller. 
        [Display(Name = "Account Number")]
        public Int32 AccountNumber {get; set;}

  

        public static DateTime Today { get; }



        [Required(ErrorMessage = "You must select an Account type")]

        [Display(Name = "Account Type")]

        public AccountTypes Type { get; set; }



        [Display(Name = "Account Name")]

        public String Name { get; set; }



        [Required]

        //[Display(Name = "Initial Deposit")]

        public Decimal Balance { get; set; }



        public virtual AppUser Customer { get; set; }


    }

}