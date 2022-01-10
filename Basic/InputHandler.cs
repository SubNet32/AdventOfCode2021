using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Basic
{
    public static class InputHandler
    {
        private static string path = "../InputFiles/day";
        public static string[] GetInputForDay(int day)
        {
            string[] input = System.IO.File.ReadAllLines(path+day+".txt");
            Console.WriteLine("Loaded " + input.Length + " items from inputFile");
            return input;
        }

        public static int[] GetInputForDayInt(int day)
        {
            return Array.ConvertAll(GetInputForDay(day), s => Convert.ToInt32(s));
        }
    }
}
