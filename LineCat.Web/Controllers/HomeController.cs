using LineCat.Web.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LineCat.Web.Repository;

namespace LineCat.Web.Controllers
{
    public class HomeController : DBController
    {
        // GET: Home
        public ActionResult Index(int? page, int? rows)
        {
            //起始页
            int pageIndex = page ?? 1;
            //每页显示的条数
            int pageSize = rows ?? GlobalPageSize;

            //Common.GetPrice();
            var list = db.PriceHistory.Where(m => true).OrderByDescending(m => m.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            
            
            return View(list);
        }
    }
}