using Microsoft.AspNet.Identity;
using PraslaBonnerWondwossenFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PraslaBonnerWondwossenFinalProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private AppDbContext db = new AppDbContext();
        public ActionResult Index()
        {
            if (User.IsInRole("Customer")) {
                return RedirectToAction("Index", "Customers");
            }
            return View();
        }
    }
}