using System.IO;
using xOS.FileSystem;

namespace xOS.Core
{
    public static class Parsing
    {
        private static readonly string s_CurrentDirectory = GlobalVariables.CurrentLocationFile;

        /// <summary>
        /// Get the path of file or adding the current directory path
        /// Used for file copy/move methods
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ParseFilePath(string filePath)
        {
            string _path;
            string _lastSplit;
            string _cDir;
            if (filePath.Contains("\\"))
            {
                int c = 0;

                foreach (char delimiter in filePath)
                {
                    if (delimiter == '\\')
                    {
                        c++;
                    }
                }
                _lastSplit = filePath.Split('\\')[c];
                filePath = filePath.Replace(_lastSplit, "");
                _path = filePath;
                return _path;
            }
            else
            {
                _cDir = File.ReadAllText(s_CurrentDirectory);
                _path = _cDir;
            }
            return _path;
        }
        /// <summary>
        /// Get the path of file or adding the current directory path
        /// Used for file copy/move methods
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static string ParseDirectoryPath(string FilePath)
        {
            string path;
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
                FilePath = FilePath.Split('\\')[c];
                path = FilePath;
                return path;
            }
            else
            {
                cDir = File.ReadAllText(s_CurrentDirectory);
                path = cDir;
            }
            return path;
        }
    }
}
