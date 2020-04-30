using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEJKOOB_BOT
{

    /// <summary>
    /// Account related activities
    /// </summary>
    static public class Account
    {


        /*===============================
         * initialize account informations
         ================================*/
        static string Username;
        static string Password;
        static bool IsInitialize = false;
        static public void InitializeAccountInfo(string username, string password)
        {
            if (!IsInitialize)
            {
                Username = username;
                Password = password;
                IsInitialize = true;
            }
        }



        /*===============================
         * signin to user account by captcha
         ================================*/
        static public void Signin(string captcha)
        {
            WebTools.FillInput(By.Name("user"), Username);
            WebTools.FillInput(By.Name("pwtraa"), Password);
            WebTools.FillInput(By.Name("captcha"), captcha);
            WebTools.ClickElem(By.Id("s1"));
            RememberMe();
        }





        /*=====================================
         * function for save last login user
         ======================================*/
        static public void RememberMe()
        {
            Utilities.WriteInTxtFile("RememberMe.txt", $"{Username}:{Password}");
        }
    }
}
