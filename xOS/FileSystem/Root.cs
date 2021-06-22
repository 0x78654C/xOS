using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;

namespace xOS.FileSystem
{
    public static class Root
    {
        /*Creating root file system*/
        public static void Create_Root()
        {
            Sys.FileSystem.CosmosVFS fs;
            fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
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
                        byte[] text_to_read = new byte[hello_file_stream.Length];
                        hello_file_stream.Read(text_to_read, 0, (int)hello_file_stream.Length);
                        Console.WriteLine(Encoding.Default.GetString(text_to_read));
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
                string Data = FileName.Split(' ')[2];
                var hello_file = Sys.FileSystem.VFS.VFSManager.GetFile(@Fn);
                if (System.IO.File.Exists(@Fn))
                {
                    var hello_file_stream = hello_file.GetFileStream();

                    if (hello_file_stream.CanWrite)
                    {
                        byte[] text_to_write = Encoding.ASCII.GetBytes(Data);
                        hello_file_stream.Write(text_to_write, 0, text_to_write.Length);
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
    }
}
