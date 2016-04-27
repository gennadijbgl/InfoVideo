using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using InfoVideo.Models;
using Newtonsoft.Json;


namespace InfoVideo.Controllers
{
    public class AccountController : Controller
    {
        private readonly InfoVideoContext _db = new InfoVideoContext();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid) return PartialView(model);

            User user  = await _db.Users.FirstAsync(u => u.Login == model.Login && u.Login == model.Login);


            if (user != null)
            {

                FormsAuthentication.SetAuthCookie(model.Login, true);    
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");

            return PartialView(model);
        }
     
        public async Task<JsonResult> LoginAjax(LoginModel model)
        {
            if (!ModelState.IsValid) return Json(false, JsonRequestBehavior.AllowGet);

            User user  = await _db.Users.FirstAsync(u => u.Login == model.Login && u.Login == model.Login);


            if (user == null) return Json(false, JsonRequestBehavior.AllowGet);

            FormsAuthentication.SetAuthCookie(model.Login, true);

            return Json(true, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(User model)
        {
            if (!ModelState.IsValid) return View(model);

            User user  = await _db.Users.FirstAsync(u => u.Login == model.Login);
              
            if (user == null)
            {
                   
                _db.Users.Add(new User { Email = model.Email, Address = model.Address,FirstName = model.FirstName, LastName = model.LastName, Password = (model.Password), Login = model.Login});
                var ans = _db.SaveChanges();

                if (ans != 1) return View(model);

                FormsAuthentication.SetAuthCookie(model.Email, true);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Пользователь с таким логином уже существует");

            return View(model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        public async  Task<PartialViewResult> UsersList()
        {
            
                return PartialView(await _db.Users.ToListAsync());
            
        }

        public JsonResult JsonSearch(string email)
        {
            var user = _db.Users.First(y => y.Email == email);

            var jsondata = user?.UserRoles.Select(t=>t.Role.Name);

            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }
    }
}