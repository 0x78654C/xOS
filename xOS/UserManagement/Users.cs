using System;
using System.Collections.Generic;
using System.Text;

namespace xOS.FileSystem
{
    public static class Users
    {
       
        private static string UsrFile;
        private static  int index = 0;
        
        
        public static string E_PWD(string data)
        {
            return Cryptography.Encrypt(data,"TestPass@!#!!@#12");
        }

        public static string D_PWD(string data)
        {
            return Cryptography.Decrypt(data, "TestPass@!#!!@#12");
        }

    }
}
