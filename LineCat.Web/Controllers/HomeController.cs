using LineCat.Web.Service;
using System.Linq;
using System.Web.Mvc;

namespace LineCat.Web.Controllers
{
    public class HomeController : DBController
    {
        public ActionResult Index(string id, string key, int? page, int? rows)
        {
            if (Common.Utils.HostPing("121.41.57.205"))
            {
                //起始页
                int pageIndex = page ?? 1;
                //每页显示的条数
                int pageSize = rows ?? GlobalPageSize;

                var list = db.PriceHistory.Where(m => (string.IsNullOrEmpty(id) || m.ProductID == id)
                    && (string.IsNullOrEmpty(key) || m.Title.Contains(key))
                    ).OrderByDescending(m => m.CreateDate)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();


                return View(list);
            }
            else
            {
                return View();
            }
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