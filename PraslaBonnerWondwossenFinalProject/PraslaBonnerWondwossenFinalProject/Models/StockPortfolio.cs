using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PraslaBonnerWondwossenFinalProject.Models
{
    public class StockPortfolio : BankAccount
    {
        //list of ticker symbols
        List<Stock> Portfolio = new List<Stock>();

        public bool isApproved { get; set; }

        public bool isBalanced { get; set;}

        public Int32 CashBalance { get; set; }

        public Decimal Balance { get; set; }

        public virtual Stock stock { get; set; }

    }
}