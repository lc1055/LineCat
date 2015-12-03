﻿using LineCat.Web.Service;
using System.Linq;
using System.Web.Mvc;

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
            
            var list = db.PriceHistory.Where(m => true)
                .OrderByDescending(m => m.CreateDate)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            
            
            return View(list);
        }


        public ActionResult History(string id, int? page, int? rows)
        {
            //起始页
            int pageIndex = page ?? 1;
            //每页显示的条数
            int pageSize = rows ?? GlobalPageSize;
            
            var list = db.PriceHistory.Where(m => m.ProductID == id)
                .OrderByDescending(m => m.IsLow)
                .OrderByDescending(m => m.CreateDate)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();


            return View(list);
        }
    }
}