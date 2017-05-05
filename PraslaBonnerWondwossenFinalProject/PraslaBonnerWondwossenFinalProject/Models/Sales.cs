using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PraslaBonnerWondwossenFinalProject.Models
{
    public class Sales
    {
        
        public Int32 SalesId { get; set; }

        [Display(Name = "# shares to sell")]
        public Int32 Shares { get; set; }

        [Display(Name = "sel date")]
        public DateTime date { get; set; }

        [Display(Name = "Net Profit")]
        public Decimal NetProfit { get; set; }

        [Display(Name = "# shares left after sale")]
        public Int32 SharesLeft { get; set; }
        
        public Int32 PurchaseId { get; set; }
        [Display(Name = "Name of Stock")]
        public string Name { get; set; }
        [Display(Name = "Fee")]
        public Int32 Fees { get; set; }
    }
}