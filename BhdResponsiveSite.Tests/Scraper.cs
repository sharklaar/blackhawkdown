using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace BhdResponsiveSite.Tests
{
    [TestFixture]
    public class Scraper
    {
        [Test]
        public void GetAllLikes()
        {
            const string likePageUrl = "https://www.facebook.com/browse/?type=page_fans&page_id=103168016397619";
            var driver = new FirefoxDriver();
            driver.Navigate().GoToUrl(likePageUrl);

            var itemCount = driver.FindElementsByClassName("adminableItem").Count;

            for (var i = 0; i < 5000; i++)
            {
                driver.Keyboard.SendKeys(Keys.PageDown);
                driver.Keyboard.SendKeys(Keys.PageDown);
                driver.Keyboard.SendKeys(Keys.PageDown);
                driver.Keyboard.SendKeys(Keys.PageDown);
                Thread.Sleep(3000);
                var newItemCount = driver.FindElementsByClassName("adminableItem").Count;
                if (newItemCount != itemCount)
                {
                    itemCount = newItemCount;
                }
                else
                {
                    break;
                }
                i++;
            }

            var items = driver.FindElementsByCssSelector(".adminableItem a");
            using (var file = new System.IO.StreamWriter(@"C:\dev\blackhawkdown\likers.txt"))
            {
                foreach (var item in items)
                {

                        file.WriteLine(item.GetAttribute("href"));

                }
            }
        }
    }
}
