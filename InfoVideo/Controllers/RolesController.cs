using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InfoVideo.Models;

namespace InfoVideo.Controllers
{
    public class RolesController : Controller
    {
        private readonly InfoVideoEntities _db = new InfoVideoEntities();


        public ActionResult Index()
        {
            if (User.IsInRole("Administrator"))
            {

                return View(_db.Roles.ToList());
            }
            return PartialView("AuthAdminError");

        }


        public async Task<ActionResult> Details(int? id)
        {
            if (!User.IsInRole("Administrator")) return PartialView("AuthAdminError");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Roles roles = await _db.Roles.FindAsync(id);

            if (roles == null)
            {
                return HttpNotFound();
            }
            return View(roles);
        }


        public ActionResult Create()
        {
            if (User.IsInRole("Administrator"))
            {
                return View();
            }
            return PartialView("AuthAdminError");
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] Roles Roles)
        {
            if (User.IsInRole("Administrator"))
            {
                if (!ModelState.IsValid) return View(Roles);
                if (_db.Roles.FirstOrDefault(t => t.Name.Equals(Roles.Name)) != null)
                {
                    ModelState.AddModelError("", "Такая роля існуе");
                    return  View(Roles);
                }
                _db.Roles.Add(Roles);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return PartialView("AuthAdminError");
        }


        public async Task<ActionResult> Edit(int? id)
        {
            if (User.IsInRole("Administrator"))
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles roles = await _db.Roles.FindAsync(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return View(roles);
            }
            return PartialView("AuthAdminError");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] Roles roles)
        {
            if (User.IsInRole("Administrator"))
            {
                if (ModelState.IsValid)
            {
                _db.Entry(roles).State = System.Data.Entity.EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(roles);
            }
            return PartialView("AuthAdminError");
        }


        public async Task<ActionResult> Delete(int? id)
        {
            if (User.IsInRole("Administrator"))
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles roles = await _db.Roles.FindAsync(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return View(roles);
            }
            return PartialView("AuthAdminError");
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (User.IsInRole("Administrator"))
            {
                Roles roles = await _db.Roles.FindAsync(id);
                if (roles.Users.Count > 0)
                {
                    ModelState.AddModelError("", "Выдаліце залежнасці ад гэтага відыё");
                    return View(roles);
                }
                _db.Roles.Remove(roles);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
            }
            return PartialView("AuthAdminError");
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
