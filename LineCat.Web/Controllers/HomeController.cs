using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using LineCat.Web.Repository;

namespace LineCat.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //LineCatDb db = new LineCatDb();
            
            return View();
        }
    }
}