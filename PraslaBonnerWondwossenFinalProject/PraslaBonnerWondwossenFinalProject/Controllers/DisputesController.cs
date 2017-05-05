using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PraslaBonnerWondwossenFinalProject.Models;
using Microsoft.AspNet.Identity;

namespace PraslaBonnerWondwossenFinalProject.Controllers
{
    public enum Status { Resolved, NotApproved, Approved, WaitingOnManager }
    public class DisputesController : Controller
    {


        private AppDbContext db = new AppDbContext();
        // GET: Disputes
        public ActionResult Index()
        {
            var query = from d in db.Disputes
                        select d;

            query = query.Where(d => d.Status == Status.WaitingOnManager);
            List<Dispute> SelectedDisuputes = query.ToList();

            return View("Index", SelectedDisuputes);
        }

        public ActionResult ViewAll()
        {
            return View("Index", db.Disputes.ToList());
        }

        public ActionResult Create (int Id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create ([Bind(Include = "Id,DisputeAmount,CustomerComment")] Dispute dispute, int Id)
        {
            Transaction transaction = db.Transactions.Find(Id);
            dispute.Status = Status.WaitingOnManager;
            dispute.Transaction = transaction;
            if (ModelState.IsValid)
            {
                db.Disputes.Add(dispute);
                db.SaveChanges();
                transaction.Dispute = dispute;
                return RedirectToAction("Index");
            }

            return View(dispute);
        }

        [Authorize(Roles ="Manager")]
        public ActionResult Resolve(int Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispute dispute = db.Disputes.Find(Id);
            if (dispute == null)
            {
                return HttpNotFound();
            }
            return View(dispute);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Resolve([Bind(Include = "DisputeId")]Status SelectedResponse, String commentString, int AdjustedAmount, Dispute dispute)
        {
            if (ModelState.IsValid)
            {
                Dispute disputeToChange = db.Disputes.Find(dispute.DisputeID);
                AppUser manager = db.Users.Find(User.Identity.GetUserId());
                disputeToChange.AssignedManager = manager;
                //If manager comments
                if (commentString != null)
                {
                    disputeToChange.ManagerDescription = commentString;
                }
                //If manager adjusts amount
                if (AdjustedAmount != 0)
                {
                    disputeToChange.Transaction.Amount = AdjustedAmount;
                    disputeToChange.Status = Status.Resolved;
                    disputeToChange.Transaction.Description = "Dispute Adjusted - " + disputeToChange.Transaction.Description;
                }
                if (SelectedResponse == Status.Approved)
                {
                    disputeToChange.Transaction.Amount = disputeToChange.DisputeAmount;
                    disputeToChange.Status = Status.Resolved;
                    disputeToChange.Transaction.Description = "Dispute Approved - " + disputeToChange.Transaction.Description;
                }
                if (SelectedResponse == Status.NotApproved)
                {
                    disputeToChange.Transaction.Description = "Dispute Rejected - " + disputeToChange.Transaction.Description;
                    disputeToChange.Status = Status.Resolved;
                }

                EmailMessaging.SendEmail("ali.prasla@aiesec.net", "Team 22: Dispute Notification", "Your Dispute has been resolved");
                db.Entry(disputeToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dispute);
        }

        public ActionResult Accept(int Id)
        {
            Dispute dispute = db.Disputes.Find(Id);
            return View(dispute);
        }

        [HttpPost]
        public ActionResult Accept([Bind(Include="ManagerDescription")] Dispute dispute)
        {
            AppUser manager = db.Users.Find(User.Identity.GetUserId());
            dispute.AssignedManager = manager;
            dispute.Transaction.Amount = dispute.DisputeAmount;
            dispute.Status = Status.Resolved;
            dispute.Transaction.Description = "Dispute Approved - " + dispute.Transaction.Description;
            db.SaveChanges();
            return View("Index");
        }

        public ActionResult Reject(int Id)
        {
            Dispute dispute = db.Disputes.Find(Id);
            return View(dispute);
        }

        [HttpPost]
        public ActionResult Reject([Bind(Include = "ManagerDescription")] Dispute dispute)
        {
            AppUser manager = db.Users.Find(User.Identity.GetUserId());
            dispute.AssignedManager = manager;
            dispute.Status = Status.Resolved;
            dispute.Transaction.Description = "Dispute Rejected - " + dispute.Transaction.Description;
            db.SaveChanges();
            return View("Index");
        }

        public ActionResult Adjust(int Id)
        {
            Dispute dispute = db.Disputes.Find(Id);
            return View(dispute);
        }

        [HttpPost]
        public ActionResult Adjust([Bind(Include = "ManagerDescription")] Dispute dispute, Decimal AdjustedAmount)
        {
            AppUser manager = db.Users.Find(User.Identity.GetUserId());
            dispute.AssignedManager = manager;
            dispute.Status = Status.Resolved;
            dispute.Transaction.Amount = AdjustedAmount;
            dispute.Transaction.Description = "Dispute Adjusted - " + dispute.Transaction.Description;
            db.SaveChanges();
            return View("Index");
        }
    }
}