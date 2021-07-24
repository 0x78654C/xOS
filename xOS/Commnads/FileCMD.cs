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
            if (input.StartsWith("mf"))
            {
                FileM.CreateFile(input);
            }

            //delete file in a certain path or root folder
            if (input.StartsWith("rf"))
            {
                FileM.DeleteFile(input);
            }

            //read file from a certain path or root folder
            if (input.StartsWith("df"))
            {
                FileM.Read_File(input);
            }

            //writes data to file in a certain path or root folder with overwrite
            if (input.StartsWith("wf"))
            {
                FileM.Write_To_File(input);
            }


            //append data to file in a certain path or root folder
            if (input.StartsWith("af"))
            {
                FileM.Append_To_File(input);
            }
            #endregion

        }
    }
}
