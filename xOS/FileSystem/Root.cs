using System;
using System.IO;
using xOS.UI;
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
        private static readonly string s_rFilesDirectory = GlobalVariables.RFielsDirectory;
        private static readonly string s_rFilesFile = GlobalVariables.RFilesFile;
        //--------------------------

        /// <summary>
        /// Creating root file system.
        /// </summary>
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
        public static void InitializeSystemStructure()
        {
            Console.WriteLine("Loading system...");
            InitializeDirectories();
            InitializeFiles();
        }

        /// <summary>
        /// ls command 
        /// </summary>
        public static void ListCommand()
        {
            string cDir = File.ReadAllText(s_currentLocationFile);
            var drive = new DriveInfo("0");
            cDir = !string.IsNullOrEmpty(cDir) ? cDir : @"0:\";
            string[] filePaths = Directory.GetFiles(cDir);
            Console.WriteLine("\n");

            for (int i = 0; i < filePaths.Length; ++i)
            {
                string path = filePaths[i];
                Console.WriteLine(Path.GetFileName(path));
            }
            foreach (var d in Directory.GetDirectories(cDir))
            {
                var dir = new DirectoryInfo(d);
                var dirName = dir.Name;
                UIColor.ColorConsoleTextLine(ConsoleColor.DarkYellow, dirName);
            }
            DisplayFolderSize(cDir);
            DisplayPartitonSize(drive);
        }

        /// <summary>
        /// ls command with parameter
        /// </summary>
        /// <param name="pathDirectory"></param>
        public static void ListCommand(string pathDirectory)
        {
            if (!Directory.Exists(pathDirectory))
            {
                Console.WriteLine($"Directory {pathDirectory} does not exist!");
                return;
            }

            var drive = new DriveInfo("0");
            string[] filePaths = Directory.GetFiles(pathDirectory);

            for (int i = 0; i < filePaths.Length; ++i)
            {
                string path = filePaths[i];
                Console.WriteLine(Path.GetFileName(path));
            }
            foreach (var d in Directory.GetDirectories(pathDirectory))
            {
                var dir = new DirectoryInfo(d);
                var dirName = dir.Name;
                UIColor.ColorConsoleTextLine(ConsoleColor.DarkYellow, dirName);
            }
            DisplayFolderSize(pathDirectory);
            DisplayPartitonSize(drive);
        }

        /// <summary>
        /// Display partition information about total size and free space.
        /// </summary>
        /// <param name="drive"></param>
        private static void DisplayPartitonSize(DriveInfo drive)
        {
            Console.WriteLine("Total Size: " + $"{Conversion.ConvertSize(drive.TotalSize,false)}");
            Console.WriteLine("Available Free Space: " + $"{Conversion.ConvertSize(drive.AvailableFreeSpace,false)}");
        }

        /// <summary>
        /// Get directory size.
        /// </summary>
        /// <param name="folder"></param>
        private static void DisplayFolderSize(string folder)
        {
            // var dirInfo = new DirectoryInfo(folder).GetFiles("*.*",SearchOption.AllDirectories); - Not supported by Cosmos Kernel.
            Console.WriteLine("\n");
            Console.WriteLine(@"Partition 0:\ :"); //TODO: get partition letter dynamic.
            Console.WriteLine("Current folder Size: " + $"{Conversion.GetDirSize(new DirectoryInfo(folder))}");
        }

        /// <summary>
        /// Initialize the system directories.
        /// </summary>
        private static void InitializeDirectories()
        {
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

            //initialize 'RFiles' directory
            if (!Directory.Exists(s_rFilesDirectory))
            {
                Directory.CreateDirectory(s_rFilesDirectory);
                Console.WriteLine($"Created {s_rFilesDirectory} directory!");
            }
        }

        // Initialize the system files.
        private static void InitializeFiles()
        {
            //initialize 'cDir.t' file
            if (!File.Exists(s_currentLocationFile))
            {
                File.WriteAllText("0:\\", s_currentLocationFile); // Initilize the root over partition 0;
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

            // initialize 'RFiles.u' file
            if (!File.Exists(s_rFilesFile))
            {
                File.Create(s_rFilesFile);
                Console.WriteLine($"Created {s_rFilesFile} file!");
                Console.Clear();

                //initiliaze first user account creation on first run
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
    }
}
