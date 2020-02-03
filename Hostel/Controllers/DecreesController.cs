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
    public class DecreesController : Controller
    {
        private HostelStudent db = new HostelStudent();

        // GET: Decrees
        [Authorize]
        public async Task<ActionResult> Index(int? RoomsId, int? page)
        {
            var decree = db.Decree.Include(d => d.Rooms).Include(d => d.Students).ToList();
            string login=User.Identity.Name;
            var HousingId = db.Users.Where(u => u.Login == login).Select(s => s.HousingId).FirstOrDefault();
            decree =decree.Where(d => d.HousingId == HousingId).ToList();
            if (RoomsId != null )
            {
                decree = decree.Where(s => s.Rooms.RoomsId == RoomsId).ToList();
                ViewBag.RoomsId = new SelectList(db.Rooms.Where(h => h.HousingId == HousingId), "RoomsId", "RoomsId");
                return View( decree.ToPagedList(page ?? 1, 5));
            }
            ViewBag.RoomsId = new SelectList(db.Rooms.Where(h=>h.HousingId==HousingId), "RoomsId", "RoomsId");
            
            return View( decree.ToPagedList(page ?? 1, 5));
        }


        




        // GET: Decrees/Details/5
        [Authorize]
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
        [Authorize]
        public ActionResult Create()
        {
            string login = User.Identity.Name;
            var HousingId = db.Users.Where(u => u.Login == login).Select(s => s.HousingId).FirstOrDefault();

            ViewBag.RoomsId = new SelectList(db.Rooms.Where(r=>r.HousingId==HousingId).Where(f=>f.NumberSeatsFree>0), "RoomsId", "RoomsId");
            ViewBag.StudentsId = new SelectList(db.Students, "StudentsId", "Surname");
            ViewBag.HousingId = new SelectList(db.Housing.Where(h=>h.HousingId==HousingId), "HousingId", "HousingId");
            return View();
        }

        // POST: Decrees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create([Bind(Include = "DecreeId,DateSigning,DateArrival,DateEviction,StudentsId,RoomsId,HousingId")] Decree decree)
        {
            string login = User.Identity.Name;
            var HousingId = db.Users.Where(u => u.Login == login).Select(s => s.HousingId).FirstOrDefault();
            
            var IdRomsAdd = db.Rooms.Where(r => r.RoomsId == decree.RoomsId).Where(h=>h.HousingId== HousingId).FirstOrDefault();
       
            var IdRoomsD = db.Decree.Where(s => s.StudentsId == decree.StudentsId).OrderByDescending(s=>s.DecreeId).FirstOrDefault();
            var IdRomsS = db.Rooms.Where(r => r.RoomsId == IdRoomsD.RoomsId).Where(h => h.HousingId == HousingId).FirstOrDefault() ;
            if (ModelState.IsValid)
            {
                    IdRomsS.NumberSeatsFree = IdRomsS.NumberSeatsFree +1;
                    IdRomsAdd.NumberSeatsFree = IdRomsAdd.NumberSeatsFree - 1;
                    db.Decree.Add(decree);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
            }

            ViewBag.RoomsId = new SelectList(db.Rooms, "RoomsId", "RoomsId", decree.RoomsId);
            ViewBag.StudentsId = new SelectList(db.Students, "StudentsId", "Surname", decree.StudentsId);
            ViewBag.HousingId = new SelectList(db.Housing, "HousingId", "HousingId", decree.HousingId);
            return View(decree);
        }

        //// GET: Decrees/Edit/5
        //[Authorize]
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Decree decree = await db.Decree.FindAsync(id);
        //    if (decree == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.RoomsId = new SelectList(db.Rooms, "RoomsId", "RoomsId", decree.RoomsId);
        //    ViewBag.StudentsId = new SelectList(db.Students, "StudentsId", "Surname", decree.StudentsId);
        //    ViewBag.HousingId = new SelectList(db.Housing, "HousingId", "HousingId", decree.HousingId);
        //    return View(decree);
        //}

        //// POST: Decrees/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize]
        //public async Task<ActionResult> Edit([Bind(Include = "DecreeId,DateSigning,DateArrival,DateEviction,StudentsId,RoomsId,HousingId")] Decree decree)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(decree).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.RoomsId = new SelectList(db.Rooms, "RoomsId", "RoomsId", decree.RoomsId);
        //    ViewBag.StudentsId = new SelectList(db.Students, "StudentsId", "Surname", decree.StudentsId);
        //    ViewBag.HousingId = new SelectList(db.Housing, "HousingId", "HousingId", decree.HousingId);
        //    return View(decree);
        //}

        // GET: Decrees/Delete/5
        [Authorize]
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
        [Authorize]
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
