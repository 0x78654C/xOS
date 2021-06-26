using System;
using System.Collections.Generic;
using System.Text;

namespace xOS.FileSystem
{
    public class Users
    {
        Crypto.MEX mex = new Crypto.MEX("theX0SSYstemOp3RRa0R!");
        private string UsrFile;
        private int index = 0;
        
        
        public string E_PWD(string data)
        {
            return mex.Encrypt(data);
        }

        public string D_PWD(string data)
        {
            return mex.Decrypt(data); 
        }



    }
}
