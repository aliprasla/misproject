using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PraslaBonnerWondwossenFinalProject.Models
{
    public enum Type { Ordinary, ETF, Futures, MutualFund, IndexFund }

    public class Stock
    {
        //public Int32 Value { get; set; }
        //Jessica
        public Int32 StockID { get; set; }

        public Int32 Amount { get; set; }

        public Type Type { get; set; }

        public int Fees { get; set; }
              
        //Jessica

        public virtual StockPortfolio StockPortfolio { get; set;}

        public virtual StockQuote StockQuote { get; set; }
    }
}