using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace xOS.FileSystem
{
   public static class Directory
    {
        public static void CreateDir(string DirName)
        {
            try
            {
                DirName = DirName.Split(' ')[1];
                Sys.FileSystem.VFS.VFSManager.CreateDirectory(DirName);
                Console.WriteLine($"Directory {DirName} was created!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void DeleteDir(string DirName)
        {
            try
            {
                DirName = DirName.Split(' ')[1];
                Sys.FileSystem.VFS.VFSManager.DeleteDirectory(DirName,true);
                Console.WriteLine($"Directory {DirName} was deleted!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
