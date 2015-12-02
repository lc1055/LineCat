using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LineCat.Web.Models
{
    public class UserProduct
    {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string ProductID { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "排序")]
        public int SortNum { get; set; } = 0;
        [Display(Name = "停用")]
        public int Disable { get; set; } = 0;

        public virtual Product Product { get; set; }
    }
}