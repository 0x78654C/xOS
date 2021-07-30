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
                string d = RTC.DayOfTheMonth.ToString();
                string y = RTC.Year.ToString();
                string mo = RTC.Month.ToString();
                string h = RTC.Hour.ToString();
                string m = RTC.Minute.ToString();
               // string s = RTC.Second.ToString(); //just for test is disabled
                File.AppendAllText(logFile, $"[{d}-{mo}-{y} {h}:{m}] {logData} \n") ;
            }
            else
            {
                Console.WriteLine(logFile + " Does not exits!");
            }
        }
    }
}
