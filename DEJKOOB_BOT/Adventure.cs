using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEJKOOB_BOT
{
    static public class Adventure
    {


        /*================================
         * start adventure in 4 steps
         =================================*/
        static public bool StartAdventure()
        {
            GoToAdventuresPage();
            if (IsAnyAdventureExist())
            {
                SelectAdventure();
                return SendToAdventure();
            }
            return false;
        }



        /*================================
         * click on send hero to adventure btn
         =================================*/
        static private bool SendToAdventure()
        {
            return WebTools.ClickElem(By.Id("btn_ok"));
        }



        /*================================
         * select adventure from list of that
         =================================*/
        static private void SelectAdventure()
        {
            WebTools.ClickElem(By.XPath("//tbody/tr/td[@class=\"goTo\"]/a[@class=\"gotoAdventure arrow\"]"));
        }


        /*================================
         * go to adventure page in user panel
         =================================*/
        static private void GoToAdventuresPage()
        {
            WebTools.ClickElem(By.Id("button5225ee283b28a"));
        }




        /*================================
         * check adventure list for any adventure
         =================================*/
        static private bool IsAnyAdventureExist()
        {
            return (WebTools.CallElement(By.XPath("//tbody/tr")) != null ? true : false);
        }

    }
}
