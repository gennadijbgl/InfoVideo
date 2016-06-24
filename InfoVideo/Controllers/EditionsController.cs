using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InfoVideo.Models;


namespace InfoVideo.Controllers
{
    public class EditionsController : Controller
    {
        private readonly InfoVideoEntities _db = new InfoVideoEntities();

   
        public async Task<ActionResult> Index()
        {
            var edition = _db.Edition.Include(e => e.Format).Include(e => e.Video); ;

            return View(await edition.ToListAsync());
        }


        public ActionResult Details(int? id)
        {
            return RedirectToAction("Details", "Videos", new { id });

        }


        public ActionResult Create()
        {
            if (User.IsInRole("Administrator"))
            {
                ViewBag.IdFormat = new SelectList(_db.Format, "Id", "Container");
                ViewBag.IdVideo = new SelectList(_db.Video, "Id", "Title");
                return View();
            }
            return PartialView("AuthAdminError");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdVideo,IdFormat,Price,Box")] Edition edition)
        {
            if (User.IsInRole("Administrator"))
            {
                if (ModelState.IsValid)
                {
                 
                    _db.Edition.Add(edition);
                    await _db.SaveChangesAsync();
        
                return RedirectToAction("Index");
            }

            ViewBag.IdFormat = new SelectList(_db.Format, "Id", "Container", edition.IdFormat);
            ViewBag.IdVideo = new SelectList(_db.Video, "Id", "Title", edition.IdVideo);

            return View(edition);
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
            Edition edition = await _db.Edition.FindAsync(id);
            if (edition == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdFormat = new SelectList(_db.Format, "Id", "Container", edition.IdFormat);
            ViewBag.IdVideo = new SelectList(_db.Video, "Id", "Title", edition.IdVideo);
            return View(edition);
            }
            return PartialView("AuthAdminError");
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IdVideo,IdFormat,Price,Box")] Edition edition)
        {
            if (User.IsInRole("Administrator"))
            {
                if (ModelState.IsValid)
            {


                    _db.Entry(edition).State =  EntityState.Modified;

                    var c = await _db.SaveChangesAsync();

                    if (c == 1)
                        return RedirectToAction("Index");  
                
            }
            ViewBag.IdFormat = new SelectList(_db.Format, "Id", "Container", edition.IdFormat);
            ViewBag.IdVideo = new SelectList(_db.Video, "Id", "Title", edition.IdVideo);
            return View(edition);
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
            Edition edition = await _db.Edition.FindAsync(id);
            if (edition == null)
            {
                return HttpNotFound();
            }
            return View(edition);
            }
            return PartialView("AuthAdminError");
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (User.IsInRole("Administrator"))
            {
                var c = await _db.Edition.FindAsync(id);

                _db.Edition.Remove(c);

         
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
