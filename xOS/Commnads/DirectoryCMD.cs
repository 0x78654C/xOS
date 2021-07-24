using System;
using System.Collections.Generic;
using System.Text;


namespace xOS.Commnads
{
   public static class DirectoryCMD
    {
        public static void RunDirCMD(string input)
        {
            #region Directory Section
            //make new directory
            if (input.StartsWith("mkdir"))
            {
                FileSystem.Directory.CreateDir(input);
            }

            //delete directory
            if (input.StartsWith("rmdir"))
            {
                FileSystem.Directory.DeleteDir(input);
            }
            #endregion

        }
    }
}
