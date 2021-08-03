using Cosmos.HAL;
using System;
using System.IO;
using System.Threading;
using xOS.FileSystem;

namespace xOS.Commands
{
    public static class SytemCommand
    {
        private static readonly string s_DirFile = GlobalVariables.CurrentLocationFile;
        private static readonly string s_LoginFile = GlobalVariables.LoginFile;
        private static readonly string s_PartitionLetter = @"0:\";
        private static readonly string s_SysLogFile = GlobalVariables.SystemLogFile;

        public static void SystemCommands(string inputData)
        {

            //list file and folders
            if (inputData.StartsWith("ls"))
            {
                if (inputData.Length == 2)
                {
                    Root.ListCommand();
                    return;
                }

                inputData = inputData.Split(' ')[1];
                Root.ListCommand(inputData);

            }

            //clear console
            if (inputData == "clear")
            {
                Console.Clear();
                Console.WriteLine("--------------------Welcome to xOS----------------------");
            }

            //shutdown command... BETA
            if (inputData == "shutdown")
            {
                Console.WriteLine("xOS is shuting down!");
                CLog.LogSystem.SystemLogAudit(s_SysLogFile, "xOS is shuting down!");
                Thread.Sleep(1500);
                Cosmos.System.Power.Shutdown();
            }

            //system reboot command.. BETA
            if (inputData == "reboot")
            {
                Console.WriteLine("xOS is restarting!");
                CLog.LogSystem.SystemLogAudit(s_SysLogFile, "xOS is restarting!");
                Thread.Sleep(1500);
                Cosmos.System.Power.Reboot();
            }

            //current directory command
            if (inputData.StartsWith("cd"))
            {
                try
                {
                    string DirPath = inputData.Split(' ')[1];
                    string DirPathSaved = File.ReadAllText(s_DirFile);

                    if (string.IsNullOrEmpty(DirPathSaved))
                    {

                        if (Directory.Exists(DirPath))
                        {
                            if (DirPath.Contains(s_PartitionLetter))
                            {
                                File.WriteAllText(s_DirFile, DirPath);
                                return;
                            }
                            File.WriteAllText(s_DirFile, s_PartitionLetter + DirPath);
                            return;

                        }

                        Console.WriteLine($"Direcotry {DirPath} does not exist!");
                        return;
                    }

                    if (Directory.Exists(DirPathSaved + @"\" + DirPath))
                    {
                        if (DirPath.Contains(s_PartitionLetter))
                        {
                            File.WriteAllText(s_DirFile, DirPath);
                            return;
                        }


                        File.WriteAllText(s_DirFile, DirPathSaved + @"\" + DirPath);
                        return;
                    }

                    Console.WriteLine($"Direcotry {DirPath} does not exist!");
                }
                catch
                {
                    File.WriteAllText(s_DirFile, string.Empty);
                }
            }

            //logout the current users command
            if (inputData.StartsWith("logout"))
            {
                File.WriteAllText(s_LoginFile, "0");
                Console.Clear();
            }

            //shout current time command
            if (inputData == "time")
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