using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LineCat.Web.Models
{
    public class UserProduct
    {
        [Key, Display(Name = "ID"), StringLength(50)]
        public string ID { get; set; }
        [Display(Name = "UserID"), StringLength(50)]
        public string UserID { get; set; }
        [Display(Name = "ProductID"), StringLength(50)]
        public string ProductID { get; set; }
        [Display(Name = "Alert Price")]
        public double AlertPrice { get; set; } = 0;

        [Display(Name = "Created Date")]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Sort Number")]
        public int SortNum { get; set; } = 0;
        [Display(Name = "Disabled")]
        public int Disable { get; set; } = 0;

        public virtual Product Product { get; set; }
    }
}