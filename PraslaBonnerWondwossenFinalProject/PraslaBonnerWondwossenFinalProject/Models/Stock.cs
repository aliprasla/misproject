using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PraslaBonnerWondwossenFinalProject.Models
{
    public class Stock:BankAccount
    {
        public Int32 Value { get; set; }
        public virtual StockType StockType { get; set; }
    }
}