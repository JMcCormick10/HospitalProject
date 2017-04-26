using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalProject.Models;

namespace HospitalProject.Controllers
{
    [Authorize]
    public class ParkingLotsController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: ParkingLots
        public ActionResult Index()
        {
            var parkingLots = db.ParkingLots.Include(p => p.Building);
            return View(parkingLots.ToList());
        }

        // GET: ParkingLots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingLot parkingLot = db.ParkingLots.Find(id);
            if (parkingLot == null)
            {
                return HttpNotFound();
            }
            return View(parkingLot);
        }

        // GET: ParkingLots/Create
        public ActionResult Create()
        {
            ViewBag.Building_id = new SelectList(db.Buildings, "Id", "Building_Name");
            return View();
        }

        // POST: ParkingLots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Lot_Name,Building_id")] ParkingLot parkingLot)
        {
            if (ModelState.IsValid)
            {
                db.ParkingLots.Add(parkingLot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Building_id = new SelectList(db.Buildings, "Id", "Building_Name", parkingLot.Building_id);
            return View(parkingLot);
        }

        // GET: ParkingLots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingLot parkingLot = db.ParkingLots.Find(id);
            if (parkingLot == null)
            {
                return HttpNotFound();
            }
            ViewBag.Building_id = new SelectList(db.Buildings, "Id", "Building_Name", parkingLot.Building_id);
            return View(parkingLot);
        }

        // POST: ParkingLots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Lot_Name,Building_id")] ParkingLot parkingLot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parkingLot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Building_id = new SelectList(db.Buildings, "Id", "Building_Name", parkingLot.Building_id);
            return View(parkingLot);
        }

        // GET: ParkingLots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingLot parkingLot = db.ParkingLots.Find(id);
            if (parkingLot == null)
            {
                return HttpNotFound();
            }
            return View(parkingLot);
        }

        // POST: ParkingLots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParkingLot parkingLot = db.ParkingLots.Find(id);
            db.ParkingLots.Remove(parkingLot);
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
