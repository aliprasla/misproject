using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PraslaBonnerWondwossenFinalProject.Models
{
    public class StockType
    {
        public Int32 StockTypeID { get; set; }

        [Required(ErrorMessage = "Name required")]
        [Display(Name="Name of new stock type")]
        public String Name { get; set; }

        //Jessica
        public virtual List<Stock> Stocks { get; set; }

        //public virtual List<StockQuote> StockQuotes { get; set; }
    }
}