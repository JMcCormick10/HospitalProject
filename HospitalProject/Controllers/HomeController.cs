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
    public class HomeController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: Home
        public ActionResult Index()
        {
            //Get the Emergency Wait Time

            int doctorCount = db.EmergencyDoctors.Where(e => e.OnDuty == "Yes").Count();
            int patientCount = db.EmergencyPatients.Count();
            if (patientCount > 0)
            {
                decimal dc = Convert.ToDecimal(doctorCount);
                decimal pc = Convert.ToDecimal(patientCount);
                List<EmergencyPatient> ep = db.EmergencyPatients.ToList();

                double severitySum = ep.Average(s => s.Severity);
                decimal sv = Convert.ToDecimal(severitySum);

                decimal waitTime = (pc * 0.5m) * sv - (dc * 0.25m);

                if (waitTime == 0)
                {
                    ViewBag.noWait = "No Wait time.";
                }
                else
                {
                    decimal waitTimeHours = Math.Floor(waitTime);
                    decimal waitTimeMinutes = waitTime % 1;
                    ViewBag.Close = " minutes";


                    //testing for minutes and rounding up. 

                    if (waitTimeMinutes == 0)
                    {
                        ViewBag.Close = "";
                    }


                    if (waitTimeMinutes > 0 && waitTimeMinutes <= 0.25m)
                    {
                        ViewBag.WaitMinutes = 15;
                        ViewBag.Connect = " and ";

                    }
                    else if (waitTimeMinutes > 0.25m && waitTimeMinutes <= 0.5m)
                    {
                        waitTimeMinutes = 30;
                        ViewBag.WaitMinutes = waitTimeMinutes;
                        ViewBag.Connect = " and ";

                    }
                    else if (waitTimeMinutes > 0.5m && waitTimeMinutes <= 0.75m)
                    {
                        waitTimeMinutes = 45;
                        ViewBag.WaitMinutes = waitTimeMinutes;

                        ViewBag.Connect = " and ";

                    }

                    //rounding up an hour and setting the wait time minutes to 0.
                    else if (waitTimeMinutes > 0.75m && waitTimeMinutes <= 1)
                    {
                        waitTimeMinutes = 0;
                        ViewBag.WaitMinutes = "";
                        waitTimeHours = waitTimeHours + 1;
                        ViewBag.Connect = "";
                        ViewBag.Close = "";

                    }

                    if (waitTimeHours == 0)
                    {
                        ViewBag.WaitHours = "";
                        ViewBag.Connect = "";

                    }
                    else if (waitTimeHours == 1 && waitTimeMinutes != 0)
                    {
                        ViewBag.WaitHours = waitTimeHours + " hour ";
                    }
                    else if (waitTimeHours > 1 && waitTimeMinutes != 0)
                    {
                        ViewBag.WaitHours = waitTimeHours + " hours ";
                    }

                    else
                    {
                        if (waitTimeHours == 1)
                            ViewBag.WaitHours = waitTimeHours + " hour.";
                        if (waitTimeHours > 1)
                            ViewBag.WaitHours = waitTimeHours + " hours.";
                    }

                }

            } //end of patient count greater than 0

            //if patient count is 0
            else
            {
                ViewBag.noWait = "No Wait Time.";
            }
            return View(db.News.OrderByDescending(p => p.Priority).Take(5).ToList());     
        }
        

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

    }
}
