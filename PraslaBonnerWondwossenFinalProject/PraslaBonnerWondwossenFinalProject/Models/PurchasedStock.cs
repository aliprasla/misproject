using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PraslaBonnerWondwossenFinalProject.Models
{
    public class PurchasedStock
    {
        public int PurchasedStockId { get; set; }
        public int Shares { get; set; }
        public decimal InitialPrice { get; set; }
        public decimal TotalFees { get; set; }
        public DateTime Date { get; set; }
        public virtual Stock stock { get; set; }
        public virtual StockPortfolio stockportfolio { get; set; }
    }
}