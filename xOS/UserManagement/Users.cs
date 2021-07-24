using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace xOS.FileSystem
{
    public static class Users
    {

        private static string UsrFile = GVariables.UsrFile;


        //login system
        public static string UserLogin()
        {

            Console.WriteLine("--Login--");
            Console.Write("User Name: ");
            string user = Console.ReadLine();
            string o = string.Empty;
            bool exist = false;
            try
            {
                string[] UserFileRead;
                if (File.Exists(UsrFile))
                {
                    UserFileRead = File.ReadAllLines(UsrFile);
                    foreach (var User in UserFileRead)
                    {
                        if (User.Split('|')[0] == user)
                        {
                            exist = true;
                            string UserP = User.Split('|')[1];
                            Console.Write("User Password: ");
                            string pass = GetHiddenConsoleInput();

                            if (Cryptography.Encrypt(pass) != UserP)
                            {
                                Console.WriteLine("Wrong passwsord!");
                                o = "Wrong passwsord!";
                            }
                            else
                            {
                                o = $"logged|{User}";
                                CLog.CLog.SysLog_LoadOS($"User: {user} logged");
                                Console.Clear();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if(exist==false)
            {
                Console.WriteLine($"User {user} does not exist!");
            }
            return o;
        }

        /// <summary>
        /// Initialize the os for creating the new us
        /// </summary>
        public static void Initilize_First_User()
        {
            try
            {
                Console.WriteLine("Welcome to xOS. To use this operating system you need to create first a local administrator account. \n");
                Console.Write("User Name: ");
                string UserName = Console.ReadLine();
                Console.Write("User Password: ");
                string UserPass = GetHiddenConsoleInput();
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
                        File.AppendAllText(UsrFile, $"{UserName}|{Cryptography.Encrypt(UserPass)}|a\n");
                        Console.WriteLine($"Created user: {UserName}");
                        CLog.CLog.SysLog_LoadOS($"Created user: {UserName}");
                    }
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }

        }
        //---------------------------------------------

        /// <summary>
        /// Hidding password imput for strings
        /// </summary>
        /// <returns></returns>
        public static string GetHiddenConsoleInput()
        {
            StringBuilder input = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
                else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
            }
            return input.ToString();
        }
    }
}
