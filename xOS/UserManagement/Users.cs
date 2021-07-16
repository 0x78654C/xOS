using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace xOS.FileSystem
{
    public static class Users
    {

        private static string UsrFile = GVariables.UsrFile;
        private static int index = 0;


        //login system
        public static string UserLogin()
        {

            Console.WriteLine("--Login--");
            Console.Write("User Name: ");
            string user = Console.ReadLine();
            string o = string.Empty;
            try
            {
                string[] UserFileRead;
                if (System.IO.File.Exists(UsrFile))
                {
                    UserFileRead = System.IO.File.ReadAllLines(UsrFile);
                    foreach (var User in UserFileRead)
                    {
                        if (User.Split('|')[0] == user)
                        {
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

            if(!o.Contains("logged") || !o.Contains("Wrong passwsord"))
            {
                Console.WriteLine($"User {user} dose not exist!");
               // o = $"User {user} dose not exist!";
            }
            return o;
        }

        public static void Initilize_First_User()
        {
    
            try
            {
                Console.WriteLine("Welcome to xOS. To use this operating system you need to create first a local account. \n");
                Console.Write("User Name: ");
                string UserName = Console.ReadLine();
                Console.Write("User Password: ");
                string UserPass = GetHiddenConsoleInput();
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
        private static string GetHiddenConsoleInput()
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
