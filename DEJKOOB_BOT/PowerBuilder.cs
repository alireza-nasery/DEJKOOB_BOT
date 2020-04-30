using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEJKOOB_BOT
{
    static public class PowerBuilder
    {


        static private string[] TypeOfForce = {
            /*toten*/"گرزدار:t11", "نیزه دار:t12", "تبرزن:t13", "جاسوس:t14", "دلاور:t15","شوالیه یاغی:t16", "دژکوب_توتن:t17","منجنیق_توتن:t18" ,
        /*gool*/"سرباز پیاده:t21","شمشیرزن_گول:t22","ردیاب:t23","رعد:t24","کاهن سواره:t25","شوالیه شکارچیان:t26", "دژکوب_گول:t27","منجنیق_گول:t28",
        /*rom*/"سرباز لژیون:t1","محافظ:t2","شمشیرزن_روم:t3","خبرچین:t4","شوالیه:t5","شوالیه سزار:t6","دژکوب_روم:t7","منجنیق آتشین:t8"};
        static private string[] NumberOfForce = { 
            /*sarbaz khone:*/ /*toten*/ "t11:0", "t12:1", "t13:2", "t14:3",   /*rom*/"t1:0","t2:1","t3:2",   /*gool*/ "t21:0","t22:1",
            /*establ:*/   /*toten*/"t15:0", "t16:1",   /*rom*/ "t4:0","t5:1","t6:2",   /*gool*/ "t23:0","t24:1","t25:2","t26:3",
            /*kargah*/    /*toten*/"t17:0", "t18:1",   /*rom*/"t27:0","t28:1",      /*gool*/"t7:0","t8:1" };
        static private string[] NumberOfHat = { "کلاه خود فرمانروا:item_15" , "کلاه خود سوارکار:item_10", "کلاه خود جنگجویان:item_14",
            "کلاه خود سواره نظام:item_11" ,"کلاه خود سربازها:item_13","کلاه خود سواره نظام عالی رتبه:item_12"};
        /*==================================
         * start Build Strength (start make forces)
         ===================================*/
        static public bool StartBuildStrength(string villageName, string forceBuilding, string numberOfStaff, string typeOfForce
            , string percentageAdjustment, string typeOfHat)
        {

            try
            {
                var hatCode = NumberOfHat.SingleOrDefault(h => h.Contains(typeOfHat)).Split(':')[1];
                Village.OpenHeroStuff();
                Village.ChooseHat(hatCode);
            }
            catch (Exception)
            {

            }


            if (Village.OpenVillage(villageName))
            {
                Village.GoToVillageBuldings();
                if (OpenForceBuilding(forceBuilding))
                {
                    var code = TypeOfForce.Single(s => s.Contains(typeOfForce)).Split(':')[1];

                    if (Village.IsResourceAboveThePercentage(percentageAdjustment))
                    {
                        var number = NumberOfForce.Single(f => f.Contains(code)).Split(':')[1];
                        if (Village.ChooseAdjustments(Int32.Parse(number)))
                        {
                            Village.MakeAdjustments();
                        }
                    }

                    if (EnterNumberOfStrength(numberOfStaff, code) && StartTraining())
                    {
                        return true;
                    }
                }
            }
            return false;
        }






        /*=======================================
         * open up force building
         ========================================*/
        static private bool OpenForceBuilding(string forceBuildingKind)
        {
            return Village.OpenBuilding(forceBuildingKind);
        }


        /*=======================================
         * fill number of force
         ========================================*/
        static private bool EnterNumberOfStrength(string numberOsStrength, string strengthCode)
        {
            return WebTools.FillInput(By.Name(strengthCode), numberOsStrength);
        }


        /*=======================================
         * start training in current village
         ========================================*/
        static private bool StartTraining()
        {
            return WebTools.ClickElem(By.Id("btn_train")) || WebTools.ClickElem(By.XPath("//button[@value=\"تربیت\"]"));
        }
    }
}
