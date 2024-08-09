using System;
using System.IO;

namespace xOS.FileSystem
{
    public class Conversion
    {
        private static string[] s_sizes = { "B", "KB", "MB", "GB", "TB" };

        /// <summary>
        /// Get file size.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fixedSize"></param>
        /// <returns></returns>
        public static string GetFileSize(string fileName, bool fixedSize)
        {
            double len = new FileInfo(fileName).Length;
            if (fixedSize)
            {
                var sLen = String.Format("{0:0.##}", len);
                var fLen = Convert.ToDouble(sLen);
                for (int i = 0; i < 2; i++)
                    fLen /= 1024;
                return fLen.ToString();
            }
            else
            {
                var order = 0;
                while (len >= 1024 && order < s_sizes.Length - 1)
                {
                    order++;
                    len /= 1024;
                }
                return String.Format("{0:0.##} {1}", len, s_sizes[order]);
            }
        }

        /// <summary>
        /// Get file size.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fixedSize"></param>
        /// <returns></returns>
        public static string ConvertSize(long size, bool fixedSize)
        {
            long len = size;
            if (fixedSize)
            {
                var sLen = String.Format("{0:0.##}", len);
                var fLen = Convert.ToDouble(sLen);
                for (int i = 0; i < 2; i++)
                    fLen /= 1024;
                return fLen.ToString();
            }
            else
            {
                var order = 0;
                while (len >= 1024 && order < s_sizes.Length - 1)
                {
                    order++;
                    len /= 1024;
                }
                return String.Format("{0:0.##} {1}", len, s_sizes[order]);
            }
        }
    }
}
