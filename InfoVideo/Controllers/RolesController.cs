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
        private readonly InfoVideoContext _db = new InfoVideoContext();


        public async Task<ActionResult> Index()
        {
            return View(await _db.Roles.ToListAsync());
        }


        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles Roles = await _db.Roles.FindAsync(id);
            if (Roles == null)
            {
                return HttpNotFound();
            }
            return View(Roles);
        }


        public ActionResult Create()
        {
            return View();
        }

        public ActionResult AddUserRoles()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] Roles Roles)
        {
            if (ModelState.IsValid)
            {
                _db.Roles.Add(Roles);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(Roles);
        }


        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles Roles = await _db.Roles.FindAsync(id);
            if (Roles == null)
            {
                return HttpNotFound();
            }
            return View(Roles);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] Roles Roles)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(Roles).State = System.Data.Entity.EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(Roles);
        }


        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles Roles = await _db.Roles.FindAsync(id);
            if (Roles == null)
            {
                return HttpNotFound();
            }
            return View(Roles);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Roles Roles = await _db.Roles.FindAsync(id);
            _db.Roles.Remove(Roles);
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
