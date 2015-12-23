namespace LineCat.Web.Migrations
{
    using Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<LineCat.Web.Repository.LineCatDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LineCat.Web.Repository.LineCatDb context)
        {
            //  This method will be called after migrating to the latest version.

            var Malls = new List<Mall>
            {
                new Mall() {  ID="1", Url="amazon.co.jp", Name="日本亚马逊" }
            };
            Malls.ForEach(p => context.Mall.AddOrUpdate(m => m.ID, p));

            var MallRules = new List<MallRule>
            {
                new MallRule() {  ID="1", MallID="1", Title="日亚默认抓取规则",
                    HtmlCharset ="utf-8", RequestMethod="get",
                    TitleMatchStr ="",
                    PriceMatchStr ="<span id=\"priceblock_ourprice\" class=\"a-size-medium a-color-price\">(?<_price_>.+?)</span>",
                    Timer =5 }
            };
            MallRules.ForEach(p => context.MallRule.AddOrUpdate(m => m.ID, p));

            var Products = new List<Product>
            {
                new Product() {  ID="1", MallRuleID="1", Url="http://www.amazon.co.jp/dp/B00MC6TVZ0", Title="松下521台灯 银", RecommendAlertPrice=8000 }
            };
            Products.ForEach(p => context.Product.AddOrUpdate(m => m.ID, p));

            context.SaveChanges();
        }
    }
}
