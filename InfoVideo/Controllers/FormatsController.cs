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
    public class FormatsController : Controller
    {
        private readonly InfoVideoContext _db = new InfoVideoContext();

      
        public async Task<ActionResult> Index()
        {
            return View(await _db.Format.ToListAsync());
        }

        
        public async Task<ActionResult> Details(int? id)
        {
            if (User.IsInRole("Administrator"))
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Format format = await _db.Format.FindAsync(id);
            if (format == null)
            {
                return HttpNotFound();
            }
            return View(format);
            }
            return PartialView("AuthAdminError");
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
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Create([Bind(Include = "Id,Container,Languages,Support3D")] Format format)
        {
            if (ModelState.IsValid)
            {
                _db.Format.Add(format);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(format);
        }

     
        public async Task<ActionResult> Edit(int? id)
        {
            if (User.IsInRole("Administrator"))
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Format format = await _db.Format.FindAsync(id);
            if (format == null)
            {
                return HttpNotFound();
            }
            return View(format);
            }
            return PartialView("AuthAdminError");
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Container,Languages,Support3D")] Format format)
        {
            if (User.IsInRole("Administrator"))
            {
                if (ModelState.IsValid)
            {
                _db.Entry(format).State = System.Data.Entity.EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(format);
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
            Format format = await _db.Format.FindAsync(id);
            if (format == null)
            {
                return HttpNotFound();
            }
            return View(format);
            }
            return PartialView("AuthAdminError");
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (User.IsInRole("Administrator"))
            {
                Format format = await _db.Format.FindAsync(id);
            _db.Format.Remove(format);
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
