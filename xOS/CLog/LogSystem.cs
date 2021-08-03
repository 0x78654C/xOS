using Cosmos.HAL;
using System;
using System.IO;

namespace xOS.CLog
{
    public class LogSystem
    {

        /// <summary>
        /// System Log store
        /// </summary>
        /// <param name="logData"></param>
        public static void SystemLogAudit(string logFile,string logData)
        {
            
            if (File.Exists(logFile))
            {
                string day = RTC.DayOfTheMonth.ToString();
                string year = RTC.Year.ToString();
                string month = RTC.Month.ToString();
                string hours = RTC.Hour.ToString();
                string minutes = RTC.Minute.ToString();
                string seconds = RTC.Second.ToString();
                File.AppendAllText(logFile, $"[{day}-{month}-{year} {hours}:{minutes}:{seconds}] {logData} \n") ;
            }
            else
            {
                Console.WriteLine(logFile + " Does not exits!");
            }
        }
    }
}
