using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheatreCMS.Models;

namespace TheatreCMS.Controllers
{
    public class RentalRequestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RentalRequest
        public ActionResult Index()
        {
            return View(db.RentalRequests.ToList());
        }

        // GET: RentalRequest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalRequest rentalRequest = db.RentalRequests.Find(id);
            if (rentalRequest == null)
            {
                return HttpNotFound();
            }
            return View(rentalRequest);
        }

        // GET: RentalRequest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RentalRequest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RentalRequestId,ContactPerson,Company,RequestedTime,StartTime,EndTime,ProjectInfo,RentalCode,Accepted,ContractSigned")] RentalRequest rentalRequest)
        {
            if (ModelState.IsValid)
            {
                db.RentalRequests.Add(rentalRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rentalRequest);
        }

        // GET: RentalRequest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalRequest rentalRequest = db.RentalRequests.Find(id);
            if (rentalRequest == null)
            {
                return HttpNotFound();
            }
            return View(rentalRequest);
        }

        // POST: RentalRequest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentalRequestId,ContactPerson,Company,RequestedTime,StartTime,EndTime,ProjectInfo,RentalCode,Accepted,ContractSigned")] RentalRequest rentalRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentalRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rentalRequest);
        }

        // GET: RentalRequest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalRequest rentalRequest = db.RentalRequests.Find(id);
            if (rentalRequest == null)
            {
                return HttpNotFound();
            }
            return View(rentalRequest);
        }

        // POST: RentalRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RentalRequest rentalRequest = db.RentalRequests.Find(id);
            db.RentalRequests.Remove(rentalRequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
