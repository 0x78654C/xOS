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
            //example: cuser username password usertyp(a - administrarot, u - normal user)
            if (input.StartsWith("cuser"))
            {
                try
                {
                    Console.Write("User Name: ");
                    string UserName = Console.ReadLine();
                    Console.Write("User Type (a - Administrator, u - Normal User): ");
                    string UserType = Console.ReadLine();
                    Console.Write("User Password: ");
                    string UserPass = Users.GetHiddenConsoleInput();
                    Console.WriteLine("\n");
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
                            File.AppendAllText(UsrFile, $"{UserName}|{Cryptography.Encrypt(UserPass)}|{UserType}\n");
                            Console.WriteLine($"Created user: {UserName}");
                            CLog.CLog.SysLog_LoadOS($"Created user: {UserName}");
                        }
                    }
                    else
                    {
                        //we initialize the users file
                        File.Create(UsrFile);
                        CLog.CLog.SysLog_LoadOS($"Users file (usr.u) is initialized!");
                        UsrFileRead = File.ReadAllText(UsrFile);

                        //we check if user exists in file
                        if (UsrFileRead.Contains(UserName))
                        { 
                            Console.WriteLine($"User {UserName}, already exist!");
                        }
                        else
                        {
                            File.AppendAllText(UsrFile, $"{UserName}|{Cryptography.Encrypt(UserPass)}|{UserType}\n");
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
                    if (File.Exists(UsrFile))
                    {
                        string dUser = input.Split(' ')[1];
                        string uList = string.Empty;
                        var ReadUsers = File.ReadAllLines(UsrFile);
                        foreach(var User in ReadUsers)
                        {
                            if (!User.Contains(dUser) && User.Length > 0)
                            {
                                uList += User+Environment.NewLine;
                            }
                        }
                        File.WriteAllText(UsrFile, uList);
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
