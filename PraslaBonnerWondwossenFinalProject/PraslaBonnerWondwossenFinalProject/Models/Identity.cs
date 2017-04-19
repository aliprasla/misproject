using System.Data.Entity;

using System.Security.Claims;

using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using Microsoft.AspNet.Identity.EntityFramework;

using System.ComponentModel.DataAnnotations;

using System;

using System.Collections.Generic;

namespace PraslaBonnerWondwossenFinalProject.Models

{

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class AppUser : IdentityUser

    {



        // Put any additional fields that you need for your user here

        //For instance
        [Required]
        public string FName { get; set; }

        public string Middle { get; set; }

        [Required]
        public string LName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public Int32 Zip { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Birthday { get; set; }

        public bool? isActive { get; set; }

        public virtual List<Product> Products { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
        public virtual List<Dispute> Disputes { get; set; }

        //This method allows you to create a new user

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)

        {

            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType

            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here

            return userIdentity;

        }

    }



    // Here's your db context for the project.  All of your db sets should go in here

    public class AppDbContext : IdentityDbContext<AppUser>

    {

        //Add dbsets here, for instance there's one for books

        //Remember, Identity adds a db set for users, so you shouldn't add that one - you will get an error

        public DbSet<Payee> Payees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Dispute> Disputes { get; set; }





        //Make sure that your connection string name is correct here.

        public AppDbContext()

            : base("MyDBConnection", throwIfV1Schema: false)

        {

        }



        public static AppDbContext Create()

        {

            return new AppDbContext();

        }



        public DbSet<AppRole> AppRoles { get; set; }

    }

}