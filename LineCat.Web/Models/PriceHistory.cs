using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LineCat.Web.Models
{
    public class PriceHistory
    {
        [Key, Display(Name = "ID"), StringLength(50)]
        public string ID { get; set; }
        [StringLength(50)]
        public string ProductID { get; set; }
        [StringLength(200)]
        public string Title { get; set; }
        public double Price { get; set; } = 0;
        public int IsLow { get; set; } = 0;
        public int OutStock { get; set; } = 0;

        [Display(Name = "Created Date")]
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