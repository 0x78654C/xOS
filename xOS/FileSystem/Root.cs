using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Cosmos.HAL;
using Cosmos.Common;
using Sys = Cosmos.System;
namespace xOS.FileSystem
{
    public static class Root
    {
        //declare global variables
        private static string SysVol = GVariables.SysVol;
        private static string SysDir = GVariables.SysDir;
        private static string UsrDir = GVariables.UsrDir;
        private static string LogDir = GVariables.LogDir;
        private static string TmpDir = GVariables.TmpDir;
        private static string cDirFile = GVariables.cDirFile;
        private static string LoginFile = GVariables.LoginFile;
        private static string SYSLogFile = GVariables.SYSLogFile;
        private static string UsrFile = GVariables.UsrFile;
        //--------------------------

        /*Creating root file system*/
        public static void Create_Root()
        {
            Sys.FileSystem.CosmosVFS fs;
            fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
        }


        /// <summary>
        /// We create the system directories and files structure
        /// 0:\Sys\
        /// 0:\Sys\Log\
        /// 0:\Sys\Usr\
        /// 0:\Sys\User\usr.u - for users store
        /// 0:\Sys\Log\logSYS.l - for system logs store
        /// </summary>
        public static void Initialize_Sys_Dirs()
        {
            Console.WriteLine("Loading system...");
            //initialize 'Sys' directory
            if (!System.IO.Directory.Exists(SysDir))
            {
                System.IO.Directory.CreateDirectory(SysDir);
                Console.WriteLine($"Created {SysDir} directory!");
            }

            //initialize 'Usr' directory
            if (!System.IO.Directory.Exists(UsrDir))
            {
                System.IO.Directory.CreateDirectory(UsrDir);
                Console.WriteLine($"Created {UsrDir} directory!");
            }

            //initialize 'Log' directory
            if (!System.IO.Directory.Exists(LogDir))
            {
                System.IO.Directory.CreateDirectory(LogDir);
                Console.WriteLine($"Created {LogDir} directory!");
            }
            //initialize 'Tmp' directory
            if (!System.IO.Directory.Exists(TmpDir))
            {
                System.IO.Directory.CreateDirectory(TmpDir);
                Console.WriteLine($"Created {TmpDir} directory!");
            }

            //initialize 'Tmp' directory
            if (!System.IO.Directory.Exists(TmpDir))
            {
                System.IO.Directory.CreateDirectory(TmpDir);
                Console.WriteLine($"Created {TmpDir} directory!");
            }

            //initialize 'cDir.t' file
            if (!System.IO.File.Exists(cDirFile))
            {
                System.IO.File.Create(cDirFile);
                Console.WriteLine($"Created {cDirFile} file!");
            }

            //initialize 'login.t' file
                System.IO.File.WriteAllText(LoginFile,"0");
                Console.WriteLine($"Initialize {LoginFile} file!");

            //initialize 'logSYS.l' file
            if (!System.IO.File.Exists(SYSLogFile))
            {
                System.IO.File.Create(SYSLogFile);
                Console.WriteLine($"Created {SYSLogFile} file!");
            }
                
            // initialize 'usr.u' file
            if (!System.IO.File.Exists(UsrFile))
            {
                System.IO.File.Create(UsrFile);
                Console.WriteLine($"Created {UsrFile} file!");
                Console.Clear();

                //initiliaze first user account creation on first run
                Users.Initilize_First_User();
            }
        }
        public static void Test_Root()
        {
            string[] filePaths = System.IO.Directory.GetFiles(@"0:\");
            var drive = new DriveInfo("0");
            Console.WriteLine("Volume in drive 0 is " + $"{drive.VolumeLabel}");
            Console.WriteLine("Directory of " + @"0:\");
            Console.WriteLine("\n");
            for (int i = 0; i < filePaths.Length; ++i)
            {
                string path = filePaths[i];
                Console.WriteLine(System.IO.Path.GetFileName(path));
            }
            foreach (var d in System.IO.Directory.GetDirectories(@"0:\"))
            {
                var dir = new DirectoryInfo(d);
                var dirName = dir.Name;

                Console.WriteLine(dirName + " <DIR>");
            }
            Console.WriteLine("\n");
            Console.WriteLine("        " + $"{drive.TotalSize}" + " bytes");
            Console.WriteLine("        " + $"{drive.AvailableFreeSpace}" + " bytes free");
        }

        public static void Test_Root(string PathDir)
        {
            string Pd = System.IO.Directory.GetCurrentDirectory() + "\\" + PathDir;
            string[] filePaths = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory() + "\\" + Pd);

            for (int i = 0; i < filePaths.Length; ++i)
            {
                string path = filePaths[i];
                Console.WriteLine(System.IO.Path.GetFileName(path));
            }
            foreach (var d in System.IO.Directory.GetDirectories(Pd))
            {
                var dir = new DirectoryInfo(d);
                var dirName = dir.Name;

                Console.WriteLine(dirName + " <DIR>");
            }
            Console.WriteLine("\n");
        }
    }
}
