using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PraslaBonnerWondwossenFinalProject.Models
{
    public class Sales
    {
        public Int32 SalesId { get; set; }
        public Int32 Shares { get; set; }
        public DateTime date { get; set; }
        public Decimal NetProfit { get; set; }
        public Int32 SharesLeft { get; set; }
        public Int32 PurchaseId { get; set; }

    }
}