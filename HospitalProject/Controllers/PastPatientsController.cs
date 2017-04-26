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
    public class PastPatientsController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: PastPatients
        public ActionResult Index()
        {
            return View(db.PastPatients.ToList());
        }

        // GET: PastPatients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PastPatient pastPatient = db.PastPatients.Find(id);
            if (pastPatient == null)
            {
                return HttpNotFound();
            }
            return View(pastPatient);
        }

       
    }
}
