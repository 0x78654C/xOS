using System;
using System.IO;
using System.Text;
using xOS.Core;
using Sys = Cosmos.System;

namespace xOS.FileSystem
{
    public class FileManagement
    {
        /*File Management class*/

        private static readonly string s_CurrentDirectory = GlobalVariables.CurrentLocationFile; //current directory location


        /// <summary>
        /// Creates a simple file. Command: mf 
        /// </summary>
        /// <param name="fileName">Specify a file name</param>
        public static void CreateFile(string fileName)
        {
            try
            {
                string cDir = File.ReadAllText(s_CurrentDirectory);
                fileName = fileName.Split(' ')[1];
                fileName = !string.IsNullOrEmpty(cDir) && fileName.Contains(@"0:\") ? fileName : cDir + @"\" + fileName;
                Sys.FileSystem.VFS.VFSManager.CreateFile(fileName);
                Console.WriteLine($"File {fileName} was created!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Deletes a specific file. Command: rf
        /// </summary>
        /// <param name="fileName">Specify a file name</param>
        public static void DeleteFile(string fileName)
        {
            try
            {
                string trueMessage = $"File {fileName} was deleted!";
                string falseMessage = $"File {fileName} was not deleted!";
                fileName = fileName.Split(' ')[1];
                string cDir = File.ReadAllText(s_CurrentDirectory);
                fileName = !string.IsNullOrEmpty(cDir) && fileName.Contains(@"0:\") ? fileName : cDir + @"\" + fileName;
                File.Delete(fileName);
                CheckFile(fileName, trueMessage, falseMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Reading Data from a file. Command: df
        /// Encoding: UTF8
        /// </summary>
        /// <param name="fileName">Specify a file name</param>
        public static void ReadFile(string fileName)
        {
            try
            {
                fileName = fileName.Split(' ')[1];
                string cDir = File.ReadAllText(s_CurrentDirectory);
                fileName = !string.IsNullOrEmpty(cDir) && fileName.Contains(@"0:\") ? fileName : cDir + @"\" + fileName;

                if (File.Exists(fileName))
                {
                    var hello_file = Sys.FileSystem.VFS.VFSManager.GetFile(fileName);
                    var hello_file_stream = hello_file.GetFileStream();

                    if (hello_file_stream.CanRead)
                    {
                        byte[] text_to_read = File.ReadAllBytes(fileName);
                        Console.WriteLine(Encoding.UTF8.GetString(text_to_read));
                        return;
                    }

                    Console.WriteLine($"File {fileName} cannot be read!");
                    return;
                }

                Console.WriteLine($"File {fileName} does not exit!");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Write data to file with overwrite. Command: wf
        /// At the moment only ASCII
        /// </summary>
        /// <param name="fileName">Specify a file name</param>
        public static void WriteToFile(string fileName)
        {
            try
            {
                string Fn = fileName.Split(' ')[1];
                string cDir = File.ReadAllText(s_CurrentDirectory);
                @Fn = !string.IsNullOrEmpty(cDir) && fileName.Contains(@"0:\") ? @Fn : cDir + @"\" + @Fn;
                Write(@Fn, false);  
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }



        /// <summary>
        /// Appends data to a file. Command: af
        /// </summary>
        /// <param name="fileName">Specify a file name</param>
        public static void AppendToFile(string fileName)
        {
            try
            {
                string Fn = fileName.Split(' ')[1];
                string cDir = File.ReadAllText(s_CurrentDirectory);
                @Fn = !string.IsNullOrEmpty(cDir) && fileName.Contains(@"0:\") ? @Fn : cDir + @"\" + @Fn;
                Write(@Fn, true);
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }



        /// <summary>
        /// Copy file to a certain path. Command: fcopy
        /// </summary>
        /// <param name="data">source and destination</param>
        public static void CopyFile(string data)
        {
            try
            {
                string source = data.Split(' ')[1];
                string destination = data.Split(' ')[2];
                string cDir = File.ReadAllText(s_CurrentDirectory);
                string sPath = Parsing.ParseFilePath(source);
                string dPath = Parsing.ParseFilePath(destination);

                if ((sPath != cDir) && (dPath != cDir)) //path of destination and source file is not equal to current dir
                {
                    Copy(source, destination, false);
                }
                else if ((sPath == cDir) && (dPath != cDir)) //path of source equals the current directory but destination does not
                {
                    source = cDir + @"\" + source;
                    Copy(source, destination, false);
                }
                else if ((sPath != cDir) && (dPath == cDir)) //path of destination equals the current directory but source does not
                {
                    destination = cDir + @"\" + destination;
                    Copy(source, destination, false);
                }
                else if ((sPath == cDir) && (dPath == cDir)) //both source and destination equals to current path
                {
                    source = cDir + @"\" + source;
                    destination = cDir + @"\" + destination;
                    Copy(source, destination, false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Move file to a certain path. Command fmove
        /// </summary>
        /// <param name="data">source and destination</param>
        public static void MoveFile(string data)
        {
            try
            {
                string source = data.Split(' ')[1];
                string destination = data.Split(' ')[2];
                string cDir = File.ReadAllText(s_CurrentDirectory);
                string sPath = Parsing.ParseFilePath(source);
                string dPath = Parsing.ParseFilePath(destination);

                if ((sPath != cDir) && (dPath != cDir)) //path of destination and source file is not equal to current dir
                {
                    Copy(source, destination, true);
                }
                else if ((sPath == cDir) && (dPath != cDir)) //path of source equals the current directory but destination does not
                {
                    source = cDir + @"\" + source;
                    Copy(source, destination, true);
                }
                else if ((sPath != cDir) && (dPath == cDir)) //path of destination equals the current directory but source does not
                {
                    destination = cDir + @"\" + destination;
                    Copy(source, destination, true);
                }
                else if ((sPath == cDir) && (dPath == cDir)) //both source and destination equals to current path
                {
                    source = cDir + @"\" + source;
                    destination = cDir + @"\" + destination;
                    Copy(source, destination, true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // File check if exists
        private static void CheckFile(string fileName, string msgTrue, string msgFalse)
        {
            if (File.Exists(fileName))
            {
                Console.WriteLine(msgTrue);
            }
            else
            {
                Console.WriteLine(msgFalse);
            }
        }


        // Copy/Move
        private static void Copy(string source, string destination, bool move)
        {
            if (!File.Exists(destination) && File.Exists(source))
            {
                if (!move)
                {
                    File.Copy(source, destination);
                    Console.WriteLine($"File {source} copied to {destination}");
                    return;
                }
                File.Copy(source, destination);
                if (!File.Exists(destination))
                {
                    Console.WriteLine($"File {source} could not be moved!");
                    return;
                }

                File.Delete(source);
                Console.WriteLine($"File {source} moved to {destination}");
                return;
            }
            Console.WriteLine($"File {destination} already exists or source file does not exist!");
            return;
        }

        // Write/Append file 
        private static void Write(string fileName, bool append)
        {
            Console.Write("Type: ");
            string Data = Console.ReadLine();
            byte[] dataBytes = Encoding.ASCII.GetBytes(Data);
            Console.WriteLine(Data);
            var hello_file = Sys.FileSystem.VFS.VFSManager.GetFile(fileName);
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"File {fileName} does not exist!");
                return;
            }

            var hello_file_stream = hello_file.GetFileStream();

            if (hello_file_stream.CanWrite)
            {
                if (!append)
                {
                    hello_file_stream.Write(dataBytes, 0, dataBytes.Length);
                    return;
                }
                var read_file = File.ReadAllText(fileName);
                File.WriteAllText(fileName, read_file + Data);
                return;
            }
        }
    }
}
