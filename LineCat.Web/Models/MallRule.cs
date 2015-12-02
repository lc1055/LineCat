using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LineCat.Web.Models
{
    public class MallRule
    {
        public string ID { get; set; }
        public string MallID { get; set; }

        public string Title { get; set; }
        public string HtmlCharset { get; set; }
        public string RequestMethod { get; set; }
        public string TitleMatchStr { get; set; }
        public string PriceMatchStr { get; set; }
        public int Timer { get; set; }
        
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "排序")]
        public int SortNum { get; set; } = 0;
        [Display(Name = "停用")]
        public int Disable { get; set; } = 0;

        public virtual Mall Mall { get; set; }
    }
    
}