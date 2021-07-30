using System;
using System.IO;
using Sys = Cosmos.System;
namespace xOS.FileSystem
{
    public static class Root
    {
        //declare global variables
        private static readonly string s_systemDirectory = GlobalVariables.SystemDirectory;
        private static readonly string s_userDirectory = GlobalVariables.UserDirectory;
        private static readonly string s_userFile = GlobalVariables.UsersFile;
        private static readonly string s_logDirectory = GlobalVariables.LogDirectory;
        private static readonly string s_tempDirectory = GlobalVariables.TempDirectory;
        private static readonly string s_currentLocationFile = GlobalVariables.CurrentLocationFile;
        private static readonly string s_loginFile = GlobalVariables.LoginFile;
        private static readonly string s_systemLogFile = GlobalVariables.SystemLogFile;
        private static readonly string s_usersFile = GlobalVariables.UsersFile;
        //--------------------------

        /*Creating root file system*/
        public static void CreateRoot()
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
        public static void InitializeSystemDirectories()
        {
            Console.WriteLine("Loading system...");
            //initialize 'Sys' directory
            if (!Directory.Exists(s_systemDirectory))
            {
                Directory.CreateDirectory(s_systemDirectory);
                Console.WriteLine($"Created {s_systemDirectory} directory!");
            }

            //initialize 'Users' directory
            if (!Directory.Exists(s_userDirectory))
            {
                Directory.CreateDirectory(s_userDirectory);
                Console.WriteLine($"Created {s_userDirectory} directory!");
            }

            //initialize 'Usr' directory
            if (!Directory.Exists(s_userFile))
            {
                Directory.CreateDirectory(s_userFile);
                Console.WriteLine($"Created {s_userFile} directory!");
            }

            //initialize 'Log' directory
            if (!Directory.Exists(s_logDirectory))
            {
                Directory.CreateDirectory(s_logDirectory);
                Console.WriteLine($"Created {s_logDirectory} directory!");
            }
            //initialize 'Tmp' directory
            if (!Directory.Exists(s_tempDirectory))
            {
                Directory.CreateDirectory(s_tempDirectory);
                Console.WriteLine($"Created {s_tempDirectory} directory!");
            }

            //initialize 'Tmp' directory
            if (!Directory.Exists(s_tempDirectory))
            {
                Directory.CreateDirectory(s_tempDirectory);
                Console.WriteLine($"Created {s_tempDirectory} directory!");
            }

            //initialize 'cDir.t' file
            if (!File.Exists(s_currentLocationFile))
            {
                File.Create(s_currentLocationFile);
                Console.WriteLine($"Created {s_currentLocationFile} file!");
            }

            //initialize 'login.t' file
            File.WriteAllText(s_loginFile, "0");
            Console.WriteLine($"Initialize {s_loginFile} file!");

            //initialize 'logSYS.l' file
            if (!File.Exists(s_systemLogFile))
            {
                File.Create(s_systemLogFile);
                Console.WriteLine($"Created {s_systemLogFile} file!");
            }

            // initialize 'usr.u' file
            if (!File.Exists(s_usersFile))
            {
                File.Create(s_usersFile);
                Console.WriteLine($"Created {s_usersFile} file!");
                Console.Clear();

                //initiliaze first user account creation on first run
                UsersManagement.Initilize_First_User();
            }
        }

        /// <summary>
        /// ls command 
        /// </summary>
        public static void ListCommand()
        {
            string cDir = File.ReadAllText(s_currentLocationFile);
            var drive = new DriveInfo("0");
            if (string.IsNullOrEmpty(cDir))
            {
                string[] filePaths = Directory.GetFiles(@"0:\");
                Console.WriteLine("\n");

                for (int i = 0; i < filePaths.Length; ++i)
                {
                    string path = filePaths[i];
                    Console.WriteLine(System.IO.Path.GetFileName(path));
                }
                foreach (var d in Directory.GetDirectories(@"0:\"))
                {
                    var dir = new DirectoryInfo(d);
                    var dirName = dir.Name;

                    Console.WriteLine(dirName + " <DIR>");
                }
            }
            else
            {
                string[] filePaths = Directory.GetFiles(cDir);
                Console.WriteLine("\n");
                for (int i = 0; i < filePaths.Length; ++i)
                {
                    string path = filePaths[i];
                    Console.WriteLine(System.IO.Path.GetFileName(path));
                }
                foreach (var d in Directory.GetDirectories(cDir))
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
        /// <param name="pathDirectory"></param>
        public static void ListCommand(string pathDirectory)
        {
            if (Directory.Exists(pathDirectory))
            {
                var drive = new DriveInfo("0");
                string[] filePaths = Directory.GetFiles(pathDirectory);

                for (int i = 0; i < filePaths.Length; ++i)
                {
                    string path = filePaths[i];
                    Console.WriteLine(System.IO.Path.GetFileName(path));
                }
                foreach (var d in Directory.GetDirectories(pathDirectory))
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
                Console.WriteLine($"Directory {pathDirectory} does not exist!");
            }
        }
    }
}
