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
    public class HistoriesController : Controller
    {
        private readonly InfoVideoContext _db = new InfoVideoContext();


        public async Task<ActionResult> Index()
        {
            if (User.IsInRole("Administrator"))
            {
                var history = _db.History.Include(h => h.Edition).Include(h => h.Users);
                return View(await history.ToListAsync());
            }
            else
            {
                var history = _db.History.Where(t=>t.Users.Login == User.Identity.Name).Include(h => h.Edition).Include(h => h.Users);
                return View(await history.ToListAsync());
            }
        }


        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = await _db.History.FindAsync(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            return View(history);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdUser,IdEdition,Date")] History history)
        {
            if (ModelState.IsValid)
            {
                _db.History.Add(history);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdEdition = new SelectList(_db.Edition, "Id", "Box", history.IdEdition);
            ViewBag.IdUser = new SelectList(_db.Users, "Id", "Login", history.IdUser);
            return View(history);
        }

 
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = await _db.History.FindAsync(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEdition = new SelectList(_db.Edition, "Id", "Box", history.IdEdition);
            ViewBag.IdUser = new SelectList(_db.Users, "Id", "Login", history.IdUser);
            return View(history);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IdUser,IdEdition,Date")] History history)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(history).State = System.Data.Entity.EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdEdition = new SelectList(_db.Edition, "Id", "Box", history.IdEdition);
            ViewBag.IdUser = new SelectList(_db.Users, "Id", "Login", history.IdUser);
            return View(history);
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
