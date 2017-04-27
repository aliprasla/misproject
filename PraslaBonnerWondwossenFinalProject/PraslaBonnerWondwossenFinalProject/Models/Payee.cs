
using System;

using System.Collections.Generic;

using System.Linq;

using System.Web;

using System.ComponentModel.DataAnnotations;





namespace PraslaBonnerWondwossenFinalProject.Models

{

    public enum PayeeTypes { CreditCard, Utilities, Rent, Mortgage, Other }

    public class Payee



    {

        //primary key

        public Int32 PayeeID { get; set; }



        [Required(ErrorMessage = "Name required")]

        [Display(Name = "Account Name")]

        public String Name { get; set; }

        [Required(ErrorMessage = "Address Required")]
        public String Address { get; set; }



        [Required(ErrorMessage = "City Required")]

        [Display(Name = "City")]

        public String City { get; set; }



        [Required(ErrorMessage = "State Required")]

        [Display(Name = "State")]

        public String State { get; set; }



        [Required(ErrorMessage = "Zip Code Required")]

        [Display(Name = "Zip Code")]

        public Int32 ZipCode { get; set; }



        [Required(ErrorMessage = "Phone Number Required")]

        [Display(Name = "Phone Number")]

        public String Phone { get; set; }



        [Required(ErrorMessage = "Type Required")]

        [Display(Name = "Type")]

        public PayeeTypes Type { get; set; }





    }

}