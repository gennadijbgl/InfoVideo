using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using InfoVideo.Models;

namespace InfoVideo.Controllers
{
    public class HomeController : Controller
    {
        private readonly InfoVideoContext _db = new InfoVideoContext();
        public async Task<ActionResult> Index()
        {
            ViewData["Latest"] = await _db.Edition.ToListAsync();

            return View(await _db.Video.ToListAsync());
        }

        public ActionResult Proc()
        {
         
            return View();
        }
        public  ActionResult ProcType(string type = "mp4")
        {
            var c = _db.GetVideoByType(type);

            return PartialView(c);
        }

    }
}