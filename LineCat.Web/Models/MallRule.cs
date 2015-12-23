using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LineCat.Web.Models
{
    public class MallRule
    {
        [Key, Display(Name = "ID"), StringLength(50)]
        public string ID { get; set; }
        [Display(Name = "MallID"), StringLength(50)]
        public string MallID { get; set; }

        [Display(Name = "Title"), StringLength(200)]
        public string Title { get; set; }
        [Display(Name = "HtmlCharset"), StringLength(200)]
        public string HtmlCharset { get; set; }
        [Display(Name = "RequestMethod"), StringLength(200)]
        public string RequestMethod { get; set; }
        [Display(Name = "TitleMatchStr"), StringLength(200)]
        public string TitleMatchStr { get; set; }
        [Display(Name = "PriceMatchStr"), StringLength(200)]
        public string PriceMatchStr { get; set; }
        [Display(Name = "StockMatchStr"), StringLength(200)]
        public string StockMatchStr { get; set; }

        public int Timer { get; set; } = 0;

        [Display(Name = "Created Date")]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Sort Number")]
        public int SortNum { get; set; } = 0;
        [Display(Name = "Disabled")]
        public int Disable { get; set; } = 0;

        public virtual Mall Mall { get; set; }
    }
    
}