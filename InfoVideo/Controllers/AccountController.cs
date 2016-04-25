using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using InfoVideo.Models;


namespace InfoVideo.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
             
                User user = null;
                using (InfoVideoContext db = new InfoVideoContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email);

                }
                if (user != null)
                {
                 
                    FormsAuthentication.SetAuthCookie(model.Email, true);

                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1,
        user.Email,
        DateTime.Now,
        DateTime.Now.AddDays(90),
        true,
        string.Empty);

                    // add cookie to response stream         
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    System.Web.HttpCookie authCookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    if (authTicket.IsPersistent)
                    {
                        authCookie.Expires = authTicket.Expiration;
                    }
                    System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return PartialView(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User model)
        {
       
            if (ModelState.IsValid)
            {
                User user = null;
                using (InfoVideoContext db = new InfoVideoContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email);
                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (InfoVideoContext db = new InfoVideoContext())
                    {
                        db.Users.Add(new User { Email = model.Email, Address = model.Address,FirstName = model.FirstName, LastName = model.LastName, Password = (model.Password)});
                        db.SaveChanges();

                        user = db.Users.FirstOrDefault(u => u.Email == model.Email && u.Address == model.Address);
                    }
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}