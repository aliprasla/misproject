using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PraslaBonnerWondwossenFinalProject.Models;

namespace PraslaBonnerWondwossenFinalProject.Controllers
{
    public class ManagersController : Controller
    {
        private AppDbContext db = new AppDbContext();
        // GET: Managers
        public ActionResult Index()
        {
            return View();
        }
    }
}