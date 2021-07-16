using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using xOS.FileSystem;
namespace xOS.Commnads
{
    public static class UsrCMD

    {
        private static string UsrFile = GVariables.UsrFile;
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
                    if (System.IO.File.Exists(UsrFile))
                    {
                        UsrFileRead = System.IO.File.ReadAllText(UsrFile);

                        //we check if user exists in file
                        if (UsrFileRead.Contains(UserName))
                        {
                            Console.WriteLine($"User {UserName}, already exist!");
                        }
                        else
                        {
                            System.IO.File.AppendAllText(UsrFile, $"{UserName}|{Cryptography.Encrypt(UserPass)}\n");
                            Console.WriteLine($"Created user: {UserName}");
                            CLog.CLog.SysLog_LoadOS($"Created user: {UserName}");
                        }
                    }
                    else
                    {
                        //we initialize the users file
                        System.IO.File.Create(UsrFile);
                        CLog.CLog.SysLog_LoadOS($"Users file (usr.u) is initialized!");
                        UsrFileRead = System.IO.File.ReadAllText(UsrFile);

                        //we check if user exists in file
                        if (UsrFileRead.Contains(UserName))
                        { 
                            Console.WriteLine($"User {UserName}, already exist!");
                        }
                        else
                        {
                            System.IO.File.AppendAllText(UsrFile, $"{UserName}|{UserPass}\n");
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


            //delete user command
            if (input.StartsWith("duser"))
            {
                try
                {
                    if (System.IO.File.Exists(UsrFile))
                    {
                        string dUser = input.Split(' ')[1];
                        string uList = string.Empty;
                        var ReadUsers = System.IO.File.ReadAllLines(UsrFile);
                        foreach(var User in ReadUsers)
                        {
                            if (!User.Contains(dUser) && User.Length > 0)
                            {
                                uList += User+Environment.NewLine;
                            }
                        }
                        System.IO.File.WriteAllText(UsrFile, uList);
                        CLog.CLog.SysLog_LoadOS($"User {dUser} was deleted!");
                        Console.WriteLine($"User {dUser} was deleted!");
                    }
                    else
                    {
                        Console.WriteLine("User file dose not exist!");
                    }

                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
     }
}
