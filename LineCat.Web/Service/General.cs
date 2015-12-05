using LineCat.Web.Common;
using LineCat.Web.Models;
using LineCat.Web.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace LineCat.Web.Service
{
    public class General
    {
        public static string GetHtmlByProduct(Product en)
        {
            try
            {
                LineCatDb db = DbContextFactory.GetCurrentContext();
                MallRule rule = db.MallRule.FirstOrDefault(m => m.ID == en.MallRuleID);

                //step1: get html from url
                string urlToCrawl = en.Url;

                //generate http request
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(urlToCrawl);

                //use GET method to get url's html
                req.Method = string.IsNullOrEmpty(rule.RequestMethod) ? "GET" : rule.RequestMethod;

                //use request to get response
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                //use songtaste's html's charset GB2312 to decode html
                //otherwise will return messy code
                string htmlCharset = string.IsNullOrEmpty(rule.HtmlCharset) ? "utf-8" : rule.HtmlCharset;
                Encoding htmlEncoding = Encoding.GetEncoding(htmlCharset);
                StreamReader sr = new StreamReader(resp.GetResponseStream(), htmlEncoding);

                //read out the returned html
                string respHtml = sr.ReadToEnd();
                return respHtml;
            }
            catch (Exception e)
            {
                //写日志
                TxtLog.SaveErrorLog(e.ToString());
                return "";
            }
        }

        public static string PriceReplace(string price)
        {
            price = price.Replace(",", "").Replace("元", "").Replace("￥", "");
            price = price.Trim();
            return price;
        }

        public static void CatchPriceByAll()
        {
            LineCatDb db = DbContextFactory.GetCurrentContext();

            List<Product> productList = db.Product.Where(m => true).ToList();
            #region 循环遍历规则获取价格
            foreach (Product p in productList)
            {
                if (p.Disable == 1) break;

                MallRule rule = db.MallRule.FirstOrDefault(m => m.ID == p.MallRuleID);

                PriceHistory en = new PriceHistory();
                en.ID = Guid.NewGuid().ToString();
                en.ProductID = p.ID;
                en.Title = p.Title;
                double price = 0;

                string html = GetHtmlByProduct(p);
                if (!string.IsNullOrEmpty(html))
                {
                    string str_regex = rule.PriceMatchStr;// @"<span\s+class=""h_price""\s+id=""ECS_SHOPPRICE"">(?<_price_>.+?)</span>";
                    //MatchCollection mc= Regex.Matches(html, str_regex);
                    //foreach (Match m in mc)
                    //{
                    //}
                    Match match = (new Regex(str_regex)).Match(html);
                    if (match.Success)
                    {
                        //string strPrice = match.Groups["_price_"].Value;
                        //string price_regex = @"([1-9]\d*\.?\d*)|(0\.\d*[1-9])";
                        //Match matchprice = new Regex(price_regex).Match(strPrice);
                        //if (matchprice.Success)
                        //{
                        //    price = Convert.ToDouble(matchprice.Value);
                        //}
                        string strPrice = PriceReplace(match.Groups["_price_"].Value);
                        price = Convert.ToDouble(strPrice);

                        en.Price = price;//价格赋值
                        en.OutStock = 0;//库存赋值

                        PriceHistory low = db.PriceHistory.FirstOrDefault(m => m.ProductID == p.ID && m.IsLow == 1);
                        if (low != null)
                        {
                            if (en.Price <= low.Price)
                            {
                                en.IsLow = 1;//设置当前价格为历史最低
                                low.IsLow = 0;//重置原历史最低
                                db.Entry(low).State = EntityState.Modified;

                                if (en.Price < low.Price)
                                {
                                    TxtLog.WriteLine("productid：" + en.ProductID + "上次历史价格id：" + low.ID);
                                    TxtLog.WriteLine("最新低价：" + en.Price + "，上次低价：" + low.Price);
                                    //email
                                    Utils.SendEmail(en.Title, en.Price, low.Price);
                                }
                            }
                        }
                        else
                        {
                            en.IsLow = 1;//设置当前价格为历史最低
                        }
                    }
                }
                else
                {
                    en.Title = "[未获取到网页源] " + en.Title;
                }
                
                db.PriceHistory.Add(en);//记录数据库        
                db.SaveChanges();
            }//循环结束
            
            #endregion
        }
    }
}