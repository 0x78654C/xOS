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

       
        protected override void BeforeRun()
        {
            Console.WriteLine("xOS booted successfully. Type a line of text to get it echoed back.");
            Root.Create_Root();
            Root.Initialize_Sys_Dirs();
            CLog.CLog.SysLog_LoadOS();
            Console.Clear();
            Console.WriteLine("--------------------Welcome to xOS----------------------");
        }

        protected override void Run()
        {
            Console.Write("$ ");
            var input = Console.ReadLine();

            //run System Commands
            Commnads.SytemCMD.RunSysCMD(input);

            //run Files management commands
            Commnads.FileCMD.RunFileCMD(input);

            //run Directory management commands
            Commnads.DirectoryCMD.RunDirCMD(input);
        }
    }
}
