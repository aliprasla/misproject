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
                if (stocks.Count == 0) { return Convert.ToDecimal((Gains + Fees + Bonuses + CashBalance)); }

                Decimal stockAmount;
                stockAmount = 0;
                foreach (Stock stock in stocks) { stockAmount += Convert.ToDecimal((stock.StockQuote.PreviousClose) * (stock.Amount)); }

                if (CashBalance == null)
                {
                    stockAmount += Gains + Fees + Bonuses;
                }
                else
                {
                    stockAmount += Gains + Fees + Bonuses + Convert.ToDecimal(CashBalance);
                }
                

                return stockAmount;

            }

        }
    }
}