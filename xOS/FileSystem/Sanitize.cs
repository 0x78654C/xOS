using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xOS.FileSystem
{
    public class Sanitize
    {
        /// <summary>
        /// Sanitize path.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="currentDir"></param>
        /// <returns></returns>
        public static string SanitizePath(string path, string currentDir) => path.Contains(":") && path.Contains("\\") ? path : $@"{currentDir}{path}";
    }
}
