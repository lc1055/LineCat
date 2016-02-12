using LineCat.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LineCat.Web.Areas.A.Controllers
{
    public class PController : DBController
    {
        public ActionResult Index(string id)
        {
            var p = db.Product.FirstOrDefault(m => m.ID == id);
            return View(p);
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
                        en.MallRuleID = mallRule.ID;                 
                        if (string.IsNullOrEmpty(en.ID))
                        {
                            en.ID = Guid.NewGuid().ToString(); 
                        }
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

        [HttpPost]
        public ActionResult Edit(Models.Product en)
        {
            ViewBag.msg = "";
            try
            {
                if (!string.IsNullOrEmpty(en.ID))
                {
                    var product = db.Product.FirstOrDefault(m => m.ID == en.ID);
                    if (product != null)
                    {
                        product.Url = en.Url;
                        product.RecommendAlertPrice = en.RecommendAlertPrice;
                        product.Title = en.Title;
                        db.SaveChanges();
                        ViewBag.msg = "edit success";
                    }
                    else
                    {
                        ViewBag.msg = "product not exisit";
                    }
                }
                else
                {
                    ViewBag.msg = "product not exisit";
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