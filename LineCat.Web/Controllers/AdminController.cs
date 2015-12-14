using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LineCat.Web.Controllers
{
    public class AdminController : DBController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Delete()
        {
            try
            {
                DateTime d = DateTime.Now.AddDays(-1);
                int r = db.Database.ExecuteSqlCommand("delete from pricehistory where islow=0 and createdate < '" + d.ToString("yyyy-MM-dd") + "'");
                return Json(new { success = true, msg = r });
            }
            catch (Exception e)
            {
                Common.TxtLog.SaveErrorLog(e.ToString());
                return Json(new { success = false, msg = e.Message });
            }
        }
    }
}