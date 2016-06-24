using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InfoVideo.Models;

namespace InfoVideo.Controllers
{
    public class VideosController : Controller
    {
        private readonly InfoVideoEntities _db = new InfoVideoEntities();

        public async Task<ActionResult> Index()
        {

            return View(await _db.Video.ToListAsync());
        }

        
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = await _db.Video.FindAsync(id);
            
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

     
        public ActionResult Create()
        {
            if (User.IsInRole("Administrator"))
            {
                return View();
            }
            return PartialView("AuthAdminError");
        }

        public void ProcessFile(HttpPostedFileBase file, Video video)
        {
            if (file != null && file.ContentLength > 0)

            {
                string path = Path.Combine(Server.MapPath("~/Media/Images"),
                    Path.GetFileName(file.FileName));

                file.SaveAs(path);

                video.Logo = Path.GetFileName(file.FileName);
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description,Country,Genre")] Video video, HttpPostedFileBase Logo)
        {
            if (User.IsInRole("Administrator"))
            {
                if (ModelState.IsValid)
            {
                
                ProcessFile(Logo, video);

                _db.Video.Add(video);
              await _db.SaveChangesAsync();

              
                return RedirectToAction("Index");
            }

            return View(video);
            }
            return PartialView("AuthAdminError");
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
           
            return View();
        }

 
        public async Task<ActionResult> Edit(int? id)
        {
            if (User.IsInRole("Administrator"))
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = await _db.Video.FindAsync(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
            }
            return PartialView("AuthAdminError");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,Date,Genre")] Video video, HttpPostedFileBase Logo)
        {
            if (User.IsInRole("Administrator"))
            {
                if (ModelState.IsValid)
            {
                _db.Entry(video).State = System.Data.Entity.EntityState.Modified;
                ProcessFile(Logo, video);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(video);
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
            Video video = await _db.Video.FindAsync(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
            }
            return PartialView("AuthAdminError");
        }

 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (User.IsInRole("Administrator"))
            {
                Video video = await _db.Video.FindAsync(id);
                if (video.Edition.Count > 0)
                {
                    ModelState.AddModelError("", "Выдаліце залежнасці ад гэтага відыё");
                    return View(video);
                }
                _db.Video.Remove(video);
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
