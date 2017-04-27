
using System;

using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

using System.Linq;

using System.Web;

using PraslaBonnerWondwossenFinalProject.Controllers;



namespace PraslaBonnerWondwossenFinalProject.Models

{
<<<<<<< HEAD
    //TODO: Status needs Status types
    public enum Status { WaitingOnManager}
=======
    //TODO:NATE--i made this because i needed to add a migration so that i could work on my stuff and there was an error on this file. I assumed you were going for an enum but idk you can edit this
    public enum Status {resolved,underConsideration};
>>>>>>> d5757939c9c2bc9307f213e1947fe1424b30361f

    public class Dispute

    {
        public Decimal DisputeAmount { get; set; }

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