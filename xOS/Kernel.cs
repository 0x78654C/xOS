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

        private static int LoggedStatus = 0;
        private static string uLogin = string.Empty;
        private static string User = string.Empty;
        protected override void BeforeRun()
        {
            Console.WriteLine("xOS booted successfully. Type a line of text to get it echoed back.");
            Root.Create_Root();
            Root.Initialize_Sys_Dirs();
            CLog.CLog.SysLog_LoadOS("System loaded");
            Console.Clear();
        }

        protected override void Run()
        {

            if (LoggedStatus == 0)
            {
                uLogin = Root.UserLogin();
                if (uLogin.Contains("logged"))
                {
                    User = uLogin.Split('|')[1];
                    LoggedStatus = 1;
                    Console.WriteLine("--------------------Welcome to xOS----------------------");
                    Console.WriteLine($"Welcome {User} ");
                }
            }
            else
            {

                Console.Write($"{User} $ ");
                var input = Console.ReadLine();

                //run System Commands
                Commnads.SytemCMD.RunSysCMD(input);

                //run Files management commands
                Commnads.FileCMD.RunFileCMD(input);

                //run Directory management commands
                Commnads.DirectoryCMD.RunDirCMD(input);

                //run User Managemenet commands
                Commnads.UsrCMD.RunUserCMD(input);
            }
        }
    }
}
