using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using xOS.FileSystem;

namespace xOS.Core
{
    public static class Parsing
    {
        private static string cDirFile = GVariables.cDirFile; //current directory location
        /// <summary>
        /// Get the path of file or adding the current directory path
        /// Used for file copy/move methods
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static string ParseFilePath(string FilePath)
        {
            string path;
            string lastSplit;
            string cDir;
            if (FilePath.Contains("\\"))
            {
                int c = 0;

                foreach (char delimiter in FilePath)
                {
                    if (delimiter == '\\')
                    {
                        c++;
                    }
                }
                lastSplit = FilePath.Split('\\')[c];
                FilePath = FilePath.Replace(lastSplit, "");
                path = FilePath;
                return path;
            }
            else
            {
                cDir = File.ReadAllText(cDirFile);
                path = cDir;
            }
            return path;
        }
    }
}
