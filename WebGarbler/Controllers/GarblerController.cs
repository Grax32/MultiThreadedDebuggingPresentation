using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebGarbler.Controllers
{
    public class GarblerController : Controller
    {
        // GET: Garbler
        public ActionResult Index()
        {
            return View();
        }
    }
}