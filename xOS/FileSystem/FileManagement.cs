using System;
using System.IO;
using System.Text;
using xOS.Core;
using Sys = Cosmos.System;

namespace xOS.FileSystem
{
    public class FileManagement
    {
        /*File Management class*/

        private static readonly string s_CurrentDirectory = GlobalVariables.CurrentLocationFile; //current directory location


        /// <summary>
        /// Creates a simple file. Command: mf 
        /// </summary>
        /// <param name="fileName">Specify a file name</param>
        public static void CreateFile(string fileName)
        {
            try
            {
                string cDir = File.ReadAllText(s_CurrentDirectory);
                fileName = fileName.Split(' ')[1];
                if (string.IsNullOrEmpty(cDir) && fileName.Contains(@"0:\"))
                {
                    Sys.FileSystem.VFS.VFSManager.CreateFile(fileName);
                    Console.WriteLine($"File {fileName} was created!");
                }
                else
                {
                    Sys.FileSystem.VFS.VFSManager.CreateFile(cDir + @"\" + fileName);
                    Console.WriteLine($"File {fileName} was created!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Deletes a specific file. Command: rf
        /// </summary>
        /// <param name="fileName">Specify a file name</param>
        public static void DeleteFile(string fileName)
        {
            try
            {
                fileName = fileName.Split(' ')[1];
                string cDir = File.ReadAllText(s_CurrentDirectory);
                if (string.IsNullOrEmpty(cDir) && fileName.Contains(@"0:\"))
                {
                    File.Delete(fileName);
                    Console.WriteLine($"File {fileName} was deleted!");
                }
                else
                {
                    File.Delete(cDir + @"\" + fileName);
                    Console.WriteLine($"File {fileName} was deleted!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Reading Data from a file. Command: df
        /// Encoding: UTF8
        /// </summary>
        /// <param name="fileName">Specify a file name</param>
        public static void ReadFile(string fileName)
        {
            try
            {
                fileName = fileName.Split(' ')[1];
                string cDir = File.ReadAllText(s_CurrentDirectory);
                if (string.IsNullOrEmpty(cDir) && fileName.Contains(@"0:\"))
                {
                    if (File.Exists(fileName))
                    {
                        var hello_file = Sys.FileSystem.VFS.VFSManager.GetFile(fileName);
                        var hello_file_stream = hello_file.GetFileStream();

                        if (hello_file_stream.CanRead)
                        {
                            byte[] text_to_read = File.ReadAllBytes(fileName);
                            Console.WriteLine(Encoding.UTF8.GetString(text_to_read));
                        }
                    }
                    else
                    {
                        Console.WriteLine($"FIle {fileName} does not exit!");
                    }
                }
                else
                {
                    fileName = cDir + @"\" + fileName;
                    if (File.Exists(fileName))
                    {
                        var hello_file = Sys.FileSystem.VFS.VFSManager.GetFile(fileName);
                        var hello_file_stream = hello_file.GetFileStream();

                        if (hello_file_stream.CanRead)
                        {
                            byte[] text_to_read = File.ReadAllBytes(fileName);
                            Console.WriteLine(Encoding.UTF8.GetString(text_to_read));
                        }
                    }
                    else
                    {
                        Console.WriteLine($"FIle {fileName} does not exit!");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Write data to file with overwrite. Command: wf
        /// At the moment only ASCII
        /// </summary>
        /// <param name="fileName">Specify a file name</param>
        public static void WriteToFile(string fileName)
        {
            try
            {
                string Fn = fileName.Split(' ')[1];
                string cDir = File.ReadAllText(s_CurrentDirectory);
                if (string.IsNullOrEmpty(cDir) && fileName.Contains(@"0:\"))
                {
                    Console.Write("Type: ");
                    string Data = Console.ReadLine();
                    Console.WriteLine(Data);
                    var hello_file = Sys.FileSystem.VFS.VFSManager.GetFile(@Fn);
                    if (File.Exists(@Fn))
                    {
                        var hello_file_stream = hello_file.GetFileStream();

                        if (hello_file_stream.CanWrite)
                        {
                            File.WriteAllText(@Fn, Data);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"File {fileName} does not exist!");
                    }
                }
                else
                {
                    Fn = cDir + @"\" + Fn;
                    Console.Write("Type: ");
                    string Data = Console.ReadLine();
                    Console.WriteLine(Data);
                    var hello_file = Sys.FileSystem.VFS.VFSManager.GetFile(@Fn);
                    if (File.Exists(@Fn))
                    {
                        var hello_file_stream = hello_file.GetFileStream();

                        if (hello_file_stream.CanWrite)
                        {
                            File.WriteAllText(@Fn, Data);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"File {fileName} does not exist!");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Appends data to a file. Command: af
        /// </summary>
        /// <param name="fileName">Specify a file name</param>
        public static void AppendToFile(string fileName)
        {
            try
            {
                string Fn = fileName.Split(' ')[1];
                string Data = fileName.Split(' ')[2];
                string cDir = File.ReadAllText(s_CurrentDirectory);
                if (string.IsNullOrEmpty(cDir) && fileName.Contains(@"0:\"))
                {
                    var hello_file = Sys.FileSystem.VFS.VFSManager.GetFile(@Fn);
                    if (File.Exists(@Fn))
                    {
                        var hello_file_stream = hello_file.GetFileStream();
                        var read_file = File.ReadAllText(@Fn);

                        if (hello_file_stream.CanWrite)
                        {
                            File.WriteAllText(@Fn, read_file + Data);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"File {Fn} does not exist!");
                    }
                }
                else
                {
                    Fn = cDir + @"\" + Fn;
                    var hello_file = Sys.FileSystem.VFS.VFSManager.GetFile(@Fn);
                    if (File.Exists(@Fn))
                    {
                        var hello_file_stream = hello_file.GetFileStream();
                        var read_file = File.ReadAllText(@Fn);

                        if (hello_file_stream.CanWrite)
                        {
                            File.WriteAllText(@Fn, read_file + Data);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"File {Fn} does not exist!");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Copy file to a certain path. Command: fcopy
        /// </summary>
        /// <param name="data">source and destination</param>
        public static void CopyFile(string data)
        {
            try
            {
                string source = data.Split(' ')[1];
                string destination = data.Split(' ')[2];
                string cDir = File.ReadAllText(s_CurrentDirectory);
                string sPath = Parsing.ParseFilePath(source);
                string dPath = Parsing.ParseFilePath(destination);

                if ((sPath != cDir) && (dPath != cDir)) //path of destination and source file is not equal to current dir
                {
                    if (!File.Exists(destination) && File.Exists(source))
                    {
                        File.Copy(source, destination);
                        Console.WriteLine($"File {source} copied to {destination}");
                    }
                    else
                    {
                        Console.WriteLine($"File {destination} already exists or source file does not exist!");
                    }
                }
                else if ((sPath == cDir) && (dPath != cDir)) //path of source equals the current directory but destination does not
                {
                    source = cDir + @"\" + source;

                    if (!File.Exists(destination) && File.Exists(source))
                    {
                        File.Copy(source, destination);
                        Console.WriteLine($"File {source} copied to {destination}");
                    }
                    else
                    {
                        Console.WriteLine($"File {destination} already exists or source file does not exist!");
                    }
                }
                else if ((sPath != cDir) && (dPath == cDir)) //path of destination equals the current directory but source does not
                {
                    destination = cDir + @"\" + destination;
                    if (!File.Exists(destination) && File.Exists(source))
                    {
                        File.Copy(source, destination);
                        Console.WriteLine($"File {source} copied to {destination}");
                    }
                    else
                    {
                        Console.WriteLine($"File {destination} already exists or source file does not exist!");
                    }
                }
                else if ((sPath == cDir) && (dPath == cDir)) //both source and destination equals to current path
                {
                    source = cDir + @"\" + source;
                    destination = cDir + @"\" + destination;
                    if (!File.Exists(destination) && File.Exists(source))
                    {
                        File.Copy(source, destination);
                        Console.WriteLine($"File {source} copied to {destination}");
                    }
                    else
                    {
                        Console.WriteLine($"File {destination} already exists or source file does not exist!");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Move file to a certain path. Command fmove
        /// </summary>
        /// <param name="data">source and destination</param>
        public static void MoveFile(string data)
        {
            try
            {
                string source = data.Split(' ')[1];
                string destination = data.Split(' ')[2];
                string cDir = File.ReadAllText(s_CurrentDirectory);
                string sPath = Parsing.ParseFilePath(source);
                string dPath = Parsing.ParseFilePath(destination);

                if ((sPath != cDir) && (dPath != cDir)) //path of destination and source file is not equal to current dir
                {
                    if (!File.Exists(destination) && File.Exists(source))
                    {
                        File.Copy(source, destination);
                        if (File.Exists(destination))
                        {
                            File.Delete(source);
                            Console.WriteLine($"File {source} moved to {destination}");
                        }
                        else
                        {
                            Console.WriteLine($"File {source} could not be moved!");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"File {destination} already exists or source file does not exist!");
                    }
                }
                else if ((sPath == cDir) && (dPath != cDir)) //path of source equals the current directory but destination does not
                {
                    source = cDir + @"\" + source;
                    if (!File.Exists(destination) && File.Exists(source))
                    {
                        File.Copy(source, destination);
                        if (File.Exists(destination))
                        {
                            File.Delete(source);
                            Console.WriteLine($"File {source} moved to {destination}");
                        }
                        else
                        {
                            Console.WriteLine($"File {source} could not be moved!");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"File {destination} already exists or source file does not exist!");
                    }
                }
                else if ((sPath != cDir) && (dPath == cDir)) //path of destination equals the current directory but source does not
                {
                    destination = cDir + @"\" + destination;
                    if (!File.Exists(destination) && File.Exists(source))
                    {
                        File.Copy(source, destination);
                        if (File.Exists(destination))
                        {
                            File.Delete(source);
                            Console.WriteLine($"File {source} moved to {destination}");
                        }
                        else
                        {
                            Console.WriteLine($"File {source} could not be moved!");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"File {destination} already exists or source file does not exist!");
                    }
                }
                else if ((sPath == cDir) && (dPath == cDir)) //both source and destination equals to current path
                {
                    source = cDir + @"\" + source;
                    destination = cDir + @"\" + destination;
                    if (!File.Exists(destination) && File.Exists(source))
                    {
                        File.Copy(source, destination);
                        if (File.Exists(destination))
                        {
                            File.Delete(source);
                            Console.WriteLine($"File {source} moved to {destination}");
                        }
                        else
                        {
                            Console.WriteLine($"File {source} could not be moved!");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"File {destination} already exists or source file does not exist!");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
