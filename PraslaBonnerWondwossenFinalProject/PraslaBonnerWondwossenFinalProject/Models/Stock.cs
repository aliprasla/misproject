using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PraslaBonnerWondwossenFinalProject.Models
{
    public class Stock
    {
        //public Int32 Value { get; set; }
        //Jessica
        public Int32 StockID { get; set; }

        public Int32 Amount { get; set; }

        
        public virtual StockType StockType { get; set; }
        
        //Jessica

        public List<StockPortfolio> StockPortfolios { get; set;}

        public List<StockQuote> StockQuotes { get; set; }
    }
}