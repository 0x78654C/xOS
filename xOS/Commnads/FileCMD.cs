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
                File.CreateFile(input);
            }

            //delete file
            if (input.StartsWith("rf"))
            {
                File.DeleteFile(input);
            }

            //read file
            if (input.StartsWith("cat"))
            {
                File.Read_File(input);
            }

            //write to file
            if (input.StartsWith("wf"))
            {
                File.Write_To_File(input);
            }

            //append to file with type
            if (input.StartsWith("af"))
            {
                File.Append_To_File(input);
            }

            //append to file
            if (input.StartsWith("afx"))
            {
                File.Append_To_File2(input);
            }
            #endregion

        }
    }
}
