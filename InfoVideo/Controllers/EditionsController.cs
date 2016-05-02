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
    public class EditionsController : Controller
    {
        private readonly InfoVideoContext _db = new InfoVideoContext();

        // GET: Editions
        public async Task<ActionResult> Index()
        {
            var edition = _db.Edition.Include(e => e.Format).Include(e => e.Video);
            return View(await edition.ToListAsync());
        }

        // GET: Editions/Details/5
        public async Task<ActionResult> Details(int? id)
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

        // GET: Editions/Create
        public ActionResult Create()
        {
            ViewBag.IdFormat = new SelectList(_db.Format, "Id", "Container");
            ViewBag.IdVideo = new SelectList(_db.Video, "Id", "Title");
            return View();
        }

        // POST: Editions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdVideo,IdFormat,Price,Box")] Edition edition)
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

        // GET: Editions/Edit/5
        public async Task<ActionResult> Edit(int? id)
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

        // POST: Editions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IdVideo,IdFormat,Price,Box")] Edition edition)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(edition).State = System.Data.Entity.EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdFormat = new SelectList(_db.Format, "Id", "Container", edition.IdFormat);
            ViewBag.IdVideo = new SelectList(_db.Video, "Id", "Title", edition.IdVideo);
            return View(edition);
        }

        // GET: Editions/Delete/5
        public async Task<ActionResult> Delete(int? id)
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

        // POST: Editions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Edition edition = await _db.Edition.FindAsync(id);
            _db.Edition.Remove(edition);
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
