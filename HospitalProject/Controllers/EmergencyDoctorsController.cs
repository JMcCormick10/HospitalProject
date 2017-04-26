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
    public class EmergencyDoctorsController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: EmergencyDoctors
        public ActionResult Index()
        {
            return View(db.EmergencyDoctors.ToList());
        }

        // GET: EmergencyDoctors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmergencyDoctor emergencyDoctor = db.EmergencyDoctors.Find(id);
            if (emergencyDoctor == null)
            {
                return HttpNotFound();
            }
            return View(emergencyDoctor);
        }

        // GET: EmergencyDoctors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmergencyDoctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,First_Name,Last_Name,OnDuty")] EmergencyDoctor emergencyDoctor)
        {
            if (ModelState.IsValid)
            {
                db.EmergencyDoctors.Add(emergencyDoctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emergencyDoctor);
        }

        // GET: EmergencyDoctors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmergencyDoctor emergencyDoctor = db.EmergencyDoctors.Find(id);
            if (emergencyDoctor == null)
            {
                return HttpNotFound();
            }
            return View(emergencyDoctor);
        }

        // POST: EmergencyDoctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,First_Name,Last_Name,OnDuty")] EmergencyDoctor emergencyDoctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emergencyDoctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emergencyDoctor);
        }

        // GET: EmergencyDoctors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmergencyDoctor emergencyDoctor = db.EmergencyDoctors.Find(id);
            if (emergencyDoctor == null)
            {
                return HttpNotFound();
            }
            return View(emergencyDoctor);
        }

        // POST: EmergencyDoctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmergencyDoctor emergencyDoctor = db.EmergencyDoctors.Find(id);
            db.EmergencyDoctors.Remove(emergencyDoctor);
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
