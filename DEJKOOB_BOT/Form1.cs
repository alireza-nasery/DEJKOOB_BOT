using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using OpenQA.Selenium;

namespace DEJKOOB_BOT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TasksStorage.InitializeTasks();
        }








        /*==============================
         * load captcha image in login tab
         ===============================*/
        private void LoadCaptcha()
        {
            var js = Utilities.ReadFromeFile("captcha_image.js");
            var base64 = WebTools.ExecuteJavascriptOnPage(js);
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64)))
            {
                CaptchaPictureBox.Image = Image.FromStream(ms);
            }
        }








        /*================================
         * get account information from login
         * tab and login user by Account class
         =================================*/
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordTextBox.Text;
            var captcha = CaptchaTextBox.Text;
            Account.InitializeAccountInfo(username, password);
            Account.Signin(captcha);
        }









        /*====================================
         * navigate browser to specific login page
         =====================================*/
        private void LoginAddressBtnBtn_Click(object sender, EventArgs e)
        {
            Func<bool> local = () =>
            {
                var url = AddressLoginTextBox.Text;
                //var url = /*"http://aj50.dejkoob.net/build.php?id=29"*/"http://e.s.dejkoob.net/build.php?id=22"/*AddressLoginTextBox.Text*/;
                WebTools.Initialize(OpenBrowser.Checked);
                WebTools.NavigateToPage(url);
                LoadCaptcha();
                Thread.CurrentThread.Abort();
                return true;
            };
            new Thread(new ThreadStart(() => local())).Start();
            new Thread(new ThreadStart(StartTasksReporter)).Start();

        }









        /*=====================================
         * adventure btn for start adventure
         ======================================*/
        private void AdventureBtn_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(() => Looper.StartLoopForAdventure((int)AdventureTimeNumericUpDown.Value))).Start();
            StateOfCelebrate.Visible = true;
            AdventureBtn.Enabled = false;
        }



















        /*================================
         * on load event for form1
         =================================*/
        private void loadingForm(object sender, EventArgs e)
        {


            var combo = Utilities.ReadFromeFile("RememberMe.txt");
            var username = combo.Split(':')[0];
            var password = combo.Split(':')[1];
            UsernameTextBox.Text = username;
            PasswordTextBox.Text = password;
        }








        /*===============================
         * this function report seguation
         * of all tasks in report tab
         ================================*/
        private void StartTasksReporter()
        {
            while (true)
            {

                for (int i = 0; i < TasksStorage.TasksReport.Count(); i++)
                {
                    DataGridViewRow row = (DataGridViewRow)TasksReportGridView.Rows[i].Clone();
                    row.Cells[0].Value = TasksStorage.TasksReport[i].taskName;
                    row.Cells[1].Value = TasksStorage.TasksReport[i].description;
                    row.Cells[2].Value = TasksStorage.TasksReport[i].time;
                    TasksReportGridView.Invoke((MethodInvoker)delegate
                    {
                        TasksReportGridView.Rows.Add(row);
                    });
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));

                try
                {
                    TasksReportGridView.Invoke((MethodInvoker)delegate
                    {
                        TasksReportGridView.Rows.Clear();
                    });
                }
                catch (Exception)
                {
                    Thread.CurrentThread.Abort();
                }

                //check MomentaryChanges
                MomentaryChanges();
            }
        }
        private void MomentaryChanges()
        {
            //is user login
            var now = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";
            var elem = WebTools.CallElement(By.XPath("//a[@href=\"logout.php\"]"));
            if (elem != null)
            {
                TasksStorage.EditReport("اکانت", "داخل پنل کاربری", now);
            }
            else
            {
                TasksStorage.EditReport("اکانت", "خارج از پنل کاربری", now);
            }


            ////is antibot active
            var source = WebTools.GetPageSource();
            if (source.Contains("تشخیص انسان از ربات"))
            {
                Thread.Sleep(TimeSpan.FromMinutes(3));
                source = WebTools.GetPageSource();
                if (source.Contains("تشخیص انسان از ربات"))
                {
                    try
                    {
                        WebTools.ClickElem(By.XPath("//a[@href=\"logout.php\"][@title=\"خروج||خروج از بازی\"]"));
                    }
                    catch (Exception)
                    {
                    }
                }

            }

        }





        /*================================
         * add new celebration task in celebration 
         * tasks list
         =================================*/
        private void AddNewCelebrationTaskBtn_Click(object sender, EventArgs e)
        {
            var villageName = CelebrationVillageNameTextBox.Text;
            var AllVillage = AllVillageCheckBox.Checked;
            var celebrationKind = CelebrationKindComboBox.Text;
            var celebrationAdjustment = CelebrationAdjustmentCheckBox.Checked;

            if (!AllVillage)
            {
                CelebrationTasksDataGridView.Rows.Add(villageName, celebrationKind, celebrationAdjustment ? "بلی" : "خیر");
            }
            else
            {
                CelebrationTasksDataGridView.Rows.Add("تمامی دهات", celebrationKind, celebrationAdjustment ? "بلی" : "خیر");
            }
        }







        /*======================================
         * start celebrations tasks list
         =======================================*/
        private void StartCelebrationsTasksBtn_Click(object sender, EventArgs e)
        {
            var time = (double)CelebrationTasksTimeNumericUpDown.Value;
            new Thread(new ThreadStart(() => Looper.StartLooperForCelebration(time, CelebrationTasksDataGridView))).Start();
            StateOfCelebrationLabel.Visible = true;
            StartCelebrationsTasksBtn.Enabled = false;
        }








        /*=========================================
         * add new task to power builder tasks list
         ==========================================*/
        private void AddNewPowerBuilderTaskBtn_Click(object sender, EventArgs e)
        {
            var villageName = PowerBuilderVillageNameTextBox.Text;
            var AllVillage = PowerBuilderAllVillageCheckBox.Checked;
            var typeOfForce = TypeOfForceComboBox.Text;
            var forceBuilding = ForceBuildingComboBox.Text;
            var numberOfForce = NumberOfStaffTextBox.Text;
            var percentageAdjustment = PercentageAdjustmentTextBox.Text;
            var typeOfHat = HatTypeComboBox.Text;

            if (!AllVillage)
            {
                PowerBuilderTasksDataGridView.Rows.Add(typeOfForce, forceBuilding, typeOfHat, percentageAdjustment, villageName, numberOfForce);
            }
            else
            {
                PowerBuilderTasksDataGridView.Rows.Add(typeOfForce, forceBuilding, typeOfHat, percentageAdjustment, "تمامی دهات", numberOfForce);
            }

        }


        /*====================================
         * event for start power building button
         =====================================*/
        private void PowerBuilderStartTasksBtn_Click(object sender, EventArgs e)
        {
            var time = (double)PowerBuilderNumericUpDown.Value;
            new Thread(new ThreadStart(() => Looper.StartLooperForPowerBuilder(time, PowerBuilderTasksDataGridView))).Start();
            PowerBuilderStartTasksBtn.Enabled = false;
            StateOfPowerBuilderLabel.Visible = true;
        }



        /*===================================
         * add new tasks in ironing tab
         ====================================*/
        private void IroningAddNewTaskBtn_Click(object sender, EventArgs e)
        {
            var villageName = IroningVillageNameTextBox.Text;
            var AllVillage = IroningAllVillageCheckBox.Checked;
            var typeOfForce = IroningTypeOfForceComboBox.Text;
            var level = IroningLevelTextBox.Text;
            var armorOrWeapon = IroningArmorOrWeaponMakingComboBox.Text;


            if (!AllVillage)
            {
                IroningTasksDataGridVIew.Rows.Add(typeOfForce, villageName, level, armorOrWeapon);
            }
            else
            {
                IroningTasksDataGridVIew.Rows.Add(typeOfForce, "تمامی دهات", level, armorOrWeapon);
            }

        }



        /*===================================
         * start play tasks for ironing
         ====================================*/
        private void IroningStartTasksBtn_Click(object sender, EventArgs e)
        {
            var time = (double)IroningTimeNumericUpDown.Value;
            new Thread(new ThreadStart(() => Looper.StartLooperForIroning(time, IroningTasksDataGridVIew))).Start();
            IroningStartTasksBtn.Enabled = false;
            StateOfIroning.Visible = true;
        }




        /*===================================
         * event for click complete construction 
         * quickly
         ====================================*/
        private void CompleteConstructionQuicklyStartBtn_Click(object sender, EventArgs e)
        {
            var time = (double)CompleteConstructionQuicklyTimeNumericUpDown.Value;
            var allVillage = CompleteConstructionQuicklyAllVillageCheckBox.Checked;
            var villageName = CompleteConstructionQuicklyVillageNameTextBox.Text;
            new Thread(new ThreadStart(() => Looper.StartLooperForCompleteConstructionQuickly(time, villageName, allVillage))).Start();
            CompleteConstructionQuicklyStartBtn.Enabled = false;
            StateOfCompleteConstructionQuicklyLabel.Visible = true;
        }






        /*========================================
         * add enw tasks in buildings tasks list
         =========================================*/
        private void BuildingsAddNewTasksBtn_Click(object sender, EventArgs e)
        {
            var villageName = BuildingsVillageNameTextBox.Text;
            var AllVillage = BuildingAllVillageCheckBox.Checked;
            var commercial = BuildingsCommercialComboBox.Text.Replace("تجاری", "");
            var military = BuildingsMilitaryComboBox.Text.Replace("نظامی", "");
            var others = BuildingsOthersComboBox.Text.Replace("سایر", "");
            var allBuildings = BuildingsAllBuildingsCheckBox.Checked;
            var level = BuildingsLevelTextBox.Text;
            var lastLevel = BuildingsLastLevelCheckBox.Checked;

            var buildingType = commercial + military + others;

            if (AllVillage)
            {
                villageName = "تمامی دهات";
                if (allBuildings)
                {
                    buildingType = "همه ساختمان ها";
                }
                if (lastLevel)
                {
                    level = "اخرین سطح";
                }
            }
            else
            {
                if (allBuildings)
                {
                    buildingType = "همه ساختمان ها";
                }
                if (lastLevel)
                {
                    level = "اخرین سطح";
                }
            }
            BuildingsTasksDataGridView.Rows.Add(buildingType, villageName, level);


        }





        /*=======================================
         * event for start tasks for buildings
         ========================================*/
        private void BuildingsStartTasksBtn_Click(object sender, EventArgs e)
        {
            var time = (double)BuildingsTimeNumericUpDown.Value;
            new Thread(new ThreadStart(() => Looper.StartLooperForBuildings(time, BuildingsTasksDataGridView))).Start();
            BuildingsStartTasksBtn.Enabled = false;
            StateOfBuildingsLabel.Visible = true;
        }




        /*========================================
         * add new tasks to resources tasks list
         =========================================*/
        private void ResourcesAddNewTasksBtn_Click(object sender, EventArgs e)
        {
            var villageName = ResourcesVillageNameTextBox.Text;
            var allVillage = ResourcesAllVillageCheckBox.Checked;
            var level = ResourcesLevelTextBox.Text;
            var lastLevel = ResourcesLastLevelCheckBox.Checked;

            if (allVillage)
            {
                villageName = "تمامی دهات";
                if (lastLevel)
                {
                    level = "20";
                }
            }
            else
            {
                if (lastLevel)
                {
                    level = "20";
                }
            }
            ResourcesTasksDAtaGridView.Rows.Add(villageName, level);
        }

        private void ResourcesStartTasksListBtn_Click(object sender, EventArgs e)
        {
            var time = (double)ResourcesTimeNumericUpDown.Value;
            new Thread(new ThreadStart(() => Looper.StartLooperForResources(time, ResourcesTasksDAtaGridView))).Start();
            ResourcesStartTasksListBtn.Enabled = false;
            StateOfResourcesLabel.Visible = true;
        }

        private void _closing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();

            Environment.Exit(0);
        }


    }
}
