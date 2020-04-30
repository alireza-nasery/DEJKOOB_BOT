using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEJKOOB_BOT
{
    static public class CompleteConstructionQuickly
    {

        /*===========================
         * start finish now for current 
         * village
         ============================*/
        static public bool StartFinishNow(string villageName)
        {
            if (Village.OpenVillage(villageName) && Village.OpenVillageResources())
            {
                return ClickOnFinishNowLink();
            }
            return false;
        }




        /*============================
         * click on finish now link 
         =============================*/
        static private bool ClickOnFinishNowLink()
        {
            return WebTools.ClickElem(By.XPath("//a[@href=\"?cmd=buildingFinish\"]")) &&
                WebTools.AwnserToAlert(true);
        }



    }
}
