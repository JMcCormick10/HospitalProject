using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalProject.Models;
using System.IO;
using System.Text.RegularExpressions;

namespace HospitalProject.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: News
        public ActionResult Index()
        {
            return View(db.News.ToList());
        }

        // GET: News/Details/5
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

        // GET: News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, [Bind(Include = "Id,Title,Blurb,Date,Priority,Photo,Content")] News news)
        {
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    

                    ViewBag.Error = "You must upload a photo";
                    return View();
                }
              
                else
                {
                    var fileName = Path.GetFileName(file.FileName);
                    if (!Regex.IsMatch(fileName, (@"[a-z|A-Z]+(\.png|.jpg|\.PNG|\.JPG)")))
                    {

                        ViewBag.Error = "This is not a valid file format. Files must be named with only letters and end in .png or .jpg";
                        return View();
                    }
                    else if (fileName.Length <1 || fileName.Length > 50) { 
                        ViewBag.Error = "The file size is too small or large. It must be between 1 and 50 characters in length";
                        return View();
                    }
                    else
                    {
                        news.Photo = fileName;


                        news.Date = DateTime.Now;
                        db.News.Add(news);
                        db.SaveChanges();
                        var path = Path.Combine(Server.MapPath("~/Images/" + fileName));
                        file.SaveAs(path);
                    }
                }
                return RedirectToAction("Index");
            }
           
           


            return View(news);
        }

        // GET: News/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, [Bind(Include = "Id,Title,Blurb,Date,Priority,Photo,Content")] News news)
        {
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    ViewBag.Error = "You did not upload a file!";
                }
               else {
                    var fileName = Path.GetFileName(file.FileName);
                    if (!Regex.IsMatch(fileName, (@"[a-z|A-Z]+(\.png|.jpg|\.PNG|\.JPG)")))
                    {

                        ViewBag.Error = "This is not a valid file format.";
                        return View();
                    }
                    else if (fileName.Length < 1 || fileName.Length > 50)
                    {
                        ViewBag.Error = "The file size is too small or large. It must be between 1 and 50 characters in length";
                        return View();
                    }
                    else
                    {
                        news.Photo = fileName;

                        db.Entry(news).State = EntityState.Modified;
                        db.SaveChanges();
                        var path = Path.Combine(Server.MapPath("~/Images/" + fileName));
                        file.SaveAs(path);
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(news);
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
        
            db.News.Remove(news);
            string fullPath = Request.MapPath("~/Images/" + news.Photo);
            System.IO.File.Delete(fullPath);
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
