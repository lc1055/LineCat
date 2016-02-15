using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineCat.Web.Models
{
    public class CategoryX
    {
    }

    public class CategoryBrand
    {
        public string CategoryID { get; set; }
        public string BrandID { get; set; }

        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
    }

    public class CategoryProduct
    {
        public string CategoryID { get; set; }
        public string ProductID { get; set; }

        public virtual Category Category { get; set; }
        public virtual Product Product { get; set; }
    }
}