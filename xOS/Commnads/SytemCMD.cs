using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using xOS.FileSystem;
using Cosmos.HAL;

namespace xOS.Commnads
{
    public static class SytemCMD
    {
        private static string cDirFile = GVariables.cDirFile;
        private static string LoginFile = GVariables.LoginFile;
        private static string dLetter = @"0:\";

        public static void RunSysCMD(string input)
        {

            //list file and folders
            if (input.StartsWith("ls"))
            {
                if (input.Length == 2)
                {
                    Root.Test_Root();
                }
                else
                {
                    input = input.Split(' ')[1];
                    Root.Test_Root(input);
                }
            }

            //clear console
            if (input == "clear")
            {
                Console.Clear();
                Console.WriteLine("--------------------Welcome to xOS----------------------");
            }

            //shutdown command... BETA
            if(input == "shutdown")
            {
                Console.WriteLine("xOS is shuting down!");
                CLog.CLog.SysLog_LoadOS("xOS is shuting down!");
                Thread.Sleep(1500);
                Power.ACPIShutdown();
            } 

            //system reboot command.. BETA
            if (input == "reboot")
            {
                Console.WriteLine("xOS is restarting!");
                CLog.CLog.SysLog_LoadOS("xOS is restarting!");
                Thread.Sleep(1500);
                Power.ACPIReboot();
            }

            //current directory command
            if (input.StartsWith("cd"))
            {
                try
                {
                    string DirPath = input.Split(' ')[1];
                    string DirPathSaved = File.ReadAllText(cDirFile);

                    if (string.IsNullOrEmpty(DirPathSaved))
                    {

                        if (System.IO.Directory.Exists(DirPath))
                        {
                            if (DirPath.Contains(dLetter))
                            {
                                File.WriteAllText(cDirFile, DirPath);
                            }
                            else
                            {
                                File.WriteAllText(cDirFile,dLetter + DirPath);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Direcotry {DirPath} dose not exist!");
                        }
                    }
                    else
                    {
                        if (System.IO.Directory.Exists(DirPathSaved + @"\" + DirPath))
                        {
                            if (DirPath.Contains(dLetter))
                            {
                                File.WriteAllText(cDirFile, DirPath);
                            }
                            else
                            {
                                File.WriteAllText(cDirFile, DirPathSaved +@"\"+ DirPath);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Direcotry {DirPath} dose not exist!");
                        }
                    }
                }
                catch 
                {
                    File.WriteAllText(cDirFile, string.Empty);
                }
            }

            //logout the current users command
            if (input.StartsWith("logout"))
            {
                File.WriteAllText(LoginFile, "0");
                Console.Clear();
            }

            //shout current time command
            if (input == "time")
            {
                string d = RTC.DayOfTheMonth.ToString();
                string y = RTC.Year.ToString();
                string mo = RTC.Month.ToString();
                string h = RTC.Hour.ToString();
                string m = RTC.Minute.ToString();
                string s = RTC.Second.ToString();
                Console.WriteLine($"Date: {d}-{mo}-{y} / Time: {h}:{m}:{s}");
            }
        }
    }
}