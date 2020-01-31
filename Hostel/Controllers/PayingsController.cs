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
    public class PayingsController : Controller
    {
        private HostelStudent db = new HostelStudent();

        // GET: Payings
        public async Task<ActionResult> Index()
        {
            var paying = db.Paying.Include(p => p.Students);
            return View(await paying.ToListAsync());
        }

        // GET: Payings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paying paying = await db.Paying.FindAsync(id);
            if (paying == null)
            {
                return HttpNotFound();
            }
            return View(paying);
        }

        // GET: Payings/Create
        public ActionResult Create()
        {
            ViewBag.StudentsId = new SelectList(db.Students, "StudentsId", "Surname");
            return View();
        }

        // POST: Payings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PayingId,ServicePayment,DatePayment,StudentsId")] Paying paying)
        {
            if (ModelState.IsValid)
            {
                db.Paying.Add(paying);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.StudentsId = new SelectList(db.Students, "StudentsId", "Surname", paying.StudentsId);
            return View(paying);
        }

        // GET: Payings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paying paying = await db.Paying.FindAsync(id);
            if (paying == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentsId = new SelectList(db.Students, "StudentsId", "Surname", paying.StudentsId);
            return View(paying);
        }

        // POST: Payings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PayingId,ServicePayment,DatePayment,StudentsId")] Paying paying)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paying).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.StudentsId = new SelectList(db.Students, "StudentsId", "Surname", paying.StudentsId);
            return View(paying);
        }

        // GET: Payings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paying paying = await db.Paying.FindAsync(id);
            if (paying == null)
            {
                return HttpNotFound();
            }
            return View(paying);
        }

        // POST: Payings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Paying paying = await db.Paying.FindAsync(id);
            db.Paying.Remove(paying);
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
