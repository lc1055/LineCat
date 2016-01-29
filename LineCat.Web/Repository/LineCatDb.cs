using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SQLite.CodeFirst;//第三方库实现sqlite的codefirst模式
using System.Collections.Generic;
using LineCat.Web.Models;

namespace LineCat.Web.Repository
{
    //enable-migrations //-contexttypename [LineCatDb]
    //add-migration [InitialDB]


    public class LineCatDb : DbContext
    {
        //public LineCatDb() : base("HuoxingPlatform") { }
        public LineCatDb() : base("LineCat") { }
        
        public DbSet<Mall> Mall { get; set; }
        public DbSet<MallRule> MallRule { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<PriceHistory> PriceHistory { get; set; }

        public DbSet<UserProduct> UserProduct { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //防止表中的数据以复数(Students,Courses,Enrollments)命名
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //使用sqllite时，global中初始化。使用mssql时，参照自动迁移（migrations）。
            //Database.SetInitializer(new LineCatDbInitializer(modelBuilder));
        }

    }

    /// <summary>
    /// SqliteDropCreateDatabaseAlways
    /// SqliteCreateDatabaseIfNotExists
    /// SqliteDropCreateDatabaseWhenModelChanges
    /// </summary>
    public class LineCatDbInitializer : SqliteDropCreateDatabaseAlways<LineCatDb>
    {
        public LineCatDbInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
        { }

        protected override void Seed(LineCatDb context)
        {
            var Malls = new List<Mall>
            {
                new Mall() {  ID="1", Url="amazon.co.jp", Name="日本亚马逊" }
            };
            Malls.ForEach(p => context.Mall.Add(p));

            var MallRules = new List<MallRule>
            {
                new MallRule() {  ID="1", MallID="1", Title="日亚默认抓取规则",
                    HtmlCharset ="utf-8", RequestMethod="get",
                    TitleMatchStr ="",
                    PriceMatchStr ="<span id=\"priceblock_ourprice\" class=\"a-size-medium a-color-price\">(?<_price_>.+?)</span>",
                    Timer =5 }
            };
            MallRules.ForEach(p => context.MallRule.Add(p));

            var Products = new List<Product>
            {
                new Product() {ID="B00HYOGTQ4",MallRuleID="1",Title="象印 sa36 金", Url="http://www.amazon.co.jp/dp/B00HYOGTQ4", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B00HYOGTS2",MallRuleID="1",Title="象印 sa36 红", Url="http://www.amazon.co.jp/dp/B00HYOGTS2", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B00HYOGTU0",MallRuleID="1",Title="象印 sa48 金", Url="http://www.amazon.co.jp/dp/B00HYOGTU0", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B00HYOGUKY",MallRuleID="1",Title="象印 sa48 粉", Url="http://www.amazon.co.jp/dp/B00HYOGUKY", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B00HYOGSYC",MallRuleID="1",Title="象印 SA36 黑", Url="http://www.amazon.co.jp/dp/B00HYOGSYC", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B00HYOGTR8",MallRuleID="1",Title="象印 sa36 粉", Url="http://www.amazon.co.jp/dp/B00HYOGTR8", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B00HYOGTTG",MallRuleID="1",Title="象印 sa48 黑", Url="http://www.amazon.co.jp/dp/B00HYOGTTG", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B00HYOGRZC",MallRuleID="1",Title="象印 SD-EA08 黑红", Url="http://www.amazon.co.jp/gp/product/B00HYOGRZC", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},

                new Product() {ID="B00M1ECBOQ",MallRuleID="1",Title="膳魔师 jnl-500 粉", Url="http://www.amazon.co.jp/dp/B00M1ECBOQ", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B008YC1F3M",MallRuleID="1",Title="膳魔师 jnl350 棕", Url="http://www.amazon.co.jp/dp/B008YC1F3M", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B00M1EC6PA",MallRuleID="1",Title="膳魔师 jnl350 粉", Url="http://www.amazon.co.jp/dp/B00M1EC6PA", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B014KAQFT0",MallRuleID="1",Title="膳魔师 jnl402 nv-y", Url="http://www.amazon.co.jp/dp/B014KAQFT0", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B014KAQIQU",MallRuleID="1",Title="膳魔师 jnl402 p-b", Url="http://www.amazon.co.jp/dp/B014KAQIQU", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B014KAQLKS",MallRuleID="1",Title="膳魔师 jnl402 blwh", Url="http://www.amazon.co.jp/dp/B014KAQLKS", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B014KAQCRK",MallRuleID="1",Title="膳魔师 jnl402 pkw", Url="http://www.amazon.co.jp/dp/B014KAQCRK", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},        
                new Product() {ID="B013SEKBIU",MallRuleID="1",Title="膳魔师 jnl-502 粉", Url="http://www.amazon.co.jp/dp/B013SEKBIU", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B00IDYAF6O",MallRuleID="1",Title="膳魔师 FFZ-800F 黑黄", Url="http://www.amazon.co.jp/gp/product/B00IDYAF6O", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B013SEIU9M",MallRuleID="1",Title="膳魔师 jnl-502 棕", Url="http://www.amazon.co.jp/dp/B013SEIU9M", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B00EI61SMQ",MallRuleID="1",Title="膳魔师 jni-301 粉", Url="http://www.amazon.co.jp/dp/B00EI61SMQ", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},             
                new Product() {ID="B013SEKGEO",MallRuleID="1",Title="膳魔师 jnl-352 棕", Url="http://www.amazon.co.jp/dp/B013SEKGEO", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},        
                new Product() {ID="B013SEJ6UE",MallRuleID="1",Title="膳魔师 jnl-352 粉", Url="http://www.amazon.co.jp/dp/B013SEJ6UE", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},               
                new Product() {ID="B00IDYAF7I",MallRuleID="1",Title="膳魔师 ffz-800f 黑红", Url="http://www.amazon.co.jp/dp/B00IDYAF7I", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B013SEK6EE",MallRuleID="1",Title="膳魔师 jns-350 蓝", Url="http://www.amazon.co.jp/dp/B013SEK6EE", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B013SEKGC6",MallRuleID="1",Title="膳魔师 JNL-352 绿", Url="http://www.amazon.co.jp/dp/B013SEKGC6", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},

                new Product() {ID="B00E3JHLWY",MallRuleID="1",Title="虎牌 mmy-a036 黑", Url="http://www.amazon.co.jp/dp/B00E3JHLWY", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B00MN84HQA",MallRuleID="1",Title="虎牌 mmy-a036 红", Url="http://www.amazon.co.jp/dp/B00MN84HQA", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B00MN84BA2",MallRuleID="1",Title="虎牌 mmz-a035 白", Url="http://www.amazon.co.jp/dp/B00MN84BA2", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B013OKRY4W",MallRuleID="1",Title="虎牌 mmp-g031 白", Url="http://www.amazon.co.jp/dp/B013OKRY4W", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B00E3JI966",MallRuleID="1",Title="虎牌 mmy-a036 白", Url="http://www.amazon.co.jp/dp/B00E3JI966", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B00E3JJT7Y",MallRuleID="1",Title="虎牌 mmy-a048 白", Url="http://www.amazon.co.jp/dp/B00E3JJT7Y", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},
                new Product() {ID="B013OKS0G8",MallRuleID="1",Title="虎牌 MMJ-a036 白", Url="http://www.amazon.co.jp/dp/B013OKS0G8", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 2000},

                new Product() {ID="B00MC6TVZ0",MallRuleID="1",Title="松下521台灯 银", Url="http://www.amazon.co.jp/dp/B00MC6TVZ0", Timer=0,CreateDate=DateTime.Now, SortNum=0,Disable=   0, RecommendAlertPrice= 8000}

            };
            Products.ForEach(p => context.Product.Add(p));

            context.SaveChanges();
        }
    }

}