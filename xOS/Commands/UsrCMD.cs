using System;
using System.IO;
using xOS.FileSystem;
namespace xOS.Commands
{
    public static class UsrCMD

    {
        private static readonly string s_UserFile = GlobalVariables.UsersFile;
        private static readonly string s_LoginFile = GlobalVariables.LoginFile;
        private static readonly string s_SysLogFile = FileSystem.GlobalVariables.SystemLogFile;

        public static void UserCommands(string inputData)
        {

            //create user commnad
            //example: cuser username password usertyp(a - administrarot, u - normal user)
            if (inputData.StartsWith("cuser"))
            {
                try
                {

                    //we check if user file exists
                    if (File.Exists(s_UserFile))
                    {
                        string UserAdmin = GetUserType(s_UserFile, s_LoginFile);

                        if (UserAdmin == "a")
                        {
                            Console.Write("User Name: ");
                            string UserName = Console.ReadLine();
                            Console.Write("User Type (a - Administrator, u - Normal User): ");
                            string UserType = Console.ReadLine();
                            Console.Write("User Password: ");
                            string UserPass = UsersManagement.GetHiddenConsoleInput();
                            Console.WriteLine("\n");
                            string UsrFileRead;

                            UsrFileRead = File.ReadAllText(s_UserFile);

                            //we check if user exists in file
                            if (UsrFileRead.Contains(UserName))
                            {
                                Console.WriteLine($"User {UserName}, already exist!");
                            }
                            else
                            {
                                File.AppendAllText(s_UserFile, $"{UserName}|{Cryptography.Encrypt(UserPass)}|{UserType}\n");
                                Console.WriteLine($"Created user: {UserName}");
                                CLog.LogSystem.SystemLogAudit(s_SysLogFile, $"Created user: {UserName}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Your account is not Administrator!");
                        }
                    }
                    else
                    {
                        Console.Write("User Name: ");
                        string UserName = Console.ReadLine();
                        Console.Write("User Password: ");
                        string UserPass = UsersManagement.GetHiddenConsoleInput();
                        Console.WriteLine("\n");
                        string UsrFileRead;

                        //we initialize the users file
                        File.Create(s_UserFile);
                        CLog.LogSystem.SystemLogAudit(s_SysLogFile,$"Users file (usr.u) is initialized!");
                        UsrFileRead = File.ReadAllText(s_UserFile);

                        //we check if user exists in file
                        if (UsrFileRead.Contains(UserName))
                        {
                            Console.WriteLine($"User {UserName}, already exist!");
                        }
                        else
                        {
                            File.AppendAllText(s_UserFile, $"{UserName}|{Cryptography.Encrypt(UserPass)}|a\n");
                            Console.WriteLine($"Created user: {UserName}");
                            CLog.LogSystem.SystemLogAudit(s_SysLogFile,$"Created user: {UserName}");
                        }
                    }

                }
                catch (Exception E)
                {
                    Console.WriteLine(E.Message);
                }
            }
            //---------------------------------------------


            //delete user command
            if (inputData.StartsWith("duser"))
            {
                try
                {
                    if (File.Exists(s_UserFile))
                    {
                        string UserAdmin = GetUserType(s_UserFile, s_LoginFile);
                        if (UserAdmin == "a")
                        {
                            string dUser = inputData.Split(' ')[1];
                            string uList = string.Empty;
                            var ReadUsers = File.ReadAllLines(s_UserFile);
                            foreach (var User in ReadUsers)
                            {
                                if (!User.Contains(dUser) && User.Length > 0)
                                {
                                    uList += User + Environment.NewLine;
                                }
                            }
                            File.WriteAllText(s_UserFile, uList);
                            CLog.LogSystem.SystemLogAudit(s_SysLogFile,$"User {dUser} was deleted!");
                            Console.WriteLine($"User {dUser} was deleted!");
                        }
                        else
                        {
                            Console.WriteLine($"Your account is not Administrator!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("User file does not exist!");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }

        /// <summary>
        /// Get the type of the current user logged in 
        /// </summary>
        /// <param name="userFile">Specify the user.u path</param>
        /// <param name="loginFile">Specify the login.t path</param>
        /// <returns></returns>
        private static string GetUserType(string userFile, string loginFile)
        {
            string[] UsersList = File.ReadAllLines(userFile);
            string LogedUser = File.ReadAllText(loginFile).Split('|')[1];
            string UserAdmin = string.Empty;
            foreach (var user in UsersList)
            {
                if (user.Contains(LogedUser))
                {
                    UserAdmin = user.Split('|')[2].ToLower();
                }
            }
            return UserAdmin;
        }
    }
}
