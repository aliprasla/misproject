
using System;

using System.Collections.Generic;

using System.Linq;

using System.Web;



namespace PraslaBonnerWondwossenFinalProject.Models

{

    public class StockQuote

    {
        //Jessica
        public Int32 StockQuoteId { get; set; }

        public String Symbol { get; set; }

        public String Name { get; set; }

        public Double PreviousClose { get; set; }

        public Double LastTradePrice { get; set; }

        public Double Volume { get; set; }

        //Jessica
        public virtual List<Stock> Stocks { get; set; }

        public virtual StockType StockType { get; set; }

    }

}