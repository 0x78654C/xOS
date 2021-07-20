using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using FlappyBirds;
using System.IO;
using System.Threading;
using xOS.FileSystem;


namespace xOS
{
    public class Kernel : Sys.Kernel
    {

        public static string LoggedStatus = "0";
        private static string uLogin = string.Empty;
        private static string User = string.Empty;
        private static string cDir = string.Empty;
        private static string cDirFile = GVariables.cDirFile;
        private static string LoginFile = GVariables.LoginFile;
        /// <summary>
        /// Before run the main shell
        /// </summary>
        protected override void BeforeRun()
        {
            Root.Create_Root();                              //loading partitions and file system
            Root.Initialize_Sys_Dirs();                      //initialiaze the system structure creation 
            CLog.CLog.SysLog_LoadOS("System loaded");        //storing information in log when system is succesfully started - includes datetime
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
            LoggedStatus = System.IO.File.ReadAllText(LoginFile);
            if (LoggedStatus == "0")
            {
                uLogin = Users.UserLogin();
                if (uLogin.Contains("logged"))
                {
                    User = uLogin.Split('|')[1];
                    System.IO.File.WriteAllText(LoginFile, "1");
                    Console.Clear();
                    Console.WriteLine($"-------------- Welcome to xOS, {User}. Enjoy your stay. -------------- ");
                }
            }
            else
            {
                cDir = System.IO.File.ReadAllText(cDirFile);
                if (!string.IsNullOrEmpty(cDir))
                {
                    Console.Write($"{User} ({cDir})$ ");
                }
                else
                {
                    Console.Write($"{User} $ ");
                }
                var input = Console.ReadLine();

                //run System Commands
                Commnads.SytemCMD.RunSysCMD(input);

                //run Files management commands
                Commnads.FileCMD.RunFileCMD(input);

                //run Directory management commands
                Commnads.DirectoryCMD.RunDirCMD(input);

                //run User Managemenet commands
                Commnads.UsrCMD.RunUserCMD(input);

                //run Help command
                Commnads.HelpCMD.RunHelpCMD(input);

           }
        }
    }
}
