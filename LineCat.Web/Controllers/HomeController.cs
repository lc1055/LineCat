using LineCat.Web.Service;
using System.Linq;
using System.Web.Mvc;
using System;
using System.Text.RegularExpressions;

namespace LineCat.Web.Controllers
{
    public class HomeController : DBController
    {
        public ActionResult Index(string id, string key, int? page, int? rows)
        {
            ////起始页
            //int pageIndex = page ?? 1;
            ////每页显示的条数
            //int pageSize = rows ?? GlobalPageSize;

            //var list = db.PriceHistory.Where(m => (string.IsNullOrEmpty(id) || m.ProductID == id)
            //    && (string.IsNullOrEmpty(key) || m.Title.Contains(key))
            //    ).OrderByDescending(m => m.CreateDate)
            //    .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            //return View(list);
            return View();
        }

        public ActionResult GetProducts(string brandId, string key)
        {
            //使用sqlserver
            //var list = db.Product.Where(m => (string.IsNullOrEmpty(brandId) || m.BrandID == brandId)
            //    && (string.IsNullOrEmpty(key) || m.Title.Contains(key))
            //    ).OrderByDescending(m => m.SortNum).Select(p => new LatestPrice
            //    {
            //        Price = db.PriceHistory.FirstOrDefault(h => h.ProductID == p.ID).Price,
            //        IsLow = db.PriceHistory.FirstOrDefault(h => h.ProductID == p.ID).IsLow,
            //        ID = p.ID,
            //        Title = p.Title,
            //        Url = p.Url,
            //        BrandID = p.BrandID,
            //        CreateDate = db.PriceHistory.FirstOrDefault(h => h.ProductID == p.ID).CreateDate,
            //    }).ToList();

            //使用sqlite
            var list = db.Product.Where(m => (string.IsNullOrEmpty(brandId) || m.BrandID == brandId)
                && (string.IsNullOrEmpty(key) || m.Title.Contains(key))
                ).OrderByDescending(m => m.SortNum).Select(p => new LatestPrice
                {
                    ID = p.ID,
                    Title = p.Title,
                    Url = p.Url,
                    BrandID = p.BrandID
                }).ToList();
            foreach(var i in list)
            {
                var history = db.PriceHistory.OrderByDescending(h => h.CreateDate).FirstOrDefault(h => h.ProductID == i.ID);
                if (history != null)
                {
                    i.Price = history.Price;
                    i.IsLow = history.IsLow;
                    i.CreateDate = history.CreateDate;
                }

            }
            
            return Json(list);
        }
        public class LatestPrice
        {
            public string ID { get; set; }
            public string Title { get; set; }
            public string Url { get; set; }
            public string BrandID { get; set; }
            public double Price { get; set; }
            public int IsLow { get; set; }
            public DateTime CreateDate { get; set; }
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


        public ActionResult Match()
        {
            return View();
        }

        public ActionResult CatchSource(string Url)
        {
            string msg = "";
            double price = 0;
            string html = General.GetHtmlByUrl(Url);
            string str_regex= @"<span\s+class=""h_price""\s+id=""ECS_SHOPPRICE"">(?<_price_>.+?)</span>";
            Match match = (new Regex(str_regex)).Match(html);
            if (match.Success)
            {
                string strPrice = General.PriceReplace(match.Groups["_price_"].Value);
                price = Convert.ToDouble(strPrice);
            }
            else
            {
                msg = "[未匹配到价格]";
            }

            ViewBag.Url = Url;           
            ViewBag.Source = html;
            ViewBag.PriceMatchStr = str_regex;
            ViewBag.Price = price;
            ViewBag.Msg = msg;

            return View("match");
        }
    }
}