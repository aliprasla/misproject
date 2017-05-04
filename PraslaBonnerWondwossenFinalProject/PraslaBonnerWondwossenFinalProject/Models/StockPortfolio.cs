using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PraslaBonnerWondwossenFinalProject.Models
{
    public class StockPortfolio : BankAccount
    {
        //list of ticker symbols
        //List<Stock> Portfolio = new List<Stock>();

        public bool isApproved { get; set; }

        public bool isBalanced { get; set; }


        public Decimal Gains { get; set; }

        public Decimal Fees { get; set; }
        public Decimal Bonuses { get; set; }


        public Decimal? CashBalance { get; set; }

        public virtual List<Stock> stocks { get; set; }

        public new Decimal Balance
        {
            get
            {
                Decimal stockAmount;
                stockAmount = 0;
                foreach (Stock stock in stocks) { stockAmount += Convert.ToDecimal((stock.StockQuote.PreviousClose) * (stock.Amount)); }

                stockAmount += Gains + Fees + Bonuses + Balance;

                return stockAmount;

            }

        }
    }
}