using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PraslaBonnerWondwossenFinalProject.Models;
using System.ComponentModel.DataAnnotations;


namespace PraslaBonnerWondwossenFinalProject.Models
{
    public class SellViewModel
    {
        [Display(Name ="Date of Sale") ]
        public DateTime SaleDate { get; set; }

        [Display(Name = "Number of shares you wish to sell")]
        public Int32 SharesSold { get; set; }


        public Decimal NetProfit { get; set; }

        public String Name { get; set; }

        public Int32 SharesLeft { get; set; }
        public Int32 Fees { get; set; }
    }
}