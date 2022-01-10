using AdventOfCode.Basic;
using AdventOfCode.Day12Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day12 : Day
    {
        public static int day = 12;

        public static void GetResult1()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);

            PathFinder pathFinder = new PathFinder();

            foreach(string s in input)
            {
                pathFinder.AddConnectionFromInput(s);
            }
            pathFinder.PrintNodes();
            int result = pathFinder.FindAllPaths();

            PrintResult(day, result.ToString());
        }
    }
}
