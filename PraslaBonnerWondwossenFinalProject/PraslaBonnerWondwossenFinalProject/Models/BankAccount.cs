
using System;

using System.Collections.Generic;

using System.Linq;

using System.Web;

using System.ComponentModel.DataAnnotations;




namespace PraslaBonnerWondwossenFinalProject.Models

{

    public enum AccountTypes { Saving, Checking, IRA, StockPortfolio }

    public class BankAccount



    {

        //primary key

        public Int32 BankAccountID { get; set; }

        

        private Int32 _decBankAccountNumber { get; set; }

        public Int32 ProductNumber

        {

            get { return _decBankAccountNumber; }

            set { _decBankAccountNumber = Convert.ToInt32(9999999999 + BankAccountID); }

        }

  

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