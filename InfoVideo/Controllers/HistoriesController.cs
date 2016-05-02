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

        // GET: Histories
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

        // GET: Histories/Details/5
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

        // GET: Histories/Create
        public ActionResult Create()
        {
            ViewBag.IdEdition = new SelectList(_db.Edition, "Id", "Box");
            ViewBag.IdUser = new SelectList(_db.Users, "Id", "Login");
            return View();
        }

        // POST: Histories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Histories/Edit/5
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

        // POST: Histories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Histories/Delete/5
        public async Task<ActionResult> Delete(int? id)
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

        // POST: Histories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            History history = await _db.History.FindAsync(id);
            _db.History.Remove(history);
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
