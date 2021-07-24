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
        private static string UsersDir = GVariables.UsersDir;
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
        /// 0:\Sys\User\usr.u - for users login data store
        /// 0:\Sys\Log\logSYS.l - for system logs store
        /// 0:\Tmp\
        /// 0:\Tmp\cDir.t - Stores current directory used by 'cd' command
        /// 0:\Tmp\loing.t - Stores login state. 1 - loged in, 0 - loged out
        /// 0:\Users\
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

            //initialize 'Users' directory
            if (!System.IO.Directory.Exists(UsersDir))
            {
                System.IO.Directory.CreateDirectory(UsersDir);
                Console.WriteLine($"Created {UsersDir} directory!");
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
            if (!File.Exists(cDirFile))
            {
                File.Create(cDirFile);
                Console.WriteLine($"Created {cDirFile} file!");
            }

            //initialize 'login.t' file
                File.WriteAllText(LoginFile,"0");
                Console.WriteLine($"Initialize {LoginFile} file!");

            //initialize 'logSYS.l' file
            if (!File.Exists(SYSLogFile))
            {
                File.Create(SYSLogFile);
                Console.WriteLine($"Created {SYSLogFile} file!");
            }
                
            // initialize 'usr.u' file
            if (!File.Exists(UsrFile))
            {
                File.Create(UsrFile);
                Console.WriteLine($"Created {UsrFile} file!");
                Console.Clear();

                //initiliaze first user account creation on first run
                Users.Initilize_First_User();
            }
        }

        /// <summary>
        /// ls command 
        /// </summary>
        public static void Test_Root()
        {
            string cDir = File.ReadAllText(cDirFile);
            var drive = new DriveInfo("0");
            if (string.IsNullOrEmpty(cDir))
            {
                string[] filePaths = System.IO.Directory.GetFiles(@"0:\");
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
            }
            else
            {
                string[] filePaths = System.IO.Directory.GetFiles(cDir);
                Console.WriteLine("\n");
                for (int i = 0; i < filePaths.Length; ++i)
                {
                    string path = filePaths[i];
                    Console.WriteLine(System.IO.Path.GetFileName(path));
                }
                foreach (var d in System.IO.Directory.GetDirectories(cDir))
                {
                    var dir = new DirectoryInfo(d);
                    var dirName = dir.Name;

                    Console.WriteLine(dirName + " <DIR>");
                }
            }
            Console.WriteLine("\n");
            Console.WriteLine(@"Partition 0:\ :");
            Console.WriteLine("Total Size: " + $"{drive.TotalSize}" + " bytes");
            Console.WriteLine("Available Free Space: " + $"{drive.AvailableFreeSpace}" + " bytes free");
        }

        /// <summary>
        /// ls command with parameter
        /// </summary>
        /// <param name="PathDir"></param>
        public static void Test_Root(string PathDir)
        {
            if (System.IO.Directory.Exists(PathDir))
            {
                var drive = new DriveInfo("0");
                string[] filePaths = System.IO.Directory.GetFiles(PathDir);

                for (int i = 0; i < filePaths.Length; ++i)
                {
                    string path = filePaths[i];
                    Console.WriteLine(System.IO.Path.GetFileName(path));
                }
                foreach (var d in System.IO.Directory.GetDirectories(PathDir))
                {
                    var dir = new DirectoryInfo(d);
                    var dirName = dir.Name;

                    Console.WriteLine(dirName + " <DIR>");
                }

                Console.WriteLine("\n");
                Console.WriteLine(@"Partition 0:\ :");
                Console.WriteLine("Total Size: " + $"{drive.TotalSize}" + " bytes");
                Console.WriteLine("Available Free Space: " + $"{drive.AvailableFreeSpace}" + " bytes free");
            }
            else
            {
                Console.WriteLine($"Directory {PathDir} dose not exist!");
            }
        }
    }
}
