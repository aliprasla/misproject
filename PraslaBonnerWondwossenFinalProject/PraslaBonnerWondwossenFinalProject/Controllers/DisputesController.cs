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

        public ActionResult Create (string Id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create ([Bind(Include = "Id,DisputeAmount,CustomerComment")] Dispute dispute, string Id)
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


        public ActionResult Resolve(string Id)
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
                }
                if (SelectedResponse == Status.Approved)
                {
                    disputeToChange.Transaction.Amount = disputeToChange.DisputeAmount;
                    disputeToChange.Status = Status.Resolved;
                }
                if (SelectedResponse == Status.NotApproved)
                {
                    disputeToChange.Status = Status.Resolved;
                }
                db.Entry(disputeToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dispute);
        }
    }
}