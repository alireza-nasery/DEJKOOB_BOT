using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEJKOOB_BOT
{
    static public class Celebration
    {

        /*==================================
         * start celebration in 3 step
         ===================================*/
        static public bool StartCelebration(string villageName, string celebrationKind, bool celebrationAdjustment)
        {
            if (Village.OpenVillage(villageName))
            {
                Village.GoToVillageBuldings();
                if (OpenCelebrationsPlace())
                {
                    if (!IsCelebrationPlaceActive())
                    {
                        var r = celebrationAdjustment ? OpenAdjustmentsPage(celebrationKind) && Village.MakeAdjustments() : false;
                    }
                    var result = HoldingACelebration(celebrationKind);
                    return result;
                }
            }
            return false;
        }





        /*==============================
         * check celebration is active
         ===============================*/
        static private bool IsCelebrationPlaceActive()
        {
            if (WebTools.GetPageSource().Contains("هم اکنون در تالار جشن برپاست"))
            {
                return true;
            }
            return false;
        }
        
        
        
        
        
        /*====================================
         * make adjustments in celabration place
         =====================================*/
        static public bool OpenAdjustmentsPage(string celebrationKind)
        {
            var btns = WebTools.CallElements(By.XPath("//button[@value=\"npc\"]"));
            
            //if there is no celebration kind in place
            try
            {
                switch (celebrationKind)
                {
                    case "جشن کوچک": btns.ElementAt(0).Click(); return true;
                    case "جشن بزرگ": btns.ElementAt(1).Click(); return true;
                    default: return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }












        /*==================================
         * open celebration place
         ===================================*/
        static private bool OpenCelebrationsPlace()
        {
            return Village.OpenBuilding("تالارسطح");
        }






        /*==================================
         * holding a celebratio in village
         ===================================*/
        static private bool HoldingACelebration(string celebrationKind)
        {
            var btns = WebTools.CallElements(By.XPath("//button[@value=\"برگزار شود\"]"));
           
            //if there is no celebration kind in place
            try
            {
                switch (celebrationKind)
                {
                    case "جشن کوچک": btns.ElementAt(0).Click(); return true;
                    case "جشن بزرگ": btns.ElementAt(1).Click(); return true;
                    default: return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
