using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LineCat.Web.Models
{
    /// <summary>
    /// product sku
    /// </summary>
    public class Product
    {
        [Key, Display(Name = "ID"), StringLength(50)]
        public string ID { get; set; }
        [Display(Name = "MallRuleID"), StringLength(50)]
        public string MallRuleID { get; set; }

        [Display(Name = "Product Name"), StringLength(200)]
        public string Title { get; set; }
        [Display(Name = "Brand ID"), StringLength(200)]
        public string BrandID { get; set; }

        [Display(Name = "Url"), StringLength(200)]
        public string Url { get; set; }
        public double RecommendAlertPrice { get; set; } = 0;//系统推荐的提醒价格
        public int Timer { get; set; } = 0;
        
        [Display(Name = "Created Date")]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Sort Number")]
        public int SortNum { get; set; } = 0;
        [Display(Name = "Disabled")]
        public int Disable { get; set; } = 0;

        public virtual MallRule MallRule { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}