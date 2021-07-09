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
        
        //login system
        public static string UserLogin()
        {

            Console.WriteLine("--Login--");
            Console.Write("UserName: ");
            string user = Console.ReadLine();
            string o=string.Empty;
            try
            {
                string[] UserFileRead;
                if (System.IO.File.Exists(UsrFile))
                {
                    UserFileRead = System.IO.File.ReadAllLines(UsrFile);
                    foreach(var User in UserFileRead)
                    {
                        if (User.Split('|')[0] == user)
                        {
                            string UserP = User.Split('|')[1];
                            Console.Write("Password: ");
                            string pass = Console.ReadLine();

                            if (pass != UserP)
                            {
                                Console.WriteLine("Wrong passwsord!");
                                o = "Wrong passwsord!";
                            }
                            else
                            {
                                o = $"logged|{User}";
                                CLog.CLog.SysLog_LoadOS($"User: {user} logged");
                            }
                        }
                        else
                        {
                            o = $"User {user} dose not exist!";
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if(o.Contains("dose not exist"))
            {
                Console.WriteLine($"User {user} dose not exist!");
            }
            return o;
        }
        public static void Initialize_Sys_Dirs()
        {
            Console.WriteLine("Loading system...");
            if (!System.IO.Directory.Exists(SysDir))
            {
                System.IO.Directory.CreateDirectory(SysDir);
                Console.WriteLine($"Created {SysDir} directory!");
            }

            if (!System.IO.Directory.Exists(UsrDir))
            {
                System.IO.Directory.CreateDirectory(UsrDir);
                Console.WriteLine($"Created {UsrDir} directory!");
            }

            if (!System.IO.Directory.Exists(UsrFile))
            {
                System.IO.Directory.CreateDirectory(UsrFile);
                Console.WriteLine($"Created {UsrFile} directory!");
            }

            if (!System.IO.Directory.Exists(LogDir))
            {
                System.IO.Directory.CreateDirectory(LogDir);
                Console.WriteLine($"Created {LogDir} directory!");
            }

            if (!System.IO.File.Exists(SYSLogFile))
            {
                System.IO.File.Create(SYSLogFile);
                Console.WriteLine($"Created {SYSLogFile} file!");
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
            string[] filePaths = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory()+"\\"+Pd);

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

        public static void Open_Directory()
        {
//future work
        }
    }
}
