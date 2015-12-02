using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineCat.Web.Models
{
    public class Mall
    {
        public string ID { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int SortNum { get; set; } = 0;
        public int Disable { get; set; } = 0;
    }
}