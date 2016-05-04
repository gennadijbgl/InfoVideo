using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
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
           
            if (!ModelState.IsValid)
                return PartialView(model);


            Users user  =  await _db.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);


            if (user != null)
            {

                FormsAuthentication.SetAuthCookie(model.Login, true);
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true });
                }
                return RedirectToAction("Index","Home");
            }

           
            ModelState.AddModelError("", "Карыстач ня знойдзены");

            return PartialView(model);
        }

  
     


        public ActionResult Register()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Users model)
        {
            if (!ModelState.IsValid) return View(model);

            Users user  = await _db.Users.FirstAsync(u => u.Login == model.Login);
              
            if (user == null)
            {
                   
                _db.Users.Add(new Users { Email = model.Email, Address = model.Address,FirstName = model.FirstName, LastName = model.LastName, Password = (model.Password.Trim()), Login = model.Login});
                var ans = _db.SaveChanges();

                if (ans != 1) return View(model);

                FormsAuthentication.SetAuthCookie(model.Login, true);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Такі логін ужо існуе");

            return View(model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }



        public async  Task<PartialViewResult> Index()
        {
            if (User.IsInRole("Administrator"))
            {
                var users = _db.Users.Include(e => e.Roles).Include(e => e.History);
                return PartialView(await users.ToListAsync());

            }
            return PartialView("AuthAdminError");
        }


        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user = await _db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public JsonResult JsonSearch(string email)
        {
            if (User.IsInRole("Administrator"))
            {
                email = email.Trim();
                var user = _db.Users.FirstOrDefault(y => y.Email == email);

                var jsondata = user?.Roles.Name;

                return Json(jsondata, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }



        public async Task<ActionResult> Buy(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Register", "Account");
                }
                Edition model = await _db.Edition.FindAsync(id);
                if (model == null)
                {
                    return HttpNotFound();
                }

                return View(model);
            }
           
                return PartialView("AuthError");
            
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Buy(int Id)
        {
            if (User.Identity.IsAuthenticated)
            {

                if (ModelState.IsValid)
                {
                    Users user = await _db.Users.FirstAsync(t => t.Login == User.Identity.Name);

                    Edition edition = await _db.Edition.FindAsync(Id);

                    user.History.Add(new History {Date = DateTime.Now, Users = user, Edition = edition});

                    _db.Entry(user).State = EntityState.Modified;


                    await _db.SaveChangesAsync();

                    if (Request.IsAjaxRequest())
                    {
                        Thread.Sleep(2000);
                        return Json(new {success = true, responseText = "Your message successfuly sent!"},
                            JsonRequestBehavior.AllowGet);

                    }

                    return RedirectToAction("Index");
                }
                return View();
            }
            return PartialView("AuthError");
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (User.IsInRole("Administrator"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Users model = await _db.Users.FindAsync(id);
                if (model == null)
                {
                    return HttpNotFound();
                }

                ViewBag.Roles = await _db.Roles.ToListAsync();

                return View(model);
            }
            return PartialView("AuthError");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Login,Password,Email,FirstName,LastName,Address,Discount,IdRole")] Users user)
        {
            if (User.IsInRole("Administrator"))
            {
                if (ModelState.IsValid)
                {
                    user.Password = user.Password.Trim();
                    _db.Entry(user).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            return PartialView("AuthError");
        }

    
        public async Task<ActionResult> Delete(int? id)
        {
            if (User.IsInRole("Administrator"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Users user = await _db.Users.FindAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            return PartialView("AuthError");
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (User.IsInRole("Administrator"))
            {
                Users user = await _db.Users.FindAsync(id);
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return PartialView("AuthError");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}