﻿using System;
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
            ModelState.AddModelError("", "Пользователь с таким логином уже существует");

            return View(model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        public async  Task<PartialViewResult> Index()
        {

            var users = _db.Users.Include(e => e.Roles).Include(e => e.History);
            return PartialView(await users.ToListAsync());
           
            
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
            email = email.Trim();
            var user = _db.Users.FirstOrDefault(y => y.Email == email);

            var jsondata = user?.Roles.Name;

            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }



        public async Task<ActionResult> Buy(int? id)
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

  

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<ActionResult> Buy(int Id)
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
                    return Json(new { success = true, responseText = "Your message successfuly sent!" }, JsonRequestBehavior.AllowGet);

                }

                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<ActionResult> Edit(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Login,Password,Email,FirstName,LastName,Address,Discount,IdRole")] Users user)
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

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
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

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Users user = await _db.Users.FindAsync(id);
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
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