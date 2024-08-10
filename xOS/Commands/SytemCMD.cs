using Cosmos.HAL;
using System;
using System.IO;
using System.Threading;
using xOS.FileSystem;
using xOS.UI;

namespace xOS.Commands
{
    public static class SytemCommand
    {
        private static readonly string s_DirFile = GlobalVariables.CurrentLocationFile;
        private static readonly string s_LoginFile = GlobalVariables.LoginFile;
        private static readonly string s_PartitionLetter = @"0:\";
        private static readonly string s_SysLogFile = GlobalVariables.SystemLogFile;
        private static string s_User = string.Empty;

        public static void SystemCommands(string inputData)
        {

            // Find dir and files by text
            if (inputData.StartsWith("find"))
            {
                if (inputData.Length == 4)
                {
                    UIColor.ErrorConsoleTextLine("You need to provide data for search!");
                    return;
                }
                inputData = inputData.Split(' ')[1];
                var currentPath = File.ReadAllText(s_DirFile);
                Find.FindFiles(inputData, currentPath);
            }


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
                s_User = GetConnectedUser(s_LoginFile);
                Console.Clear();
                Console.WriteLine($"-------------- Welcome to xOS, {s_User}. Enjoy your stay. -------------- ");
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
                    // Back to root of main partition.
                    // In this case 0:\
                    if(inputData.Trim() == "cd")
                    {
                        File.WriteAllText(s_DirFile, s_PartitionLetter);
                        return;
                    }

                    string dirPath = inputData.Split(' ')[1];
                    string dirPathSaved = File.ReadAllText(s_DirFile);

                    // Backward change directory
                    if (dirPath.Trim() == "..")
                    {
                        if (dirPathSaved == s_PartitionLetter)
                            return;
                        var readPathStoread = dirPathSaved.Split('\\');
                        var lenDirSeparator = readPathStoread.Length;
                        var backwardDir = "";
                        for (int i = 0; i < lenDirSeparator - 2; i++)
                            backwardDir += readPathStoread[i] + "\\";
                        
                        if (lenDirSeparator == 2)
                        {
                            File.WriteAllText(s_DirFile, s_PartitionLetter);
                            return;
                        }
                        File.WriteAllText(s_DirFile, backwardDir);
                        return;
                    }

                    if (string.IsNullOrEmpty(dirPathSaved))
                    {

                        if (Directory.Exists(dirPath))
                        {
                            if (dirPath.Contains(s_PartitionLetter))
                            {
                                File.WriteAllText(s_DirFile, dirPath + "\\");
                                return;
                            }
                            File.WriteAllText(s_DirFile, s_PartitionLetter + dirPath + "\\");
                            return;

                        }

                        Console.WriteLine($"Direcotry {dirPath} does not exist!");
                        return;
                    }

                    if (Directory.Exists(dirPathSaved + dirPath))
                    {
                        if (dirPath.Contains(s_PartitionLetter))
                        {
                            File.WriteAllText(s_DirFile, dirPath + "\\");
                            return;
                        }


                        File.WriteAllText(s_DirFile, dirPathSaved + dirPath + "\\");
                        return;
                    }

                    Console.WriteLine($"Direcotry {dirPath} does not exist!");
                }
                catch 
                {
                    // Ingnore.
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

        // Get current connected user.

        private static string GetConnectedUser(string loginFile)
        {
            string userName = File.ReadAllText(loginFile).Split('|')[1];
            return userName;
        }
    }
}