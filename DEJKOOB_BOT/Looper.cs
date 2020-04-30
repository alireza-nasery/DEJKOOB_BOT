using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using OpenQA.Selenium;

namespace DEJKOOB_BOT
{

    /// <summary>
    /// looper for tasks
    /// </summary>
    static public class Looper
    {

        /*=========================
         * looper for adventure task
         ==========================*/
        static public void StartLoopForAdventure(double time)
        {
            while (true)
            {
                try
                {
                    var now = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";
                    if (Adventure.StartAdventure())
                    {
                        TasksStorage.EditReport("ماجراجویی", "ماجراجویی فرستاده شد", now);
                    }
                    else
                    {
                        TasksStorage.EditReport("ماجراجویی", "شکست در ارسال قهرمان به ماجراجویی", now);
                    }
                }
                catch (Exception)
                {
                    WebTools.RefreshPage();
                }
                Thread.Sleep(TimeSpan.FromMinutes(time));
            }
        }



        /*==========================
         * looper for celebration tasks
         ===========================*/
        static public void StartLooperForCelebration(double time, DataGridView celebrationTasksDataGridView)
        {
            while (true)
            {
                try
                {

                    var number = celebrationTasksDataGridView.Rows.Count;
                    for (int i = 0; i < number - 1; i++)
                    {
                        var now = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";

                        var row = celebrationTasksDataGridView.Rows[i];
                        var villageName = row.Cells["CelebrationVillageNameColumn"].Value.ToString();
                        var celebrationKind = row.Cells["CelebrationKind"].Value.ToString();
                        var celebrationAdjustment = row.Cells["CelebrationAdjustmentColumn"].Value.ToString();
                        if (villageName == "تمامی دهات")
                        {
                            //exception--GetListOfVillageNames--be null
                            foreach (var name in Village.GetListOfVillageNames())
                            {
                                WebTools.RefreshPage();

                                var r = Celebration.StartCelebration(name, celebrationKind, celebrationAdjustment == "بلی" ? true : false) ?
                                    TasksStorage.EditReport("جشن", "جشن برگذار شد", now) : TasksStorage.EditReport("جشن", "برگزاری جشن با شکست مواجه شد", now);
                            }
                        }
                        else
                        {
                            var r = Celebration.StartCelebration(villageName, celebrationKind, celebrationAdjustment == "بلی" ? true : false) ?
                                    TasksStorage.EditReport("جشن", "جشن برگذار شد", now) : TasksStorage.EditReport("جشن", "برگزاری جشن با شکست مواجه شد", now);
                        }
                    }
                }
                catch (Exception)
                {
                    WebTools.RefreshPage();
                }
                Thread.Sleep(TimeSpan.FromMinutes(time));
            }
        }




        /*=====================================
         * looper for power builder
         ======================================*/
        static public void StartLooperForPowerBuilder(double time, DataGridView powerBuilderTasksDataGridView)
        {
            while (true)
            {
                try
                {
                    
                    var number = powerBuilderTasksDataGridView.Rows.Count;
                    for (int i = 0; i < number - 1; i++)
                    {
                        var now = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";

                        var row = powerBuilderTasksDataGridView.Rows[i];
                        var typeOfForce = row.Cells["TypeOfForceColumn"].Value.ToString();
                        var forceBuilding = row.Cells["ForceBuildingColumn"].Value.ToString();
                        var numberOfStaff = row.Cells["NumberOfStaffColumn"].Value.ToString();
                        var percentageAdjustment = row.Cells["PowerBuilderPercentageAdjustmentColumn"].Value.ToString();
                        var typeOfHat = row.Cells["HatTypeColumn"].Value.ToString();
                        var villageName = row.Cells["PowerBuilderVillageNameColumn"].Value.ToString();


                        if (villageName == "تمامی دهات")
                        {
                            //exception--GetListOfVillageNames--be null
                            foreach (var name in Village.GetListOfVillageNames())
                            {
                                WebTools.RefreshPage();

                                var description = string.Empty;
                                var r = PowerBuilder.StartBuildStrength(name, forceBuilding, numberOfStaff, typeOfForce, percentageAdjustment, typeOfHat) ?
                                    description = "تربیت نیرو با موفقیت انجام شد" : description = "تربیت نیرو با شکست مواجه شد";
                                TasksStorage.EditReport("نیروساز", description, now);
                            }
                        }
                        else
                        {
                            var description = string.Empty;
                            var r = PowerBuilder.StartBuildStrength(villageName, forceBuilding, numberOfStaff, typeOfForce, percentageAdjustment, typeOfHat) ?
                                description = "تربیت نیرو با موفقیت انجام شد" : description = "تربیت نیرو با شکست مواجه شد";
                            TasksStorage.EditReport("نیروساز", description, now);
                        }
                    }
                }
                catch (Exception e)
                {
                    WebTools.RefreshPage();
                }
                Thread.Sleep(TimeSpan.FromMinutes(time));
            }
        }



        /*===============================
         * start ironing for current tasks
         ================================*/
        static public void StartLooperForIroning(double time, DataGridView ironingTasksDataGridVIew)
        {
            while (true)
            {
                try
                {
                    //if page is lock
                    WebTools.RefreshPage();

                    var number = ironingTasksDataGridVIew.Rows.Count;
                    for (int i = 0; i < number - 1; i++)
                    {
                        var now = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";

                        var row = ironingTasksDataGridVIew.Rows[i];
                        var typeOfForce = row.Cells["IroningTypeOfForceColumn"].Value.ToString();
                        var villageName = row.Cells["IroningVillageNameColumn"].Value.ToString();
                        var level = row.Cells["IroningLevelColumn"].Value.ToString();
                        var armorOrWeapon = row.Cells["IroningArmorOrWeaponMakingColumn"].Value.ToString();


                        if (villageName == "تمامی دهات")
                        {
                            //exception--GetListOfVillageNames--be null
                            foreach (var name in Village.GetListOfVillageNames())
                            {
                                WebTools.RefreshPage();


                                var description = string.Empty;
                                var r = Ironing.StartIroning(typeOfForce, name, level, armorOrWeapon) ?
                                    description = "ارتقا انجام شد" : description = "ارتقا با شکست مواجه شد";
                                TasksStorage.EditReport("اهنگری", description, now);
                            }
                        }
                        else
                        {
                            var description = string.Empty;
                            var r = Ironing.StartIroning(typeOfForce, villageName, level, armorOrWeapon) ?
                                    description = "ارتقا انجام شد" : description = "ارتقا با شکست مواجه شد";
                            TasksStorage.EditReport("اهنگری", description, now);
                        }
                    }
                }
                catch (Exception e)
                {
                    WebTools.RefreshPage();
                }
                Thread.Sleep(TimeSpan.FromMinutes(time));
            }
        }




        /*=============================
         * start looper for finish
         ==============================*/
        static public void StartLooperForCompleteConstructionQuickly(double time, string villageName, bool isAllVillage)
        {
            while (true)
            {
                try
                {
                    WebTools.RefreshPage();
                    var now = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";


                    if (isAllVillage)
                    {
                        foreach (var name in Village.GetListOfVillageNames())
                        {
                            WebTools.RefreshPage();

                            var description = string.Empty;
                            var r = CompleteConstructionQuickly.StartFinishNow(name) ?
                                description = "اتمام سریع انجام شد" : description = "اتمام سریع با شکست مواجه شد";
                            TasksStorage.EditReport("اتمام سریع ساخت", description, now);
                        }
                    }
                    else
                    {
                        var description = string.Empty;
                        var r = CompleteConstructionQuickly.StartFinishNow(villageName) ?
                            description = "اتمام سریع انجام شد" : description = "اتمام سریع با شکست مواجه شد";
                        TasksStorage.EditReport("اتمام سریع ساخت", description, now);
                    }
                }
                catch (Exception e)
                {
                    WebTools.RefreshPage();
                }

                Thread.Sleep(TimeSpan.FromMinutes(time));
            }
        }





        /*===============================
         * start looper for buildings
         ================================*/
        static public void StartLooperForBuildings(double time, DataGridView buildingsDataGridView)
        {
            while (true)
            {
                try
                {
                    WebTools.RefreshPage();

                    var number = buildingsDataGridView.Rows.Count;
                    for (int i = 0; i < number - 1; i++)
                    {


                        var row = buildingsDataGridView.Rows[i];

                        var villageName = row.Cells["BuildingsVillageNameColumn"].Value.ToString();
                        var level = row.Cells["BuildingsLevelColumn"].Value.ToString();
                        var buildingType = row.Cells["BuildingsBuildTypeColumn"].Value.ToString();

                        if (level == "اخرین سطح")
                            level = "20";

                        if (villageName == "تمامی دهات")
                        {
                            foreach (var village_name in Village.GetListOfVillageNames())
                            {
                                if (buildingType == "همه ساختمان ها")
                                {
                                    foreach (var building_name in Village.GetAllBuildingsNameInVilageByVillageName(village_name))
                                    {
                                        UpgradeBuilding(building_name, village_name, level);
                                    }
                                }
                                else
                                {
                                    UpgradeBuilding(buildingType, village_name, level);
                                }
                            }
                        }
                        else
                        {
                            if (buildingType == "همه ساختمان ها")
                            {
                                foreach (var building_name in Village.GetAllBuildingsNameInVilageByVillageName(villageName))
                                {
                                    UpgradeBuilding(building_name, villageName, level);
                                }
                            }
                            else
                            {
                                UpgradeBuilding(buildingType, villageName, level);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    WebTools.RefreshPage();
                }
                Thread.Sleep(TimeSpan.FromMinutes(time));
            }
        }
        static private void UpgradeBuilding(string buildingType, string villageName, string level)
        {
            WebTools.RefreshPage();


            var now = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";

            var description = string.Empty;
            var r = Buildings.StartUpgradeBuildings(buildingType, villageName, level) ?
                description = "ساختمان ارتقا یافت" : description = "ارتقا ساختمان با شکست روبرو شد";
            TasksStorage.EditReport("ساختمان ها", description, now);
        }




        /*====================================
         * start looper for resources
         =====================================*/
        static public void StartLooperForResources(double time, DataGridView resourcesTasksDataGridView)
        {
            while (true)
            {
                try
                {
                    WebTools.RefreshPage();

                    var number = resourcesTasksDataGridView.Rows.Count;
                    for (int i = 0; i < number - 1; i++)
                    {

                        var row = resourcesTasksDataGridView.Rows[i];

                        var villageName = row.Cells["ResourcesVillageNameColumn"].Value.ToString();
                        var level = row.Cells["ResourcesLevelColumn"].Value.ToString();

                        if (level == "اخرین سطح")
                            level = "20";

                        if (villageName == "تمامی دهات")
                        {
                            foreach (var village_name in Village.GetListOfVillageNames())
                            {
                                UpgradeResources(village_name, level);
                            }
                        }
                        else
                        {
                            UpgradeResources(villageName, level);
                        }
                    }
                }
                catch (Exception e)
                {
                    WebTools.RefreshPage();
                }
                Thread.Sleep(TimeSpan.FromMinutes(time));
            }
        }
        static private void UpgradeResources(string villageName, string level)
        {
            WebTools.RefreshPage();
            var xpath = "//area[contains(@href,\"build\")]";
            var description = string.Empty;

            if (Village.OpenVillage(villageName) && Village.OpenVillageResources())
            {
                var numbers = Village.GetCurrentVillageResourcesNumber();
                for (int i = 0; i < numbers; i++)
                {
                    Village.OpenVillageResources();
                    var now = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";
                    var map = WebTools.CallElement(By.Id("rx"));
                    try
                    {
                        var area = map.FindElements(By.XPath(xpath))[i];
                        area.Click();
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                    var r = CheckLevelAndUpgradeRespurces(level) ?
                        description = "منابع ارتقا یافت" : description = "ارتقا منابع با شکست مواجه شد";
                    TasksStorage.EditReport("منابع", description, now);
                }
            }
        }
        static private bool CheckLevelAndUpgradeRespurces(string level)
        {
            try
            {
                var currentLevel = WebTools.CallElement(By.ClassName("level")).Text.Replace("سطح ", "");
                if (Int32.Parse(currentLevel) < Int32.Parse(level))
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
                return false;
            }
        }






    }
}
