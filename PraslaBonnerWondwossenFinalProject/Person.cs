using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LonghornBank.Models
{
    public class Person
    {
        public int CustomerID { get; set; }
        
        [Required]
        public string FName { get; set; }

        [Required]
        public string LName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        public bool? isCustomer { get; set; }

        public bool? isEmployee { get; set; }

        public bool? isManager { get; set; }

        public bool? isDeactive { get; set; }
    }
}