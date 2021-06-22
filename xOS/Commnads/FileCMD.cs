using System;
using System.Collections.Generic;
using System.Text;
using xOS.FileSystem;
namespace xOS.Commnads
{
   public static class FileCMD
    {
        public static void RunFileCMD(string input)
        {

            #region Files Section
            //create file
            if (input.StartsWith("mkfile"))
            {
                FileSystem.File.CreateFile(input);
            }

            //delete file
            if (input.StartsWith("rf"))
            {
                FileSystem.File.DeleteFile(input);
            }

            //read file
            if (input.StartsWith("cat"))
            {
                Root.Read_File(input);
            }

            //write to file
            if (input.StartsWith("wf"))
            {
                Root.Write_To_File(input);
            }
            #endregion

        }
    }
}
