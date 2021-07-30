namespace xOS.Commands
{
    public static class DirectoryCommand
    {
        public static void DirectoryCommands(string inputData)
        {
            #region Directory Section
            //make new directory
            if (inputData.StartsWith("mkdir"))
            {
                FileSystem.DirectoryManagement.CreateDirectory(inputData);
            }

            //delete directory
            if (inputData.StartsWith("rmdir"))
            {
                FileSystem.DirectoryManagement.DeleteDirectory(inputData);
            }

            //copy directory
            if (inputData.StartsWith("dcopy"))
            {
                FileSystem.DirectoryManagement.CopyDirectory(inputData);
            }

            //move directory
            if (inputData.StartsWith("dmove"))
            {
                FileSystem.DirectoryManagement.MoveDirectory(inputData);
            }
            #endregion
        }
    }
}
