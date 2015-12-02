using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineCat.Web.Models
{
    public class PriceHistory
    {
        public string ID { get; set; }
        public string ProductID { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int IsLow { get; set; } = 0;
        public int OutStock { get; set; } = 0;
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public virtual Product Product { get; set; }
    }

    //public class PriceHistoryViewModel
    //{
    //    public string ID { get; set; }
    //    public string ProductID { get; set; }
    //    public string Title { get; set; }
    //    public double Price { get; set; }
    //    public int IsLow { get; set; } = 0;
    //    public int OutStock { get; set; } = 0;
    //    public DateTime CreateDate { get; set; } = DateTime.Now;
    //}
}