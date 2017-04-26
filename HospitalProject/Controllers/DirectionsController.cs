using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalProject.Models;
using System.Collections;

namespace HospitalProject.Controllers
{
    public class DirectionsController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: Directions
      

        // GET: Directions/Create
        public ActionResult Index()
        {
            //creating a drop down list of all the amenities.
            IEnumerable<SelectListItem> items = db.Amenities.Select(l => new SelectListItem
            { Value = l.Id.ToString(),
            Text = l.Amenity_Name
            });
            ViewBag.Amenity = items;

            return View();
        }

        // POST: Directions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection form, Amenity amenity)
        {
            //re-writing the drop down list so it's available in post.
            IEnumerable<SelectListItem> items = db.Amenities.Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.Amenity_Name
            });
            ViewBag.Amenity = items;

            //collecting the value of the form so we can get the building it the service is located in, closest parking and entrance.

            //converting our string value into an int so we can use it to grab our amenity/service.
            string amenity_id = form["Amenity"];
            int a_id = Convert.ToInt32(amenity_id);

            amenity = db.Amenities.Where(a => a.Id == a_id).FirstOrDefault();
            ViewBag.AmenityName = amenity.Amenity_Name;


            //now lets get the building.
            Building building = new Building();
             building = db.Buildings.Where(b => b.Id == amenity.Building_id).FirstOrDefault();

            ViewBag.Building = building.Building_Name;

            //now let's get the parking
            List<ParkingLot> parking = db.ParkingLots.Where(p => p.Building_id == amenity.Building_id).ToList();
            foreach(var item in parking)
            {
                ViewBag.Parking += "<li>" + item.Lot_Name + "</li>";
            }

            //now let's get the entrances
            List<Entrance> entrance = db.Entrances.Where(e => e.Building_id == amenity.Building_id).ToList();
            foreach(var item in entrance)
            {
                ViewBag.Entrance += "<li>" +item.Entrance_Name + "</li>";
            }
            return View(amenity);
        }

       
    }
}
