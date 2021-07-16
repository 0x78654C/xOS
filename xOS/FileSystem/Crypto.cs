using System;
using System.Text;

namespace xOS.FileSystem
{
    public static class Cryptography
    {
        public static string Encrypt(string input)
        {
            byte xorConstant = 0x53;
            string output;
           
            byte[] data = Encoding.UTF8.GetBytes(input);
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(data[i] ^ xorConstant);
            }
            output = Convert.ToBase64String(data);
            return output;
        }


        public static string Decrypt(string input)
        {
            byte xorConstant = 0x53;
            byte[] data = Convert.FromBase64String(input);
            string plainText;
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(data[i] ^ xorConstant);
            }
            plainText = Encoding.UTF8.GetString(data);
            return plainText;
        }
    }
}
