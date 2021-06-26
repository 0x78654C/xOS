﻿using System;
using System.Collections.Generic;
using System.Text;
using xOS.FileSystem;

namespace xOS.Commnads
{
    public static class SytemCMD
    {
        public static void RunSysCMD(string input)
        {

            //list file and folders
            if (input.StartsWith("ls"))
            {
                if (input.Length == 2)
                {
                    Root.Test_Root();
                }
                else
                {
                    input = input.Split(' ')[1];
                    Root.Test_Root(input);
                }
            }

            //clear console
            if (input == "clear")
            {
                Console.Clear();
                Console.WriteLine("--------------------Welcome to xOS----------------------");
            }
        }
    }
}