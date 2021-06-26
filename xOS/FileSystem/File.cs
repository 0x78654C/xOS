using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace xOS.FileSystem
{
    public class File
    {
        public static void CreateFile(string FileName)
        {
            try
            {
                FileName = FileName.Split(' ')[1];
                Sys.FileSystem.VFS.VFSManager.CreateFile(FileName);
                Console.WriteLine($"File {FileName} was created!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void DeleteFile(string FileName)
        {
            try
            {
                FileName = FileName.Split(' ')[1];
                System.IO.File.Delete(FileName);
                Console.WriteLine($"File {FileName} was deleted!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Reading Data from a file
        /// Encoding: UTF8
        /// </summary>
        /// <param name="FileName"></param>
        public static void Read_File(string FileName)
        {
            try
            {
                FileName = FileName.Split(' ')[1];
                if (System.IO.File.Exists(FileName))
                {
                    var hello_file = Sys.FileSystem.VFS.VFSManager.GetFile(FileName);
                    var hello_file_stream = hello_file.GetFileStream();

                    if (hello_file_stream.CanRead)
                    {
                        byte[] text_to_read= System.IO.File.ReadAllBytes(FileName);
                        Console.WriteLine(Encoding.UTF8.GetString(text_to_read));
                    }
                }
                else
                {
                    Console.WriteLine($"FIle {FileName} dose not exit!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Write data to file. 
        /// At the moment only ASCII
        /// </summary>
        /// <param name="FileName"></param>
        public static void Write_To_File(string FileName)
        {
            try
            {
                string Fn = FileName.Split(' ')[1];
                Console.Write("Type: ");
                string Data = Console.ReadLine();
                Console.WriteLine(Data);
                var hello_file = Sys.FileSystem.VFS.VFSManager.GetFile(@Fn);
                if (System.IO.File.Exists(@Fn))
                {
                    var hello_file_stream = hello_file.GetFileStream();

                    if (hello_file_stream.CanWrite)
                    {
                        System.IO.File.WriteAllText(@Fn, Data);
                    }
                }
                else
                {
                    Console.WriteLine($"File {FileName} dose not exist!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void Append_To_File(string input)
        {
            try
            {
                string Fn = input.Split(' ')[1];
                Console.Write("Type: ");
                string Data = Console.ReadLine();
                Console.WriteLine(Data);
                var hello_file = Sys.FileSystem.VFS.VFSManager.GetFile(@Fn);
                if (System.IO.File.Exists(@Fn))
                {
                    var hello_file_stream = hello_file.GetFileStream();
                    var read_file = System.IO.File.ReadAllText(@Fn);

                    if (hello_file_stream.CanWrite)
                    {
                        System.IO.File.WriteAllText(@Fn, read_file + Data);
                    }
                }
                else
                {
                    Console.WriteLine($"File {Fn} dose not exist!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void Append_To_File2(string input)
        {
            try
            {
                string Fn = input.Split(' ')[1];
                string Data = input.Split(' ')[2];
                var hello_file = Sys.FileSystem.VFS.VFSManager.GetFile(@Fn);
                if (System.IO.File.Exists(@Fn))
                {
                    var hello_file_stream = hello_file.GetFileStream();
                    var read_file = System.IO.File.ReadAllText(@Fn);

                    if (hello_file_stream.CanWrite)
                    {
                        System.IO.File.WriteAllText(@Fn, read_file + Data);
                    }
                }
                else
                {
                    Console.WriteLine($"File {Fn} dose not exist!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
