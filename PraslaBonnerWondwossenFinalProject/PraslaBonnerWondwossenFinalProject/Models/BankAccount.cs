
using System;

using System.Collections.Generic;

using System.Linq;

using System.Web;

using System.ComponentModel.DataAnnotations;
using Microsoft.Ajax.Utilities;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.Collections;

namespace PraslaBonnerWondwossenFinalProject.Models

{

    public enum AccountTypes { Savings, Checking, IRA, Stock }

    public class BankAccount

    {
        private AppDbContext db = new AppDbContext();
        
        public BankAccount(){
            Transactions = new List<Transaction>();
        
        }
        //primary key
        [Key]
        public Int32 BankAccountID { get; set; }


        //we'll work on the AccountNumber thing in the controller. 
        [Display(Name = "Account Number")]
        public Int32 AccountNumber { get; set; }



        public static DateTime Today { get; }



        [Required(ErrorMessage = "You must select an Account type")]

        [Display(Name = "Account Type")]

        public AccountTypes Type { get; set; }



        [Display(Name = "Account Name")]

        public String Name { get; set; }





        [Required]

        public Decimal Balance { get; set; }



        public virtual AppUser Customer { get; set; }

        public virtual List<Transaction> Transactions {get;set;}


        public string NameNo
        {
            get
            {
                try
                {
                    return "( XXXXXX" + Convert.ToString(this.AccountNumber).Substring(6, 4) + " ) " + this.Name;
                }
                catch 
                {
                    return "";
                }
            }
        }
        public string lastFour {get

            {
                return "XXXXXX" + Convert.ToString(this.AccountNumber).Substring(6, 4);
            } }
    }

}