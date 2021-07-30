using System;
using System.IO;
using xOS.Core;
using fs = Cosmos.System;

namespace xOS.FileSystem
{
    public static class DirectoryManagement
    {
        private static readonly string s_CurrentDirectory = GlobalVariables.CurrentLocationFile; //current directory location

        /// <summary>
        /// Creates a directory. Command: mkdir
        /// </summary>
        /// <param name="dirName">The directory name</param>
        public static void CreateDirectory(string dirName)
        {
            try
            {
                string cDir = File.ReadAllText(s_CurrentDirectory);
                dirName = dirName.Split(' ')[1];

                if (!string.IsNullOrEmpty(cDir) && !dirName.Contains(@":\"))
                {
                    Directory.CreateDirectory(cDir + @"\" + dirName);
                    Console.WriteLine($"Directory {cDir + @"\" + dirName} was created!");
                }
                else
                {
                    Directory.CreateDirectory(dirName);
                    Console.WriteLine($"Directory {dirName} was created!");
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
        public static void DeleteDirectory(string DirName)
        {
            try
            {
                string cDir = File.ReadAllText(s_CurrentDirectory);
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
        /// <param name="data"></param>
        public static void CopyDirectory(string data)
        {
            try
            {
                string source = data.Split(' ')[1];
                string destination = data.Split(' ')[2];
                string cDir = File.ReadAllText(s_CurrentDirectory);
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
                        DirectoryCopy(source, destination, true);
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
        /// <param name="data"></param>
        public static void MoveDirectory(string data)
        {
            try
            {
                string source = data.Split(' ')[1];
                string destination = data.Split(' ')[2];
                string cDir = File.ReadAllText(s_CurrentDirectory);
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
        private static void DirectoryCopy(string source, string destination, bool copySubDirs)
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
                    file.CopyTo(tmpP, false);
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
