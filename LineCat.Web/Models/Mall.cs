using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LineCat.Web.Models
{
    public class Mall
    {
        [Key, Display(Name = "ID"), StringLength(50)]
        public string ID { get; set; }
        [Display(Name = "Url"), StringLength(200)]
        public string Url { get; set; }
        [Display(Name = "Name"), StringLength(200)]
        public string Name { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Sort Number")]
        public int SortNum { get; set; } = 0;
        [Display(Name = "Disabled")]
        public int Disable { get; set; } = 0;
    }
}