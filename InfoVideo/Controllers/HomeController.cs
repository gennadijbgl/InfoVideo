using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InfoVideo.Models;

namespace InfoVideo.Controllers
{
    public class HomeController : Controller
    {
        private readonly InfoVideoContext _db = new InfoVideoContext();
        public ActionResult Index()
        {
            return View();
        }

    }
}