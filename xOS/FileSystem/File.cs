using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;

namespace xOS.FileSystem
{
    public class FileM
    {
        /*File Management class*/

        private static string cDirFile = GVariables.cDirFile; //current directory location


        /// <summary>
        /// Creates a simple file. Command: mf 
        /// </summary>
        /// <param name="FileName">Specify a file name</param>
        public static void CreateFile(string FileName)
        {
            try
            {
                string cDir = File.ReadAllText(cDirFile);
                FileName = FileName.Split(' ')[1];
                if (string.IsNullOrEmpty(cDir) && FileName.Contains(@"0:\"))
                {
                    Sys.FileSystem.VFS.VFSManager.CreateFile(FileName);
                    Console.WriteLine($"File {FileName} was created!");
                }
                else
                {
                    Sys.FileSystem.VFS.VFSManager.CreateFile(cDir + @"\" + FileName);
                    Console.WriteLine($"File {FileName} was created!");
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
        /// <param name="FileName">Specify a file name</param>
        public static void DeleteFile(string FileName)
        {
            try
            {
                FileName = FileName.Split(' ')[1];
                string cDir = File.ReadAllText(cDirFile);
                if (string.IsNullOrEmpty(cDir) && FileName.Contains(@"0:\"))
                {
                    File.Delete(FileName);
                    Console.WriteLine($"File {FileName} was deleted!");
                }
                else
                {
                    File.Delete(cDir + @"\" + FileName);
                    Console.WriteLine($"File {FileName} was deleted!");
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
        /// <param name="FileName">Specify a file name</param>
        public static void Read_File(string FileName)
        {
            try
            {
                FileName = FileName.Split(' ')[1];
                string cDir = File.ReadAllText(cDirFile);
                if (string.IsNullOrEmpty(cDir) && FileName.Contains(@"0:\"))
                {
                    if (File.Exists(FileName))
                    {
                        var hello_file = Sys.FileSystem.VFS.VFSManager.GetFile(FileName);
                        var hello_file_stream = hello_file.GetFileStream();

                        if (hello_file_stream.CanRead)
                        {
                            byte[] text_to_read = File.ReadAllBytes(FileName);
                            Console.WriteLine(Encoding.UTF8.GetString(text_to_read));
                        }
                    }
                    else
                    {
                        Console.WriteLine($"FIle {FileName} does not exit!");
                    }
                }
                else
                {
                    FileName = cDir + @"\" + FileName;
                    if (File.Exists(FileName))
                    {
                        var hello_file = Sys.FileSystem.VFS.VFSManager.GetFile(FileName);
                        var hello_file_stream = hello_file.GetFileStream();

                        if (hello_file_stream.CanRead)
                        {
                            byte[] text_to_read = File.ReadAllBytes(FileName);
                            Console.WriteLine(Encoding.UTF8.GetString(text_to_read));
                        }
                    }
                    else
                    {
                        Console.WriteLine($"FIle {FileName} does not exit!");
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
        /// <param name="FileName">Specify a file name</param>
        public static void Write_To_File(string FileName)
        {
            try
            {
                string Fn = FileName.Split(' ')[1];
                string cDir = File.ReadAllText(cDirFile);
                if (string.IsNullOrEmpty(cDir) && FileName.Contains(@"0:\"))
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
                        Console.WriteLine($"File {FileName} does not exist!");
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
                        Console.WriteLine($"File {FileName} does not exist!");
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
        /// <param name="FileName">Specify a file name</param>
        public static void Append_To_File(string FileName)
        {
            try
            {
                string Fn = FileName.Split(' ')[1];
                string Data = FileName.Split(' ')[2];
                string cDir = File.ReadAllText(cDirFile);
                if (string.IsNullOrEmpty(cDir) && FileName.Contains(@"0:\"))
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
    }
}
