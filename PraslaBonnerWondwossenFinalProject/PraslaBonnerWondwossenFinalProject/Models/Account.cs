using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace PraslaBonnerWondwossenFinalProject.Models
{
    public enum AccountTypes { Saving, Checking, IRA, StockPortfolio }
    public class Account

    {
        //primary key
        public Int32 AccountID { get; set; }

        public static DateTime Today { get; }

        [Required(ErrorMessage = "You must select an account type")]
        [Display(Name = "Account Type")]
        public AccountTypes Type { get; set; }

        [Display(Name = "Account Name")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Please enter an initial deposit")]
        [Display(Name = "Initial Deposit")]
        public Decimal Balance { get; set; }

        public virtual Person Customer { get; set; }
    }
}
