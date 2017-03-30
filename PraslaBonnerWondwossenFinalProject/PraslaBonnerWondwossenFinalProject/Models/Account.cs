using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace PraslaBonnerWondwossenFinalProject.Models
{
    public enum AccountTypes { Saving, Checking, IRA, Stock Portfolio }
    public class Account

    {        
        //primary key
        public Int32 AccountID { get; set; }
        
        private Int32 _decAccountNumber
        public Int32 AccountNumber 
        { 
            get{ return _intAccountNumber; } 
            set{ _intAccountNumber=9999999999 + AccountID; } 
        }
  
        
        public static DateTime Today { get; }
        
        [Required(ErrorMessage = "You must select an account type")]
        [Display(Name="Account Type")]
        public AccountTypes Type { get; set; }
        
        [Display(Name="Account Name")]
        public Str Name { get; set; }
        
        [Required(ErrorMessage= "Please enter an initial deposit")]
        [Display(Name="Initial Deposit")]
        public Decimal Balance { get; set; }
        
        public virtual Person Person { get; set; }
    }
