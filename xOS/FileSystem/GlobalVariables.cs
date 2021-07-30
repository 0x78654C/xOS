namespace xOS.FileSystem
{
    public static class GlobalVariables
    {
        /*Decalre Global Variables*/
        public static string SystemVolume = "0:\\";
        public static string SystemDirectory = SystemVolume + "Sys";
        public static string UserDirectory = SystemVolume + "Users";
        public static string UsersDirectory = SystemDirectory + "\\Usr";
        public static string LogDirectory = SystemDirectory + "\\Log";
        public static string TempDirectory = SystemDirectory + "\\Tmp";
        public static string CurrentLocationFile = TempDirectory + "\\cDir.t";
        public static string LoginFile = TempDirectory + "\\login.t";
        public static string SystemLogFile = LogDirectory + "\\logSYS.l";
        public static string UsersFile = UsersDirectory + "\\usr.u";
    }
}
