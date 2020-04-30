using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DEJKOOB_BOT
{
    static public class Village
    {


        /*=====================================
         * open village building map in user village
         ======================================*/
        static public bool OpenVillage(string villageName)
        {
            try
            {
                var villages = WebTools.CallElement(By.Id("sidebarBoxVillagelist")).
                FindElement(By.XPath(".//div[@class=\"innerBox content\"]")).
                FindElements(By.TagName("li"));
                var links = villages.Select(place => place.FindElement(By.TagName("a")));
                foreach (var li in links)
                {
                    var txt = li.FindElements(By.TagName("div"))[0].Text;
                    if (txt == villageName)
                    {
                        li.Click();
                        break;
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        /*=========================================
         * return all buildings names in specific
         * village
         ==========================================*/
        static public List<string> GetAllBuildingsNameInVilageByVillageName(string villageName)
        {
            try
            {
                OpenVillage(villageName);
                GoToVillageBuldings();
                var buildings = WebTools.CallElement(By.Id("village_map")).FindElements(By.XPath("//img[contains(@class,\"building\")]"));
                var buildingsNamesList = buildings.Select(im =>
                    Regex.Replace(
                    im.GetAttribute("alt").Replace("سطح", "").Replace("محل ساخت", ""),
                    @"[\d-]",
                    string.Empty)).Where(i => !String.IsNullOrEmpty(i)).ToList();

                return buildingsNamesList;
            }
            catch (Exception)
            {
                return null;
            }
        }




        /*==================================
        * open village building map in village
        ===================================*/
        static public bool GoToVillageBuldings()
        {
            return WebTools.ClickElem(By.XPath("//li[@id=\"n2\"]/a[@href=\"dorf2.php\"]"));
        }






        /*==================================
         * open specific bulding by title name
         ===================================*/
        static public bool OpenBuilding(string value)
        {
            try
            {
                var places = WebTools.CallElement(By.Id("village_map")).FindElements(By.XPath(".//img[contains(@class,\"building\") or " +
                    "contains(@class,\"dx1\")]"/*ردوگاه */));
                var co = 0;
                foreach (var elem in places)
                {
                    co++;
                    var alt = elem.GetAttribute("alt");
                    if (alt.Contains(value))
                    {
                        WebTools.CallElement(By.Id("clickareas")).FindElements(By.TagName("area"))[co - 1].Click();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }



        /*====================================
         * make adjustments in celabration place
         =====================================*/
        static public bool MakeAdjustments()
        {
            return WebTools.ClickElem(By.XPath("//button[@value=\"منابع را تعدیل کن (مرحله 1 از 2)\"]")) &&
                WebTools.ClickElem(By.XPath("//button[@value=\"منابع را معامله کن. (مرحله 2 از 2)\"]")) &&
                /*back to place*/WebTools.ClickElem(By.XPath("//a[contains(text(), \"بازگشت به ساختمان\")]"));
        }




        /*================================
         * chose adjustments in page
         =================================*/
        static public bool ChooseAdjustments(int numberOfAjustmentsInPage)
        {
            try
            {
                var btns = WebTools.CallElements(By.XPath("//button[@value=\"npc\"]"));
                var imgs = WebTools.CallElements(By.XPath("//img[@class=\"npc\"]"));
                if (btns != null && btns.Count() != 0)
                {
                    btns.ElementAt(numberOfAjustmentsInPage).Click();
                    return true;
                }
                if (imgs != null && imgs.Count() != 0)
                {
                    var img = imgs.ElementAt(numberOfAjustmentsInPage);
                    var link = WebTools.ExecuteJavascriptOnPage($"return arguments[0].parentNode", img);
                    link.Click();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }



        /*================================
         * return list of village name from
         * user panel
         =================================*/
        static public List<string> GetListOfVillageNames()
        {
            try
            {
                var villages = WebTools.CallElement(By.Id("sidebarBoxVillagelist")).
            FindElement(By.XPath(".//div[@class=\"innerBox content\"]")).
            FindElements(By.TagName("li"));
                var names = villages.Select(v => v.FindElement(By.XPath(".//a/div[@class=\"name\"]")).Text).ToList();
                return names;
            }
            catch (Exception e)
            {
                return null;
            }
        }




        /*=================================
         * abou village resource
         ==================================*/
        static private string[] VillageResource = { "چوب:lbar1", "خشت:lbar2", "آهن:lbar3", "گندم:lbar4" };
        static public bool IsResourceAboveThePercentage(string percentageAdjustment)
        {
            try
            {
                for (int i = 0; i < VillageResource.Length; i++)
                {
                    var n = WebTools.CallElement(By.Id(VillageResource[i].Split(':')[1])).GetCssValue("width").Replace("px", "");
                    if (float.Parse(n) > float.Parse(percentageAdjustment))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }



        /*==================================
         * open village resources
         ===================================*/
        static public bool OpenVillageResources()
        {
            return WebTools.ClickElem(By.Id("n1"));
        }



        /*=================================
        * open heroes item in user panel
        ==================================*/
        static public bool OpenHeroStuff()
        {
            try
            {
                WebTools.ClickElem(By.Id("heroImageButton"));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /*=====================================
         * choose hat in heroes items
         ======================================*/
        static public bool ChooseHat(string code)
        {
            try
            {
                var heroItems = WebTools.CallElement(By.Id("itemsToSale"));
                var items = heroItems.FindElements(By.XPath("//div[@class=\"inventory draggable\"]"));
                foreach (var item in items)
                {
                    var attr = item.FindElement(By.XPath(".//div")).GetAttribute("class");
                    if (attr.Contains(code))
                    {
                        item.Click();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /*======================================
         * get list of web element village resources 
         =======================================*/
        static public int GetCurrentVillageResourcesNumber()
        {
            try
            {
                var map = WebTools.CallElement(By.Id("rx"));
                var list = map.FindElements(By.XPath("//area[contains(@href,\"build\")]")).ToList();
                return list.Count();
            }
            catch (Exception e)
            {
                return 0;
            }
        }





    }
}
