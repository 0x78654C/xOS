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
    }
}
