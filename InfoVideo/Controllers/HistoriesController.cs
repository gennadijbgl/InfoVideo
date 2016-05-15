using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using InfoVideo.Models;
using Newtonsoft.Json;

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

        public async Task<ActionResult> Statistics()
        {
           return View();
        }


        public string GetByDate(int yearDisplay = 2016)
        {
            DateTimeFormatInfo mfi = new DateTimeFormatInfo();
            var b = _db.History.Include(t => t.Edition).GroupBy(t=>t.Date).ToList();

            var y = _db.History.Min(t => t.Date.Year);

            var changesPerYearAndMonth =
          
            from month in Enumerable.Range(1, 12)
            let key = new { Year = yearDisplay, Month = month }
            join revision in b on key
                      equals new
                      {
                          revision.Key.Year,
                          revision.Key.Month
                      } into g
            select new {  key.Year, Month= mfi.GetMonthName(key.Month), Count = g.Count() };


            var c =  JsonConvert.SerializeObject(changesPerYearAndMonth, Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            PreserveReferencesHandling = PreserveReferencesHandling.Objects
                        });

            return c;

        }


        public string GetByVideo(int yearDisplay = 2016)
        {

            var b = _db.History.GroupBy(t => t.Edition.Video.Title).Select(
                group => new {
                Title = group.Key,
                Count = group.Count()
            }).ToList();
                                           


            var c = JsonConvert.SerializeObject(b, Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            PreserveReferencesHandling = PreserveReferencesHandling.Objects
                        });

            return c;

        }


        public string GetByUser(int yearDisplay = 2016)
        {

            var b = _db.History.GroupBy(t => t.Users.Login).Select(
                group => new {
                    Login = group.Key,
                    Count = group.Count()
                }).ToList();



            var c = JsonConvert.SerializeObject(b, Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            PreserveReferencesHandling = PreserveReferencesHandling.Objects
                        });

            return c;

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

            var u = _db.Users.FirstOrDefault(t => t.Login == User.Identity.Name);

            if (u != null || User.IsInRole("Administrator"))

                return View(history);

            return PartialView("AuthAdminError");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdUser,IdEdition,Date")] History history)
        {
            if (User.IsInRole("Administrator"))
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
            History history = await _db.History.FindAsync(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEdition = new SelectList(_db.Edition, "Id", "Box", history.IdEdition);
            ViewBag.IdUser = new SelectList(_db.Users, "Id", "Login", history.IdUser);
            return View(history);
            }
            return PartialView("AuthAdminError");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IdUser,IdEdition,Date")] History history)
        {
            if (User.IsInRole("Administrator"))
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
