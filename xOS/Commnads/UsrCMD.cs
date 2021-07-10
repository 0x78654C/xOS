using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace xOS.Commnads
{
    public static class UsrCMD

    {
        private static string UsrFile = FileSystem.GVariables.UsrFile;
        public static void RunUserCMD(string input)
        {

            //create user commnad
            //example: cuser username password
            if (input.StartsWith("cuser"))
            {
                try
                {
                    string UserName = input.Split(' ')[1];
                    string UserPass = input.Split(' ')[2];
                    string UsrFileRead;

                    //we check if user file exists
                    if (File.Exists(UsrFile))
                    {
                        UsrFileRead = File.ReadAllText(UsrFile);

                        //we check if user exists in file
                        if (UsrFileRead.Contains(UserName))
                        {
                            Console.WriteLine($"User {UserName}, already exist!");
                        }
                        else
                        {
                            File.AppendAllText(UsrFile, $"{UserName}|{UserPass}\n");
                            Console.WriteLine($"Created user: {UserName}");
                            CLog.CLog.SysLog_LoadOS($"Created user: {UserName}");
                        }
                    }
                    else
                    {
                        //we initialize the users file
                        System.IO.File.Create(UsrFile);
                        CLog.CLog.SysLog_LoadOS($"Users file (usr.u) is initialized!");
                        UsrFileRead = File.ReadAllText(UsrFile);

                        //we check if user exists in file
                        if (UsrFileRead.Contains(UserName))
                        { 
                            Console.WriteLine($"User {UserName}, already exist!");
                        }
                        else
                        {
                            File.AppendAllText(UsrFile, $"{UserName}|{UserPass}\n");
                            Console.WriteLine($"Created user: {UserName}");
                            CLog.CLog.SysLog_LoadOS($"Created user: {UserName}");
                        }
                    }

                }
                catch(Exception E)
                {
                    Console.WriteLine(E.Message);
                }
            }
            //---------------------------------------------


        }

    }
}
