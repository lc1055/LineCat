using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LineCat.Web.Models
{
    public class Category
    {
        [Key, Display(Name = "分类id"), StringLength(50)]
        public string ID { get; set; }

        [Display(Name = "父id"), StringLength(50)]
        public string PID { get; set; }

        [Display(Name = "分类编号"), StringLength(50)]
        public string CategoryCode { get; set; }

        [Display(Name = "分类名称"), StringLength(50)]
        public string CategoryName { get; set; }

        [Display(Name = "前端显示名称"), StringLength(50)]
        public string DisplayName { get; set; }
        
        [Display(Name = "图标"), StringLength(100)]
        public string Icon { get; set; }

        [Display(Name = "图片"), StringLength(100)]
        public string Image { get; set; }

        [Display(Name = "排序")]
        public int SortNum { get; set; } = 0;

        [Display(Name = "停用")]
        public int Disable { get; set; } = 0;

        [Display(Name = "创建时间")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Display(Name = "创建人id")]
        public string CreateUserID { get; set; }


        public virtual ICollection<Brand> Brands { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }

    
}