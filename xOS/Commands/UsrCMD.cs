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
        private static int s_UserPassCheck = 0;

        public static void UserCommands(string inputData)
        {

            //create user commnad
            //example: cuser username password usertyp(a - administrarot, u - normal user)
            if (inputData.StartsWith("cuser"))
            {
                try
                {
                    string UserName = string.Empty;
                    string UserType = string.Empty;
                    string UserPass = string.Empty;
                    string UsrFileRead = string.Empty;
                    Console.Write("User Name: ");
                    UserName = Console.ReadLine();
                    Console.Write("User Password: ");
                    UserPass = UsersManagement.GetHiddenConsoleInput();
                    CheckUserPassIntergirty(UserName, UserPass);

                    if (s_UserPassCheck > 1)
                    {
                        return;
                    }

                    //we check if user file exists
                    if (File.Exists(s_UserFile))
                    {
                        string UserAdmin = GetUserType(s_UserFile, s_LoginFile);

                        if (UserAdmin == "a")
                        {
                            Console.Write("User Type (a - Administrator, u - Normal User): ");
                            UserType = Console.ReadLine();
                            Console.WriteLine(Environment.NewLine);
                            UsrFileRead = File.ReadAllText(s_UserFile);

                            //we check if user exists in file
                            if (UsrFileRead.Contains(UserName))
                            {
                                Console.WriteLine($"User {UserName}, already exist!");
                                return;
                            }

                            File.AppendAllText(s_UserFile, $"{UserName}|{Cryptography.Encrypt(UserPass)}|{UserType}" + Environment.NewLine);
                            Console.WriteLine($"Created user: {UserName}");
                            CLog.LogSystem.SystemLogAudit(s_SysLogFile, $"Created user: {UserName}");
                            return;
                        }

                        Console.WriteLine($"Your account is not Administrator!");
                        return;
                    }

                    //we initialize the users file
                    File.Create(s_UserFile);
                    CLog.LogSystem.SystemLogAudit(s_SysLogFile, $"Users file (usr.u) is initialized!");
                    UsrFileRead = File.ReadAllText(s_UserFile);

                    //we check if user exists in file
                    if (UsrFileRead.Contains(UserName))
                    {
                        Console.WriteLine($"User {UserName}, already exist!");
                        return;
                    }

                    File.AppendAllText(s_UserFile, $"{UserName}|{Cryptography.Encrypt(UserPass)}|a" + Environment.NewLine);
                    Console.WriteLine($"Created user: {UserName}");
                    CLog.LogSystem.SystemLogAudit(s_SysLogFile, $"Created user: {UserName}");
                    return;
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
                            CLog.LogSystem.SystemLogAudit(s_SysLogFile, $"User {dUser} was deleted!");
                            Console.WriteLine($"User {dUser} was deleted!");
                            return;
                        }

                        Console.WriteLine($"Your account is not Administrator!");
                        return;
                    }

                    Console.WriteLine("User file does not exist!");

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

        // Check the password and user length. Should be bigger than 4
        private static void CheckUserPassIntergirty(string userName, string passWord)
        {
            if (userName.Length < 4)
            {
                Console.WriteLine(Environment.NewLine + "User name must have more than 4 characters!");
                s_UserPassCheck++;
            }

            if (passWord.Length < 4)
            {
                Console.WriteLine(Environment.NewLine + "Password must have more than 4 characters!");
                s_UserPassCheck++;
            }
        }
    }
}
