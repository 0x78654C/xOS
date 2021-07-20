using System;
using System.Collections.Generic;
using System.Text;

namespace xOS.FileSystem
{
    public static class GVariables
    {
        /*Decalre Global Variables*/
        public static string SysVol = "0:\\";
        public static string SysDir = SysVol+"Sys";
        public static string UsersDir = SysVol + "Users";
        public static string UsrDir = SysDir+"\\Usr";
        public static string LogDir = SysDir+"\\Log";
        public static string TmpDir = SysDir + "\\Tmp";
        public static string cDirFile = TmpDir + "\\cDir.t";
        public static string LoginFile = TmpDir + "\\login.t";
        public static string SYSLogFile = LogDir+ "\\logSYS.l";
        public static string UsrFile = UsrDir+ "\\usr.u";
    }
}
