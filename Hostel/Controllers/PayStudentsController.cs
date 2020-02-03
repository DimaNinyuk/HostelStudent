using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hostel.Models;
using PagedList;

namespace Hostel.Controllers
{
    public class PayStudentsController : Controller
    {
        private HostelStudent db = new HostelStudent();

        // GET: PayStudents
        [Authorize]
        public ActionResult Index(int? StudentsId, int? page)
        {
            string login = User.Identity.Name;
            int? HousingId = db.Users.Where(u => u.Login == login).Select(s => s.HousingId).FirstOrDefault();

            var paustudents = db.PayStudent.Where(p => p.HousingId == HousingId);
            if (StudentsId != null)
            {
                paustudents= paustudents.Where(s => s.StudentsId == StudentsId);
                
                return View(paustudents.ToList().ToPagedList(page ?? 1, 5));
            }


                return View(paustudents.ToList().ToPagedList(page ?? 1, 5));
        }

        // GET: PayStudents/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var payStudent = db.PayStudent.Where(w=>w.PayingId==id).First();
            if (payStudent == null)
            {
                return HttpNotFound();
            }
            return View(payStudent);
        }

        // GET: PayStudents/Create
        [Authorize]
        public ActionResult Create()
        {
            

            ViewBag.StudentsId = new SelectList(db.Students, "StudentsId", "StudentsId");
            return View();
        }

        // POST: PayStudents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "PayingId,StudentsId,ServicePayment,DatePayment")] Paying payStudent)
        {
            
            if (ModelState.IsValid)
            {
                db.Paying.Add(payStudent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(payStudent);
        }

        // GET: PayStudents/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paying payStudent = db.Paying.Find(id);
            if (payStudent == null)
            {
                return HttpNotFound();
            }
            return View(payStudent);
        }

        // POST: PayStudents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "PayingId,ServicePayment,DatePayment,StusentId")] Paying payStudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payStudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payStudent);
        }

        // GET: PayStudents/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayStudent payStudent = db.PayStudent.Where(i=>i.PayingId==id).FirstOrDefault();
            if (payStudent == null)
            {
                return HttpNotFound();
            }
            return View(payStudent);
        }

        // POST: PayStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Paying payStudent = db.Paying.Find(id);
            db.Paying.Remove(payStudent);
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
