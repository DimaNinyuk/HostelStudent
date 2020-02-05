using System.Linq;
using System.Web.Security;
using System.Web.Mvc;
using Hostel.Models;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Cookies;

namespace Hostel.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                Users user = null;
                using (HostelStudent db = new HostelStudent())
                {
                    user = db.Users.FirstOrDefault(u => u.Login == model.Login && u.Possword == model.Password);

                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }
        public ActionResult Logout()
        {
            
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}