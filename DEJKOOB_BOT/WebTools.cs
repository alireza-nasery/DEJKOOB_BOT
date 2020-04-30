using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace DEJKOOB_BOT
{
    /// <summary>
    /// selenium tools for web automation
    /// </summary>
    static public class WebTools
    {
        static WebDriverWait wait;
        static IJavaScriptExecutor js;
        static IWebDriver driver;
        static bool IsInitialize = false;
        static ChromeOptions Options;
        /// <summary>
        /// initial driver,option,...
        /// </summary>
        static public void Initialize(bool browser)
        {
            if (!IsInitialize)
            {
                Options = new ChromeOptions();
                Options.AddArguments("headless");
                Options.AddArgument("window-size=1920x1080");
                if (browser)
                {
                    driver = new ChromeDriver(/*Options*/);
                }
                else
                {
                    driver = new ChromeDriver(Options);
                }
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20/*maximum time*/));
                js = (IJavaScriptExecutor)driver;
                IsInitialize = true;
            }
        }





        /// <summary>
        /// click on first parents of specific element
        /// </summary>
        /// <param name="by"></param>
        /// <returns>return true if selenium can click on element</returns>
        static public bool ClickOnFirstParent(By by)
        {
            try
            {
                var child = CallElement(by);
                var parent = ExecuteJavascriptOnPage($"return arguments[0].parentNode;", child);
                parent.Click();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }






        /// <summary>
        /// find element in page,if no element exists after 20 secend return null
        /// </summary>
        /// <param name="by"></param>
        /// <returns>null</returns>
        static public IWebElement CallElement(By by)
        {
            try
            {
                Thread.Sleep(1000);
                //https://stackoverflow.com/questions/49866334/c-sharp-selenium-expectedconditions-is-obsolete
                return wait.Until(ExpectedConditions.ElementExists(by));
            }
            catch (Exception e)
            {
                return null;
            }
        }





        /// <summary>
        /// refresh page
        /// </summary>
        /// <returns>is no exception throw return true if not return false</returns>
        static public bool RefreshPage()
        {
            try
            {
                driver.Navigate().Refresh();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }





        /// <summary>
        /// find elements in page,if no elements exists after 20 secend return null
        /// </summary>
        /// <param name="by"></param>
        /// <returns>null</returns>
        static public IEnumerable<IWebElement> CallElements(By by)
        {
            try
            {
                Thread.Sleep(1000);
                //https://stackoverflow.com/questions/49866334/c-sharp-selenium-expectedconditions-is-obsolete
                return wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
            }
            catch (Exception e)
            {
                return null;
            }
        }









        /// <summary>
        /// execute javascript in page
        /// </summary>
        /// <param name="jsCode">javascript code</param>
        /// <returns>result of execution(string)</returns>
        static public string ExecuteJavascriptOnPage(string jsCode)
        {
            try
            {
                var result = (string)js.ExecuteScript(jsCode);
                return result;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }



        /// <summary>   
        /// execute javascript in page
        /// </summary>
        /// <param name="jsCode">javascript code</param>
        /// <returns>result of execution(web element)</returns>
        static public IWebElement ExecuteJavascriptOnPage(string jsCode, params object[] args)
        {
            try
            {
                var result = (IWebElement)js.ExecuteScript(jsCode, args);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }











        /// <summary>
        /// get HTML source of page
        /// </summary>
        /// <returns>string of html</returns>
        static public string GetPageSource()
        {
            try
            {
                return driver.PageSource;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }







        /// <summary>
        /// navigate page to specific url
        /// </summary>
        /// <param name="urlPage">current url</param>
        static public bool NavigateToPage(string urlPage)
        {
            try
            {
                driver.Navigate().GoToUrl(urlPage);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }








        /// <summary>
        /// click on specific element
        /// </summary>
        /// <param name="by"></param>
        /// <returns>if element exists then click and return true if not return false</returns>
        static public bool ClickElem(By by)
        {
            return IsElementClickable(by);
        }





        /// <summary>
        /// click on element
        /// </summary>
        /// <param name="by"></param>
        /// <returns> if it is so return true if not return false</returns>
        static private bool IsElementClickable(By by)
        {
            try
            {
                CallElement(by).Click();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }







        /// <summary>
        /// fill input in page
        /// </summary>
        /// <param name="by"></param>
        /// <param name="key"></param>
        /// <returns>if element exists then fill that and return true if not return false</returns>
        static public bool FillInput(By by, string key)
        {
            try
            {
                //no more \n\r
                key = key.Replace("\n", "").Replace("\r", "");

                CallElement(by).SendKeys(key);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }




        /// <summary>
        /// awnser to alert like confirm
        /// </summary>
        /// <param name="choose">true for OK,false for cancle</param>
        /// <returns>if no exception throw return true if not return false</returns>
        static public bool AwnserToAlert(bool choose)
        {
            try
            {
                var alert = driver.SwitchTo().Alert();
                if (choose)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
