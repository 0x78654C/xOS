using System;
using System.IO;
using fs = Cosmos.System;
using xOS.Core;

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


        /// <summary>
        /// Copy a directory to a certain location
        /// </summary>
        /// <param name="Data"></param>
        public static void CopyDirectory(string Data)
        {
            try
            {
                string source = Data.Split(' ')[1];
                string destination = Data.Split(' ')[2];
                string cDir = File.ReadAllText(cDirFile);
                string sPath = Parsing.ParseDirectoryPath(source);
                string dPath = Parsing.ParseDirectoryPath(destination);

                if ((sPath != cDir) && (dPath != cDir)) //path of destination and source file is not equal to current dir
                {
                    if (!Directory.Exists(destination) && Directory.Exists(source))
                    {
                        DirectoryCopy(source, destination, true);
                        Console.WriteLine($"Directory {source} was copied to {destination}.");
                    }
                    else
                    {
                        Console.WriteLine($"Directory {destination} already exists or source directory does not exist!");
                    }
                }
                else if ((sPath == cDir) && (dPath != cDir)) //path of source equals the current directory but destination does not
                {
                    source = cDir + @"\" + source;
                    if (!Directory.Exists(destination) && Directory.Exists(source))
                    {
                        DirectoryCopy(source, destination, true);
                        Console.WriteLine($"Directory {source} was copied to {destination}.");
                    }
                    else
                    {
                        Console.WriteLine($"Directory {destination} already exists or source directory does not exist!");
                    }
                }
                else if ((sPath != cDir) && (dPath == cDir)) //path of destination equals the current directory but source does not
                {
                    destination = cDir + @"\" + destination;
                    if (!Directory.Exists(destination) && Directory.Exists(source))
                    {
                        DirectoryCopy(source, destination, true);
                        Console.WriteLine($"Directory {source} was copied to {destination}.");
                    }
                    else
                    {
                        Console.WriteLine($"Directory {destination} already exists or source directory does not exist!");
                    }
                }
                else if ((sPath == cDir) && (dPath == cDir)) //both source and destination equals to current path
                {
                    source = cDir + @"\" + source;
                    destination = cDir + @"\" + destination;
                    if (!Directory.Exists(destination) && Directory.Exists(source))
                    {
                        DirectoryCopy(source, destination,true);
                        Console.WriteLine($"Directory {source} was copied to {destination}.");
                    }
                    else
                    {
                        Console.WriteLine($"Directory {destination} already exists or source directory does not exist!");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /*Work in progress*/
        /// <summary>
        /// Move a directory to a certain location
        /// </summary>
        /// <param name="Data"></param>
        public static void MoveDirectory(string Data)
        {
            try
            {
                string source = Data.Split(' ')[1];
                string destination = Data.Split(' ')[2];
                string cDir = File.ReadAllText(cDirFile);
                string sPath = Parsing.ParseDirectoryPath(source);
                string dPath = Parsing.ParseDirectoryPath(destination);

                if ((sPath != cDir) && (dPath != cDir)) //path of destination and source file is not equal to current dir
                {
                    if (!Directory.Exists(destination) && Directory.Exists(source))
                    {
                        DirectoryMove(source, destination, true);
                        Console.WriteLine($"Directory {source} was moved to {destination}.");
                    }
                    else
                    {
                        Console.WriteLine($"Directory {destination} already exists or source directory does not exist!");
                    }
                }
                else if ((sPath == cDir) && (dPath != cDir)) //path of source equals the current directory but destination does not
                {
                    source = cDir + @"\" + source;
                    if (!Directory.Exists(destination) && Directory.Exists(source))
                    {
                        DirectoryMove(source, destination, true);
                        Console.WriteLine($"Directory {source} was moved to {destination}.");
                    }
                    else
                    {
                        Console.WriteLine($"Directory {destination} already exists or source directory does not exist!");
                    }
                }
                else if ((sPath != cDir) && (dPath == cDir)) //path of destination equals the current directory but source does not
                {
                    destination = cDir + @"\" + destination;
                    if (!Directory.Exists(destination) && Directory.Exists(source))
                    {
                        DirectoryMove(source, destination, true);
                        Console.WriteLine($"Directory {source} was moved to {destination}.");
                    }
                    else
                    {
                        Console.WriteLine($"Directory {destination} already exists or source directory does not exist!");
                    }
                }
                else if ((sPath == cDir) && (dPath == cDir)) //both source and destination equals to current path
                {
                    source = cDir + @"\" + source;
                    destination = cDir + @"\" + destination;
                    if (!Directory.Exists(destination) && Directory.Exists(source))
                    {
                        DirectoryMove(source, destination, true);
                        Console.WriteLine($"Directory {source} was moved to {destination}.");
                    }
                    else
                    {
                        Console.WriteLine($"Directory {destination} already exists or source directory does not exist!");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Copy a directory to a certain location. Command: dcopy
        /// </summary>
        /// <param name="source">Source directory name that you need to be copied</param>
        /// <param name="destination">Destiantion name of directory</param>
        /// <param name="copySubDirs">Copy Sub DIrectories</param>
        private static void DirectoryCopy(string source , string destination, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(source);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException($"Direcotry {source} does not exist");
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);

                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string tmpP = Path.Combine(destination, file.Name);
                    file.CopyTo(tmpP, false);
                }

                if (copySubDirs)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        string tmpP = Path.Combine(destination, subdir.Name);
                        DirectoryCopy(subdir.FullName, tmpP, copySubDirs);
                    }
                }
            }
        }


        /*Works in progess*/
        /// <summary>
        /// Move a directory to a certain location. Command: dcopy
        /// </summary>
        /// <param name="source">Source directory name that you need to be copied</param>
        /// <param name="destination">Destiantion name of directory</param>
        /// <param name="copySubDirs">Copy Sub DIrectories</param>
        private static void DirectoryMove(string source, string destination, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(source);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException($"Direcotry {source} does not exist");
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);

                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string tmpP = Path.Combine(destination, file.Name);
                    file.CopyTo(tmpP,false);
                    fs.FileSystem.VFS.VFSManager.DeleteFile(file.FullName);
                }
                Directory.Delete(source, true);

                if (copySubDirs)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        string tmpP = Path.Combine(destination, subdir.Name);
                        DirectoryMove(subdir.FullName, tmpP, copySubDirs);
                    }
                }
            }
        }

    }
}
