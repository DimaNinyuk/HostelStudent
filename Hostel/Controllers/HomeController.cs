using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hostel.Models;
using PagedList;

namespace Hostel.Controllers
{
    public class HomeController : Controller
    {
        HostelStudent db = new HostelStudent();
        [Authorize]
        public ActionResult Index(int? page)
        {
            var rooms=db.Rooms.ToList();
            if (User.Identity.IsAuthenticated)
            {

                ViewBag.Name =User.Identity.Name;
                int? IdLogin = db.Users.Where(u => u.Login == User.Identity.Name).Select(s => s.HousingId).FirstOrDefault();
                ViewBag.Phone = db.Housing.Where(p => p.HousingId == IdLogin).Select(s => s.Phone).FirstOrDefault();
               
                ViewBag.Adress= db.Housing.Where(p => p.HousingId == IdLogin).Select(s => s.Surname).FirstOrDefault();
                rooms = db.Rooms.Where(f => f.NumberSeatsFree >= 1 && f.HousingId == IdLogin).ToList();
            }
           
            
            return View(rooms.ToPagedList(page ?? 1, 12));
        }
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}