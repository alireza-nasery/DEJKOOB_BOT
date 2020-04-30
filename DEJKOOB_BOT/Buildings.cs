using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEJKOOB_BOT
{
    static public class Buildings
    {

        /*========================================
         * start upgrading buildings
         =========================================*/
        static public bool StartUpgradeBuildings(string buildingName, string villageName, string level)
        {
            if (Village.OpenVillage(villageName) &&
                Village.GoToVillageBuldings() &&
                Village.OpenBuilding(buildingName))
            {
                return CheckLevelAndUpgradeVurrentBuilding(level);
            }
            return false;
        }


        /*=================================
         * check current buildings level
         * and upgrade that
         ==================================*/
        static private bool CheckLevelAndUpgradeVurrentBuilding(string level)
        {
            try
            {
                var currentLevel = string.Empty;
                var elem = WebTools.CallElement(By.ClassName("titleInHeader"));
                if (elem == null)
                {
                    elem = WebTools.CallElement(By.ClassName("level"));
                    currentLevel = elem.Text.Replace("سطح ", "");
                }
                else
                {
                    currentLevel = elem.FindElement(By.XPath(".//span[@class=\"level\"]")).Text.Replace("سطح ", "");
                }
                if (Int32.Parse(level) > Int32.Parse(currentLevel))
                {
                    var contract = WebTools.CallElement(By.Id("contract"));
                    var b = contract.FindElements(By.XPath(".//div[@style=\"display: block;\"]"));
                    var button = b[0].FindElement(By.XPath(".//button"));
                    button.Click();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return true;
            }
        }


    }
}
