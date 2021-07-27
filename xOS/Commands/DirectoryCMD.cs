﻿using System;
using System.Collections.Generic;
using System.Text;


namespace xOS.Commands
{
   public static class DirectoryCMD
    {
        public static void RunDirCMD(string input)
        {
            #region Directory Section
            //make new directory
            if (input.StartsWith("mkdir"))
            {
                FileSystem.DirectoryM.CreateDir(input);
            }

            //delete directory
            if (input.StartsWith("rmdir"))
            {
                FileSystem.DirectoryM.DeleteDir(input);
            }
            #endregion
        }
    }
}