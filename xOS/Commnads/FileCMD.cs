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
            //create file in a certain path or root folder
            if (input.StartsWith("mkfile"))
            {
                File.CreateFile(input);
            }

            //delete file in a certain path or root folder
            if (input.StartsWith("rf"))
            {
                File.DeleteFile(input);
            }

            //read file from a certain path or root folder
            if (input.StartsWith("cat"))
            {
                File.Read_File(input);
            }

            //writes data to file in a certain path or root folder with overwrite
            if (input.StartsWith("wf"))
            {
                File.Write_To_File(input);
            }


            //append data to file in a certain path or root folder
            if (input.StartsWith("af"))
            {
                File.Append_To_File2(input);
            }
            #endregion

        }
    }
}
