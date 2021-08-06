using System;
using System.IO;
using xOS.FileSystem;
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
        private static readonly string s_LoginFile = GlobalVariables.LoginFile;
        private static readonly string s_SysLogFile = GlobalVariables.SystemLogFile;
        /// <summary>
        /// Before run the main shell
        /// </summary>
        protected override void BeforeRun()
        {
            Root.CreateRoot();                              //loading partitions and file system
            Root.InitializeSystemStructure();                      //initialiaze the system structure creation 
            CLog.LogSystem.SystemLogAudit(s_SysLogFile, "System loaded");        //storing information in log when system is succesfully started - includes datetime
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
                    Console.WriteLine($"-------------- Welcome to xOS, {s_User}. Enjoy your stay. -------------- ");
                }
            }
            else
            {
                s_CurrentLocation = File.ReadAllText(s_CurrentLocationFile);
                string consoleUser = !string.IsNullOrEmpty(s_CurrentLocation) ? $"{s_User} ({s_CurrentLocation})$ " : $"{s_User} $ ";
                Console.Write(consoleUser);
                
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
