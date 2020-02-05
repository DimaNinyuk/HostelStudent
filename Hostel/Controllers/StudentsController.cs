using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hostel.Models;
using PagedList;

namespace Hostel.Controllers
{
    public class StudentsController : Controller
    {
        private HostelStudent db = new HostelStudent();

        // GET: Students
        [Authorize]
        public async Task<ActionResult> Index(int? page)
        {

            return View( db.Students.ToList().ToPagedList(page ?? 1, 5));
        }

        // GET: Students/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = await db.Students.FindAsync(id);
            ViewBag.Kurs=DateTime.Now.Year - students.ReceiptDate.Value.Year;
            ViewBag.RoomsId = students.Decree.Select(r=>r.RoomsId).FirstOrDefault();
            ViewBag.HousingId = students.Decree.Select(r => r.HousingId).FirstOrDefault();
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        // GET: Students/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create([Bind(Include = "StudentsId,Surname,Name,Faculty,ReceiptDate")] Students students)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(students);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(students);
        }

        // GET: Students/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = await db.Students.FindAsync(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit([Bind(Include = "StudentsId,Surname,Name,Faculty,ReceiptDate")] Students students)
        {
            if (ModelState.IsValid)
            {
                db.Entry(students).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(students);
        }

        // GET: Students/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = await db.Students.FindAsync(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Students students = await db.Students.FindAsync(id);
            db.Students.Remove(students);
            await db.SaveChangesAsync();
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
        public ActionResult Deleterooms(int? idRooms)
        {
            if (idRooms == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string login = User.Identity.Name;
            var HousingId = db.Users.Where(u => u.Login == login).Select(s => s.HousingId).FirstOrDefault();

            var rooms = db.Rooms.Where(s => s.RoomsId == idRooms).Where(w=>w.HousingId==HousingId).FirstOrDefault();
            rooms.NumberSeatsFree = rooms.NumberSeatsFree+1;
            db.Entry(rooms).State = EntityState.Modified;
            db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
