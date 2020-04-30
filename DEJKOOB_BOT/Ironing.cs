using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEJKOOB_BOT
{
    static public class Ironing
    {



        /*=====================================
         * start ironing in specific village
         ======================================*/
        static public bool StartIroning(string typeOfForce, string villageName, string level, string armorOrWeapon)
        {
            if (Village.OpenVillage(villageName) &&
                Village.GoToVillageBuldings() &&
                OpenIIroningPlace() &&
                OpenArmorOrWeaponInIroning(armorOrWeapon))
            {
                var result= CheckLevelAndUpgrade(level, typeOfForce);
                return result;
            }

            return false;
        }






        /*===================================
         * open ironing place in current village
         ====================================*/
        static private bool OpenIIroningPlace()
        {
            Village.OpenBuilding("آهنگری");
            return true;
        }






        /*===================================
         * open armor or weapon part in ironing 
         * place
         ====================================*/
        static private bool OpenArmorOrWeaponInIroning(string kind)
        {
            return WebTools.ClickOnFirstParent(By.XPath($"//span[contains(text(),\"{kind}\")]"));
        }







        /*===================================
         * check current level of force in ironing
         * place and click on upgrade btn level 
         * of force
         ====================================*/
        static private bool CheckLevelAndUpgrade(string level, string typeOfForce)
        {
            try
            {
                var infos = WebTools.CallElements(By.XPath("//div[@class=\"information\"]"));
                foreach (var info in infos)
                {
                    var title = info.FindElement(By.XPath(".//div[@class=\"title\"]"));
                    var linkNameTag = title.FindElements(By.XPath(".//a"))[1];
                    if (linkNameTag.Text.Contains(typeOfForce) /*for دژکوب_روم like*/ || typeOfForce.Contains(linkNameTag.Text))
                    {
                        var currentLevel = title.FindElement(By.TagName("span")).Text.Replace("سطح", "").Replace(" ", "");
                        if (Int32.Parse(currentLevel) < Int32.Parse(level))
                        {
                            var contractLink = info.FindElement(By.ClassName("contractLink"));
                            var upgradeBtn = contractLink.FindElement(By.XPath(".//button[@value=\"Upgrade level\"]"));
                            upgradeBtn.Click();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }




    }
}
