using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PraslaBonnerWondwossenFinalProject.Models
{
    public class StockPortfolio : BankAccount
    {
        //list of ticker symbols
        //List<Stock> Portfolio = new List<Stock>();

        public bool isApproved { get; set; }

        public bool isBalanced { get; set; }

        //calculate gains
        public Decimal Gains { get {
                Decimal holder = 0;
                if (purchasedstocks.Count()==0)
                {
                    holder = 0;
                    return holder;
                }
                else
                {
                    foreach(PurchasedStock item in purchasedstocks)
                    {
                        holder = holder + (Convert.ToDecimal(item.stock.LastPrice) * item.Shares);
                        holder = holder - (Convert.ToDecimal(item.stock.Price) * item.Shares);
                    }
                }
                return holder; 
            } }

        public Decimal Fees { get; set; }
        public Decimal Bonuses { get; set; }
        public Decimal? CashBalance { get; set; }
        public string info { get { return Name + "     " + "Cash: " + Balance+"      Total Balance:"+Balance; } }

        //public virtual List<Stock> stocks { get; set; }

        public virtual List<Stock> stocks { get; set; }

        public virtual List<PurchasedStock> purchasedstocks { get; set; }

        public new Decimal Balance
        {
            get
            {
                if (purchasedstocks.Count() == 0||purchasedstocks == null) { return Convert.ToDecimal((Gains - Fees + Bonuses + CashBalance)); }

                Decimal stockAmount;
                stockAmount = 0;
                foreach (PurchasedStock purchase in purchasedstocks) { stockAmount += Convert.ToDecimal((purchase.stock.LastPrice) * purchase.Shares); }

                if (CashBalance == null)
                {
                    stockAmount += Gains + Fees + Bonuses;
                }
                else
                {
                    stockAmount += Gains + Fees + Bonuses + Convert.ToDecimal(CashBalance);
                }


                return stockAmount;

            }

        }
    }
}