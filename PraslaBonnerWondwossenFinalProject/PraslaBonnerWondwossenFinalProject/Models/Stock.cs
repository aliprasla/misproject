using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PraslaBonnerWondwossenFinalProject.StockUtilities;

namespace PraslaBonnerWondwossenFinalProject.Models
{
    public class Stock
    {
        public string Ticker { get; set; }
        public string Type { get; set; }
        public double Price { get { return (GetQuote.GetStock(Ticker).LastTradePrice); } }
        public int TransactionFee { get; set; }
    }
}