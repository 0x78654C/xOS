using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Cosmos.HAL;

namespace xOS.CLog
{
    public class CLog
    {
        private static string SysLogFile= FileSystem.GVariables.SYSLogFile;

        /// <summary>
        /// System Log store
        /// </summary>
        /// <param name="logData"></param>
        public static void SysLog_LoadOS(string logData)
        {
            if (File.Exists(SysLogFile))
            {
                string d = RTC.DayOfTheMonth.ToString();
                string y = RTC.Year.ToString();
                string mo = RTC.Month.ToString();
                string h = RTC.Hour.ToString();
                string m = RTC.Minute.ToString();
                string s = RTC.Second.ToString();
                File.AppendAllText(SysLogFile, $"[{d}-{mo}-{y} {h}:{m}:{s}] {logData} \n");
            }
            else
            {
                Console.WriteLine(SysLogFile + " Does not exits!");
            }
        }
    }
}
