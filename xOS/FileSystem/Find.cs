using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using xOS.UI;

namespace xOS.FileSystem
{
    public class Find
    {
        /// <summary>
        ///  Display files and folders.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="currentDir"></param>
        public static void FindFiles(string searchData, string currentDir)
        {
            var dirList = new List<string>();
            var fileList = new List<string>();
            Directory.GetDirectories(currentDir).ToList().ForEach(d => dirList.Add(d));
            Directory.GetFiles(currentDir).ToList().ForEach(f => fileList.Add(f));
            foreach (var file in fileList)
            {
                var fileInfo = new FileInfo(file);
                if(fileInfo.Name.Contains(searchData))
                    Console.WriteLine($"File: {fileInfo.FullName}");
            }
            foreach (var directory in dirList)
            {
                var dirInfo = new DirectoryInfo(directory);
                if(dirInfo.Name.Contains(searchData))
                    UIColor.ColorConsoleTextLine(ConsoleColor.Green, $"DIR: {dirInfo.FullName}");
                FindFiles(searchData, directory);
            }
        }
    }
}
