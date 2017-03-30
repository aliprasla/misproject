using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Prasla_Ali_HW5.DAL;
using Prasla_Ali_HW5.Models;
namespace Prasla_Ali_HW5.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index (String SearchString) {
            ViewBag.CustomerCount = db.Customers.ToList().Count();
            List<Customer> SelectedCustomers = new List<Customer> ();
            if (SearchString == null || SearchString == "")
            {
                ViewBag.SelectedCustomerCount = ViewBag.CustomerCount;
                return View(db.Customers.ToList());
            }
            else
            {
                var query = from c in db.Customers select c;
                query = query.Where(c => c.FirstName.Contains(SearchString) || c.LastName.Contains(SearchString));
                SelectedCustomers = query.ToList();
                ViewBag.SelectedCustomerCount = SelectedCustomers.Count();
                SelectedCustomers.OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ThenBy(c => c.AverageSale);
                return View(SelectedCustomers);
            }
        }
        public ActionResult DetailedSearch() {
            ViewBag.AllFrequencies = GetAllFrequency();
            return View();
        }
        public ActionResult SearchResults(string SearchString, int SelectedFrequency, String SelectedGender, string SelectedSales, string SalesParameter)
        {
            var query = from c in db.Customers select c;
            if (SearchString != null || SearchString != "") {
                query = query.Where(c => c.FirstName.Contains(SearchString) || c.LastName.Contains(SearchString));
            }
            
            if (SelectedFrequency != 0) {
                query = query.Where(c => c.Frequency.FrequencyID.Equals(SelectedFrequency));
            }
            if (SelectedGender != "All") {
                query = query.Where(c => c.Gender.Equals(SelectedGender));
            }
            if (SelectedSales != null || SelectedSales != "") {
                Decimal compareDec;
                try
                {
                    compareDec = Convert.ToDecimal(SelectedSales);
                }
                catch {
                    ViewBag.CustomerCount = db.Customers.ToList().Count();
                    ViewBag.SelectedCustomerCount = query.Count();
                    return View("Index", query.ToList());

                }
                if (SalesParameter == "GreaterThan")
                {
                    query = query.Where(c => c.AverageSale >= compareDec);
                }
                else if (SalesParameter == "LessThan")
                {
                    query = query.Where(c => c.AverageSale <= compareDec);
                }
                else {
                    return View("View");
                }        
            }
            
            ViewBag.CustomerCount = db.Customers.ToList().Count();
            ViewBag.SelectedCustomerCount = query.Count();
            return View("Index", query.ToList());
        }
        
        public SelectList GetAllFrequency() {
            var query = from c in db.Frequencies select c;
            query = query.Where(c => c.Name != "Not Used");
            List<Frequency> Frequencies = query.ToList();
            Frequency SelectNone = new Models.Frequency() { FrequencyID = 0, Name = "All Frequencies" };
            Frequencies.Insert(0,SelectNone);
            SelectList AllFrequencies = new SelectList(Frequencies,"FrequencyID","Name");
            return  AllFrequencies;
        }
    }

}