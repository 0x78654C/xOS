using System;
using System.IO;
using System.Text;

namespace xOS.FileSystem
{
    public static class UsersManagement
    {

        private static readonly string s_usersFile = GlobalVariables.UsersFile;
        private static readonly string s_SysLogFile = FileSystem.GlobalVariables.SystemLogFile;


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
                if (File.Exists(s_usersFile))
                {
                    UserFileRead = File.ReadAllLines(s_usersFile);
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
                                CLog.LogSystem.SystemLogAudit(s_SysLogFile, $"User: {user} logged");
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

            if (exist == false)
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
                Console.WriteLine("Welcome to xOS. To use this operating system you need to create first a local administrator account." +Environment.NewLine);
                Console.Write("User Name: ");
                string UserName = Console.ReadLine();
                Console.Write("User Password: ");
                string UserPass = GetHiddenConsoleInput();
                Console.WriteLine(Environment.NewLine);
                string UsrFileRead;

                //we check if user file exists
                if (File.Exists(s_usersFile))
                {
                    UsrFileRead = File.ReadAllText(s_usersFile);

                    //we check if user exists in file
                    if (UsrFileRead.Contains(UserName))
                    {
                        Console.WriteLine($"User {UserName}, already exist!");
                        return;
                    }
                    File.AppendAllText(s_usersFile, $"{UserName}|{Cryptography.Encrypt(UserPass)}|a\n");
                    Console.WriteLine($"Created user: {UserName}");
                    CLog.LogSystem.SystemLogAudit(s_SysLogFile, $"Created user: {UserName}");
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
