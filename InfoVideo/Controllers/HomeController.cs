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
            return View(await _db.Video.ToListAsync());
        }

    }
}