using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEJKOOB_BOT
{

    /// <summary>
    /// simple storage for tasks reporter
    /// </summary>
    static public class TasksStorage
    {


        static private string[] tasks = { "ماجراجویی", "جشن", "اکانت", "نیروساز", "اهنگری", "اتمام سریع ساخت", "ساختمان ها", "منابع" };
        static public List<(string taskName, string description, string time)> TasksReport = new List<(string taskName, string description, string time)>();
        static bool IsInitialized = false;
        /*==============================
         * initialize tasks in list
         ===============================*/
        static public void InitializeTasks()
        {
            if (!IsInitialized)
            {
                for (int i = 0; i < tasks.Length; i++)
                {
                    AddNewTaskInTasksReport(tasks[i], "", "00:00");
                }
                IsInitialized = true;
            }
        }






        /*====================================
         * function for edit tasks in list
         ====================================*/
        static public int EditReport(string taskName, string newDescription, string newTime)
        {
            TasksReport.Remove(TasksReport.Single(r => r.taskName == taskName));
            AddNewTaskInTasksReport(taskName, newDescription, newTime);
            return 0;
        }






        /*====================================
         * add new tasks in report list
         ====================================*/
        static private void AddNewTaskInTasksReport(string taskName, string description, string time)
        {
            TasksReport.Add((taskName, description, time));
        }



    }
}
