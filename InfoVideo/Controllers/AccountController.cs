using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
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
        private readonly InfoVideoEntities _db = new InfoVideoEntities();

       
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

            Users user  = await _db.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
              
            if (user == null)
            {
                _db.Users.Add(model);
                var c = await _db.SaveChangesAsync();

                if (c!=1)
                {
                    ModelState.AddModelError("", "Памылка");
                    return View(model);
                    
                }

                FormsAuthentication.SetAuthCookie(model.Login, true);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Ужо існуе");
            return View(model);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }



        public async  Task<PartialViewResult> Index(int? discount,string isUp)
        {
           
            var isU = isUp?.Equals("on") ?? false;
            if (!User.IsInRole("Administrator")) return PartialView("AuthAdminError");

      

            var users = discount != null ? 

                _db.Users.Include(e => e.Roles).Include(e => e.History)
                    .Where(t => isU? t.Discount>=discount:t.Discount==discount)

                : _db.Users.Include(e => e.Roles).Include(e => e.History);

            return Request.IsAjaxRequest() ? PartialView("List", await users.ToListAsync()) 
                 : PartialView(await users.ToListAsync());
        }


        public async Task<ActionResult> Details(int? id)
        {
            Users user = null;
            if (id == null)
            {
                if (!User.Identity.IsAuthenticated) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                user = await _db.Users.FirstOrDefaultAsync(t => t.Login.Equals(User.Identity.Name));
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }

             user = _db.Users.Find(id);
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

                var jsondata = user?.Roles?.Name;

                return Json(jsondata, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }



        public async Task<ActionResult> Buy(int? id)
        {
            if (!User.Identity.IsAuthenticated) return PartialView("AuthError");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Register", "Account");
            }

            var model =await _db.Edition.FindAsync(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
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

                    History h = new History() {Date = DateTime.Now, Edition = edition,Users = user};

                    h.CalculatePriceBuy();

                    _db.History.Add(h);

                    var c = await _db.SaveChangesAsync();

                    if (Request.IsAjaxRequest())
                    {
                        Thread.Sleep(2000);
                        return Json(c==1 ? new {success = true, responseText = ""} : new { success = false, responseText = "Памылка" }, JsonRequestBehavior.AllowGet);
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

                Users model =await _db.Users.FindAsync(id);

                if (model == null)
                {
                    return HttpNotFound();
                }

                ViewBag.Roles =  _db.Roles.ToListAsync();

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
                     

                    _db.Entry(user).State = EntityState.Modified;
                    var c = await _db.SaveChangesAsync();

                    if(c==1)
                    return RedirectToAction("Index");

                 
                    return View(user);
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
                Users user =await _db.Users.FindAsync(id);
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