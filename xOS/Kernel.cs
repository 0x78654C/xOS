using System;
using System.IO;
using xOS.FileSystem;
using xOS.UI;
using Sys = Cosmos.System;


namespace xOS
{
    public class Kernel : Sys.Kernel
    {

        private static string s_LoggedStatus = "0";
        private static string s_UserLogin = string.Empty;
        private static string s_User = string.Empty;
        private static string s_CurrentLocation = string.Empty;
        private static readonly string s_CurrentLocationFile = GlobalVariables.CurrentLocationFile;
        private static readonly string s_UserFile = GlobalVariables.UsersFile;
        private static readonly string s_LoginFile = GlobalVariables.LoginFile;
        private static readonly string s_SysLogFile = GlobalVariables.SystemLogFile;
        /// <summary>
        /// Before run the main shell
        /// </summary>
        protected override void BeforeRun()
        {
            Root.CreateRoot();                                                   // Loading partitions and file system
            Root.InitializeSystemStructure();                                    // Initialiaze the system structure creation 
            CLog.LogSystem.SystemLogAudit(s_SysLogFile, "System loaded");        // Storing information in log when system is succesfully started - includes datetime
            Console.Clear();
            Console.WriteLine("=================================================================");
            Console.WriteLine("=====================WELCOME TO xOS SYSTEM=======================");
            Console.WriteLine("=================================================================\n");
        }

        /// <summary>
        /// Main shell
        /// </summary>
        protected override void Run()
        {
            // logins system initialize
            s_LoggedStatus = File.ReadAllText(s_LoginFile);
            s_LoggedStatus = s_LoggedStatus.Split('|')[0];
            if (s_LoggedStatus == "0")
            {
                s_UserLogin = UsersManagement.UserLogin();
                if (s_UserLogin.Contains("logged"))
                {
                    s_User = s_UserLogin.Split('|')[1];
                    File.WriteAllText(s_LoginFile, $"1|{s_User}");
                    Console.Clear();
                    string userAdmin = Commands.UsrCMD.GetUserType(s_UserFile, s_LoginFile);
                    if (userAdmin == "a")
                        Console.WriteLine($"-------------- Welcome to xOS, {s_User} (administrator). Enjoy your stay. -------------- ");
                    else
                        Console.WriteLine($"-------------- Welcome to xOS, {s_User}. Enjoy your stay. -------------- ");
                }
            }
            else
            {
                s_CurrentLocation = File.ReadAllText(s_CurrentLocationFile);
                string consoleUser = !string.IsNullOrEmpty(s_CurrentLocation) ? $"{s_User} ({s_CurrentLocation})" : $"{s_User}";
                UIColor.ColorConsoleText(ConsoleColor.Green, consoleUser);
                UIColor.ColorConsoleText(ConsoleColor.Cyan, " $ ");

                // Running commands.
                var input = Console.ReadLine();
                RunCommands(input);
            }
        }


        /// <summary>
        /// Initialize xOS Commands
        /// </summary>
        /// <param name="inputData"></param>
        private static void RunCommands(string inputData)
        {
            //run System Commands
            Commands.SytemCommand.SystemCommands(inputData);

            //run Files management commands
            Commands.FileCMD.FileCommands(inputData);

            //run Directory management commands
            Commands.DirectoryCommand.DirectoryCommands(inputData);

            //run User Managemenet commands
            Commands.UsrCMD.UserCommands(inputData);

            //run Help command
            Commands.HelpCMD.HelpCommands(inputData);
        }
    }
}
