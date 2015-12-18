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

        [HttpPost]
        public ActionResult Create(Models.Product en)
        {
            ViewBag.msg = "";
            try
            {
                var product = db.Product.FirstOrDefault(m => m.Url == en.Url);
                if (product == null)
                {
                    var mallRule = db.MallRule.FirstOrDefault(m =>
                    m.MallID == db.Mall.FirstOrDefault(mm => en.Url.Contains(mm.Url)).ID
                    );
                    if (mallRule != null)
                    {
                        en.ID = Guid.NewGuid().ToString();
                        en.MallRuleID = mallRule.ID;
                        db.Product.Add(en);
                        db.SaveChanges();
                        ViewBag.msg = "created success";
                    }
                    else
                    {
                        ViewBag.msg = "created failed";
                    }
                }
                else
                {
                    ViewBag.msg = "url exisit";
                }
            }
            catch (Exception e)
            {
                ViewBag.msg = e.ToString();
            }
            
            return View("index");
        }

    }
}