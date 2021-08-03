namespace xOS.Commands
{
    public static class DirectoryCommand
    {
        public static void DirectoryCommands(string inputData)
        {
            #region Directory Section
            // Make new directory
            if (inputData.StartsWith("mkdir"))
            {
                FileSystem.DirectoryManagement.CreateDirectory(inputData);
            }

            // Melete directory
            if (inputData.StartsWith("rmdir"))
            {
                FileSystem.DirectoryManagement.DeleteDirectory(inputData);
            }

            // Copy directory
            if (inputData.StartsWith("dcopy"))
            {
                FileSystem.DirectoryManagement.CopyDirectory(inputData);
            }

            // Move directory
            if (inputData.StartsWith("dmove"))
            {
                FileSystem.DirectoryManagement.MoveDirectory(inputData);
            }
            #endregion
        }
    }
}
