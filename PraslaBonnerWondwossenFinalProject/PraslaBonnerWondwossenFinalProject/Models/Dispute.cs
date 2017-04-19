
using System;

using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

using System.Linq;

using System.Web;



namespace PraslaBonnerWondwossenFinalProject.Models

{

    public enum Status { WaitingOnManager, Approved, NotApproved }

    public class Dispute

    {

        public int DisputeID { get; set; }

        [Required]

        public Status Status { get; set; }

        [Display(Name = "Transaction Dispute Description")]

        [Required(ErrorMessage = "You must describe the dispute")]

        public string CustomerDescription { get; set; }

        [Display(Name = "Manager Comments on the dispute")]

        public string ManagerDescription { get; set; }

        public virtual AppUser AssignedManager { get; set; }

        [Required(ErrorMessage = "Dispute must be attached to a specific transaction")]

        public virtual Transaction Transaction { get; set; }



    }

}