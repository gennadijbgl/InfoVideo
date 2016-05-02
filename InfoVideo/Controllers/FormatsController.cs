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
        private InfoVideoContext db = new InfoVideoContext();

        // GET: Formats
        public async Task<ActionResult> Index()
        {
            return View(await db.Format.ToListAsync());
        }

        // GET: Formats/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Format format = await db.Format.FindAsync(id);
            if (format == null)
            {
                return HttpNotFound();
            }
            return View(format);
        }

        // GET: Formats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Formats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Container,Languages,Support3D")] Format format)
        {
            if (ModelState.IsValid)
            {
                db.Format.Add(format);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(format);
        }

        // GET: Formats/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Format format = await db.Format.FindAsync(id);
            if (format == null)
            {
                return HttpNotFound();
            }
            return View(format);
        }

        // POST: Formats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Container,Languages,Support3D")] Format format)
        {
            if (ModelState.IsValid)
            {
                db.Entry(format).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(format);
        }

        // GET: Formats/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Format format = await db.Format.FindAsync(id);
            if (format == null)
            {
                return HttpNotFound();
            }
            return View(format);
        }

        // POST: Formats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Format format = await db.Format.FindAsync(id);
            db.Format.Remove(format);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
