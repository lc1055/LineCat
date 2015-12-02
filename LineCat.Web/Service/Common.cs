﻿using LineCat.Web.Models;
using LineCat.Web.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace LineCat.Web.Service
{
    public class Common
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

        public static void GetPrice()
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

                string html = Common.GetHtmlByProduct(p);
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
                        string strPrice = Common.PriceReplace(match.Groups["_price_"].Value);
                        price = Convert.ToDouble(strPrice);

                        en.Price = price;//价格
                        en.OutStock = 0;//库存

                        PriceHistory low = db.PriceHistory.FirstOrDefault(m => m.ProductID == p.ID && m.IsLow == 1);
                        if (low != null)
                        {
                            if ( en.Price <= low.Price )
                            {
                                en.IsLow = 1;
                                low.IsLow = 0;
                                General.SendEmail();
                                if (en.Price < low.Price)
                                {
                                    //email
                                }
                            }
                        }
                        else
                        {
                            en.IsLow = 1;
                        }
                    }
                }
                else
                {
                    en.Title = "[未获取到网页源] " + en.Title;
                }
                db.PriceHistory.Add(en);//记录数据库             
            }
            db.SaveChanges();
            #endregion
        }
    }
}