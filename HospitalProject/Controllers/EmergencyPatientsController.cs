using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalProject.Models;
using System.Web.UI.WebControls;

namespace HospitalProject.Controllers
{
    [Authorize]
    public class EmergencyPatientsController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: EmergencyPatients
        public ActionResult Index()
        {
            return View(db.EmergencyPatients.ToList());
        }
       
       
        // GET: EmergencyPatients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmergencyPatient emergencyPatient = db.EmergencyPatients.Find(id);
            if (emergencyPatient == null)
            {
                return HttpNotFound();
            }
            return View(emergencyPatient);
        }

        // GET: EmergencyPatients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmergencyPatients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,First_Name,Last_Name,Card_Number,Severity,Checkin")] EmergencyPatient emergencyPatient)
        {
            if (ModelState.IsValid)
            {
                emergencyPatient.Checkin = DateTime.Now;
                db.EmergencyPatients.Add(emergencyPatient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emergencyPatient);
        }

        // GET: EmergencyPatients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmergencyPatient emergencyPatient = db.EmergencyPatients.Find(id);
            if (emergencyPatient == null)
            {
                return HttpNotFound();
            }
            return View(emergencyPatient);
        }

        // POST: EmergencyPatients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,First_Name,Last_Name,Card_Number,Severity,Checkin")] EmergencyPatient emergencyPatient)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(emergencyPatient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emergencyPatient);
        }

        // GET: EmergencyPatients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmergencyPatient emergencyPatient = db.EmergencyPatients.Find(id);
            if (emergencyPatient == null)
            {
                return HttpNotFound();
            }
            return View(emergencyPatient);
        }

        // POST: EmergencyPatients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmergencyPatient emergencyPatient = db.EmergencyPatients.Find(id);
            PastPatient p = new PastPatient();
            p.Card_Number = emergencyPatient.Card_Number;
            p.Checkin = emergencyPatient.Checkin;
            p.First_Name = emergencyPatient.First_Name;
            p.Last_Name = emergencyPatient.Last_Name;
            p.Severity = emergencyPatient.Severity;
            db.PastPatients.Add(p);
            
            db.EmergencyPatients.Remove(emergencyPatient);
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
