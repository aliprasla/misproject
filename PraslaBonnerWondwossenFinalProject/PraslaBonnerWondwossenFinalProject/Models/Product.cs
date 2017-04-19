
using System;

using System.Collections.Generic;

using System.Linq;

using System.Web;

using System.ComponentModel.DataAnnotations;




namespace PraslaBonnerWondwossenFinalProject.Models

{

    public enum ProductTypes { Saving, Checking, IRA, StockPortfolio }

    public class Product



    {

        //primary key

        public Int32 ProductID { get; set; }

        

        private Int32 _decProductNumber { get; set; }

        public Int32 ProductNumber

        {

            get { return _decProductNumber; }

            set { _decProductNumber = Convert.ToInt32(9999999999 + ProductID); }

        }

  

        public static DateTime Today { get; }



        [Required(ErrorMessage = "You must select an Product type")]

        [Display(Name = "Product Type")]

        public ProductTypes Type { get; set; }



        [Display(Name = "Product Name")]

        public String Name { get; set; }



        [Required(ErrorMessage = "Please enter an initial deposit")]

        [Display(Name = "Initial Deposit")]

        public Decimal Balance { get; set; }



        public virtual AppUser Customer { get; set; }


        

        public virtual AppUser Person { get; set; }

    }

}