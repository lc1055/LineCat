using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LineCat.Web.Models
{
    public class Product
    {
        public string ID { get; set; }
        public string MallRuleID { get; set; }

        public string Title { get; set; }
        public string Url { get; set; }
        public int Timer { get; set; }
        
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "排序")]
        public int SortNum { get; set; } = 0;
        [Display(Name = "停用")]
        public int Disable { get; set; } = 0;

        public virtual MallRule MallRule { get; set; }
    }
}