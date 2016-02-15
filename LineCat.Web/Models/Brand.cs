using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LineCat.Web.Models
{
    /// <summary>
    /// 商品品牌
    /// </summary>
    public class Brand
    {
        [Key, Display(Name = "品牌id"), StringLength(50)]
        public string ID { get; set; }

        [Display(Name = "品牌编号"), StringLength(50)]
        public string BrandCode { get; set; }

        [Display(Name = "品牌名称"), StringLength(50)]
        public string BrandName { get; set; }

        [Display(Name = "次要品牌名称"), StringLength(50)]
        public string DisplayName { get; set; }

        [Display(Name = "官网"), StringLength(100)]
        public string OfficialSite { get; set; }

        [Display(Name = "品牌摘要"), StringLength(500)]
        public string BrandRemark { get; set; }

        [Display(Name = "品牌介绍")]
        public string BrandDetails { get; set; }

        [Display(Name = "key"), StringLength(200)]
        public string MetaKey { get; set; }

        [Display(Name = "desc"), StringLength(500)]
        public string MetaDesc { get; set; }

        [Display(Name = "图标"), StringLength(100)]
        public string BrandIcon { get; set; }

        [Display(Name = "排序")]
        public int SortNum { get; set; } = 0;

        [Display(Name = "停用")]
        public int Disable { get; set; } = 0;

        [Display(Name = "创建时间")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Display(Name = "创建人id"), StringLength(50)]
        public string CreateUserID { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}