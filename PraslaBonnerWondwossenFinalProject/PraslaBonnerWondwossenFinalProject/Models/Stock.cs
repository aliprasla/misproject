using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PraslaBonnerWondwossenFinalProject.StockUtilities;
using System.ComponentModel.DataAnnotations;

namespace PraslaBonnerWondwossenFinalProject.Models
{
    public enum Type { Ordinary, ETF, Futures, MutualFund, IndexFund }

    public class Stock
    {
        //public Int32 Value { get; set; }
        //Jessica
        public Int32 StockID { get; set; }

        [Required]
        public Type Type { get; set; }

        [Required]
        public int Fees { get; set; }

        [Required]
        public string Symbol { get; set; }

        public string Name { get { return GetQuote.GetStock(Symbol).Name; } }

        public decimal Price { get { return Convert.ToDecimal(GetQuote.GetStock(Symbol).LastTradePrice); } }

        public decimal LastPrice { get { return Convert.ToDecimal(GetQuote.GetStock(Symbol).PreviousClose); } }

        public string display { get { return "Stock: "+Name+"   Ticker: "+Symbol+"  Stock Type: "+Type+"    Current Price "+LastPrice+"     Fees: "+Fees; } }


        public virtual List<PurchasedStock> PurchasedStocks { get; set; }
        //public virtual StockQuote StockQuote { get; set; }

    }
}
