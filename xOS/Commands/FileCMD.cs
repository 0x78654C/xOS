using xOS.FileSystem;
namespace xOS.Commands
{
    public static class FileCMD
    {
        public static void FileCommands(string inputData)
        {
            #region Files Section
            //create file in a certain path or root folder
            if (inputData.StartsWith("mf"))
            {
                FileManagement.CreateFile(inputData);
            }

            //delete file in a certain path or root folder
            if (inputData.StartsWith("rf"))
            {
                FileManagement.DeleteFile(inputData);
            }

            //read file from a certain path or root folder
            if (inputData.StartsWith("cat"))
            {
                FileManagement.ReadFile(inputData);
            }

            //writes data to file in a certain path or root folder with overwrite
            if (inputData.StartsWith("wf"))
            {
                FileManagement.WriteToFile(inputData);
            }


            //append data to file in a certain path or root folder
            if (inputData.StartsWith("af"))
            {
                FileManagement.AppendToFile(inputData);
            }

            //file copy to a certain path
            if (inputData.StartsWith("fcopy"))
            {
                FileManagement.CopyFile(inputData);
            }

            //file move to a certain path
            if (inputData.StartsWith("fmove"))
            {
                FileManagement.MoveFile(inputData);
            }
            #endregion

        }
    }
}
