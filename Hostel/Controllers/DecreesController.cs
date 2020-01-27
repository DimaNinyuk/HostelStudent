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

namespace Hostel.Controllers
{
    public class DecreesController : Controller
    {
        private HostelStudent db = new HostelStudent();

        // GET: Decrees
        public async Task<ActionResult> Index()
        {
            var decree = db.Decree.Include(d => d.Rooms).Include(d => d.Students);
            return View(await decree.ToListAsync());
        }

        // GET: Decrees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Decree decree = await db.Decree.FindAsync(id);
            if (decree == null)
            {
                return HttpNotFound();
            }
            return View(decree);
        }

        // GET: Decrees/Create
        public ActionResult Create()
        {
            ViewBag.RoomsId = new SelectList(db.Rooms, "RoomsId", "RoomsId");
            ViewBag.StudentsId = new SelectList(db.Students, "StudentsId", "Surname");
            return View();
        }

        // POST: Decrees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DecreeId,DateSigning,DateArrival,DateEviction,StudentsId,RoomsId,HousingId")] Decree decree)
        {
            if (ModelState.IsValid)
            {
                db.Decree.Add(decree);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RoomsId = new SelectList(db.Rooms, "RoomsId", "RoomsId", decree.RoomsId);
            ViewBag.StudentsId = new SelectList(db.Students, "StudentsId", "Surname", decree.StudentsId);
            return View(decree);
        }

        // GET: Decrees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Decree decree = await db.Decree.FindAsync(id);
            if (decree == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomsId = new SelectList(db.Rooms, "RoomsId", "RoomsId", decree.RoomsId);
            ViewBag.StudentsId = new SelectList(db.Students, "StudentsId", "Surname", decree.StudentsId);
            return View(decree);
        }

        // POST: Decrees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DecreeId,DateSigning,DateArrival,DateEviction,StudentsId,RoomsId,HousingId")] Decree decree)
        {
            if (ModelState.IsValid)
            {
                db.Entry(decree).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RoomsId = new SelectList(db.Rooms, "RoomsId", "RoomsId", decree.RoomsId);
            ViewBag.StudentsId = new SelectList(db.Students, "StudentsId", "Surname", decree.StudentsId);
            return View(decree);
        }

        // GET: Decrees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Decree decree = await db.Decree.FindAsync(id);
            if (decree == null)
            {
                return HttpNotFound();
            }
            return View(decree);
        }

        // POST: Decrees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Decree decree = await db.Decree.FindAsync(id);
            db.Decree.Remove(decree);
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
    }
}
