using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PraslaBonnerWondwossenFinalProject.Models;
using System.Data.Entity;


namespace PraslaBonnerWondwossenFinalProject.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("MyDbContext") { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Dispute> Disputes { get; set; }
        public DbSet<Payee> Payees { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        
    }
}