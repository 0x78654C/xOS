using System;

namespace xOS.UI
{
    public class UIColor
    {
        /// <summary>
        /// Change color of a specific line in console. 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="text"></param>
        public static void ColorConsoleTextLine(ConsoleColor color, string text)
        {
            ConsoleColor currentForegrond = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = currentForegrond;
        }


        /// <summary>
        /// Change color of a specific text in console. 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="text"></param>
        public static void ColorConsoleText(ConsoleColor color, string text)
        {
            ConsoleColor currentForegrond = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = currentForegrond;
        }


        /// <summary>
        /// Display error message in red. 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="text"></param>
        public static void ErrorConsoleTextLine(string text)
        {
            ConsoleColor currentForegrond = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Error: {text}");
            Console.ForegroundColor = currentForegrond;
        }
    }
}
