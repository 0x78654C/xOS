using System;
using System.IO;
using xOS.FileSystem;
using Sys = Cosmos.System;


namespace xOS
{
    public class Kernel : Sys.Kernel
    {

        public static string loggedStatus = "0";
        private static string s_userLogin = string.Empty;
        private static string s_user = string.Empty;
        private static string s_currentLocation = string.Empty;
        private static readonly string s_currentLocationFile = GlobalVariables.CurrentLocationFile;
        private static readonly string s_loginFile = GlobalVariables.LoginFile;
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
            loggedStatus = File.ReadAllText(s_loginFile);
            loggedStatus = loggedStatus.Split('|')[0];
            if (loggedStatus == "0")
            {
                s_userLogin = UsersManagement.UserLogin();
                if (s_userLogin.Contains("logged"))
                {
                    s_user = s_userLogin.Split('|')[1];
                    File.WriteAllText(s_loginFile, $"1|{s_user}");
                    Console.Clear();
                    Console.WriteLine($"-------------- Welcome to xOS, {s_user}. Enjoy your stay. -------------- ");
                }
                return;
            }
            s_currentLocation = File.ReadAllText(s_currentLocationFile);
            if (!string.IsNullOrEmpty(s_currentLocation))
            {
                Console.Write($"{s_user} ({s_currentLocation})$ ");
                return;
            }
            Console.Write($"{s_user} $ ");
            var input = Console.ReadLine();
            //--------------------------------------

            RunCommands(input);
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
