using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Text.RegularExpressions;

namespace DEJKOOB_BOT
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {




            //var driver = new ChromeDriver();
            //driver.Navigate().GoToUrl("file:///C:/Users/alire/source/repos/DEJKOOB_BOT/DEJKOOB_BOT/bin/Debug/html.html");

            //var map = driver.FindElement(By.Id("rx"));
            //var list = map.FindElements(By.XPath(xpath)).ToList();
            //list[0].Click();
            //map = driver.FindElement(By.Id("rx"));
            //list = map.FindElements(By.XPath(xpath)).ToList();
            //list[1].Click();
            //var num = list.Count;
            //for (int i = 0; i < num; i++)
            //{
            //    var data = map.FindElements(By.XPath(xpath)).ToList()[i];
            //    data.Click();
            //}

            //Thread.Sleep(2000);
            //Thread.Sleep(2000);

            //Console.WriteLine();






            //var list = imgs.Select(im =>
            //Regex.Replace(
            //    im.GetAttribute("alt").Replace("سطح", "").Replace("محل ساخت", ""),
            //    @"[\d-]",
            //    string.Empty)).Where(i => !String.IsNullOrEmpty(i)).ToList();




            //foreach (var img in imgs)
            //{
            //    var alt = img.GetAttribute("alt").Replace("سطح", "");
            //    if (alt.Contains("محل ساخت"))
            //    {
            //        continue;
            //    }
            //    var data = Regex.Replace(alt, @"[\d-]", string.Empty);
            //}



            //var link = driver.FindElement(By.XPath("//a[@href=\"?cmd=buildingFinish\"]"));
            //link.Click();
            //Thread.Sleep(1000);
            //driver.SwitchTo().Alert().Accept();
            //Thread.Sleep(1000);
            //Thread.Sleep(1000);
            //Thread.Sleep(1000);
            //Thread.Sleep(1000);

            //Console.WriteLine();


            //var dr = new ChromeDriver();
            //dr.Navigate().GoToUrl("http://e.s.dejkoob.net/login.php");
            //dr.FindElement(By.Name("user")).SendKeys("zahhak");
            //dr.FindElement(By.Name("pwtraa")).SendKeys("1900622");
            //dr.FindElement(By.Name("captcha")).SendKeys("10");
            //dr.FindElement(By.Name("s1")).Click();

            //Console.WriteLine();



            //List<(string data,string time)> List = new List<(string data, string time)>();

            //for(int i = 0; i < 10; i++)
            //{
            //    List.Add((i.ToString(), i.ToString()));
            //}
            //List.Single(x => x.data == "1").data="sdsdsdsd";

            //Console.WriteLine();




            //var btns = driver.FindElements(By.XPath("//button[@value=\"npc\"]"));
            //var imgs = driver.FindElements(By.XPath("//img[@class=\"npc\"]"));
            //if (btns.Count() != 0)
            //{
            //    btns.ElementAt(2).Click();
            //}
            //if (imgs.Count() != 0)
            //{
            //    var img = imgs.ElementAt(2);
            //    var link = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript($"return arguments[0].parentNode;",img);
            //    link.Click();
            //}
            //Console.WriteLine();

            //var elem = driver.FindElement(By.Id("itemsToSale"));
            //var g = elem.FindElements(By.XPath("//div[@class=\"inventory draggable\"]"));
            //foreach(var inventory in g)
            //{
            //    //var title = item.FindElement(By.XPath(".//div[@title=\"کلاه خود فرمانروا||زمان تربیت در سربازخانه به میزان 20% کاهش خواهد یافت\"]"));
            //    var div = inventory.FindElement(By.XPath(".//div")).GetAttribute("id");
            //    if (div != null)
            //    {

            //    }
            //}



            //        var villages = driver.FindElement(By.Id("sidebarBoxVillagelist")).
            //FindElement(By.XPath(".//div[@class=\"innerBox content\"]")).
            //FindElements(By.TagName("li"));
            //        var names = villages.Select(v => v.FindElement(By.XPath(".//a/div[@class=\"name\"]")).Text).ToList();

            //        Console.WriteLine();



            //var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("headless");

            //using (var browser = new ChromeDriver(chromeOptions))
            //{
            //    browser.Navigate().GoToUrl("https://stackoverflow.com/questions/45130993/how-to-start-chromedriver-in-headless-mode");
            //    var d = browser.PageSource;
            //}
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
