using System;
using System.IO;
using fs = Cosmos.System;

namespace xOS.FileSystem
{
   public static class DirectoryM
    {
        private static string cDirFile = GVariables.cDirFile; //current directory location
        
        /// <summary>
        /// Creates a directory. Command: mkdir
        /// </summary>
        /// <param name="DirName">The directory name</param>
        public static void CreateDir(string DirName)
        {
            try
            {
                string cDir = File.ReadAllText(cDirFile);
                DirName = DirName.Split(' ')[1];

                if (!string.IsNullOrEmpty(cDir) && !DirName.Contains(@":\"))
                {
                    Directory.CreateDirectory(cDir+@"\"+DirName);
                    Console.WriteLine($"Directory {cDir + @"\" + DirName} was created!");
                }
                else
                {
                    Directory.CreateDirectory(DirName);
                    Console.WriteLine($"Directory {DirName} was created!");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Delete recursevly a directory. Command: rmdir
        /// </summary>
        /// <param name="DirName">The directory name</param>
        public static void DeleteDir(string DirName)
        {
            try
            {
                string cDir = File.ReadAllText(cDirFile);
                DirName = DirName.Split(' ')[1];

                if (!string.IsNullOrEmpty(cDir) && !DirName.Contains(@":\"))
                {
                    Directory.Delete(cDir + @"\" + DirName, true);
                    Console.WriteLine($"Directory {cDir + @"\" + DirName} was deleted!");
                }
                else
                {
                    Directory.Delete(DirName);
                    Console.WriteLine($"Directory {DirName} was deleted!");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
