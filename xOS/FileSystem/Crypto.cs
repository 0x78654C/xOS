using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace xOS.FileSystem
{
    public static class Crypto
    {

        public class MEX
        {
            #region Private/Protected Member Variables

            /// <summary>
            /// Decryptor
            /// 
            private readonly ICryptoTransform _decryptor;

            /// <summary>
            /// Encryptor
            /// 
            private readonly ICryptoTransform _encryptor;

            /// <summary>
            /// 16-byte Private Key
            /// 
            private static readonly byte[] IV = Encoding.UTF8.GetBytes("Asi@15Uy1ps2$ebt");

            /// <summary>
            /// Public Key
            /// 
            private readonly byte[] _password;

            /// <summary>
            /// Rijndael cipher algorithm
            /// 
            private readonly RijndaelManaged _cipher;

            #endregion

            #region Private/Protected Properties

            private ICryptoTransform Decryptor { get { return _decryptor; } }
            private ICryptoTransform Encryptor { get { return _encryptor; } }

            #endregion

            #region Private/Protected Methods
            #endregion

            #region Constructor

            /// <summary>
            /// Constructor
            /// 
            /// <param name="password">Public key
            public MEX(string password)
            {
                //Encode digest
                var md5 = new MD5CryptoServiceProvider();
                _password = md5.ComputeHash(Encoding.ASCII.GetBytes(password));

                //Initialize objects
                _cipher = new RijndaelManaged();
                _decryptor = _cipher.CreateDecryptor(_password, IV);
                _encryptor = _cipher.CreateEncryptor(_password, IV);

            }

            #endregion

            #region Public Properties
            #endregion

            #region Public Methods

            /// <summary>
            /// Decryptor
            /// 
            /// <param name="text">Base64 string to be decrypted
            /// <returns>
            public string Decrypt(string text)
            {
                try
                {
                    byte[] input = Convert.FromBase64String(text);

                    var newClearData = Decryptor.TransformFinalBlock(input, 0, input.Length);
                    return Encoding.ASCII.GetString(newClearData);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine("inputCount uses an invalid value or inputBuffer has an invalid offset length. " + ae);
                    return null;
                }
                catch (ObjectDisposedException oe)
                {
                    Console.WriteLine("The object has already been disposed." + oe);
                    return null;
                }


            }

            /// <summary>
            /// Encryptor
            /// 
            /// <param name="text">String to be encrypted
            /// <returns>
            public string Encrypt(string text)
            {
                try
                {
                    var buffer = Encoding.ASCII.GetBytes(text);
                    return Convert.ToBase64String(Encryptor.TransformFinalBlock(buffer, 0, buffer.Length));
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine("inputCount uses an invalid value or inputBuffer has an invalid offset length. " + ae);
                    return null;
                }
                catch (ObjectDisposedException oe)
                {
                    Console.WriteLine("The object has already been disposed." + oe);
                    return null;
                }

            }

            #endregion
        }
        }
    }
