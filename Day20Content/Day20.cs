using AdventOfCode.Basic;
using AdventOfCode.Day20Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day20 : Day
    {
        public static int day = 20;

        public static void GetResult1()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);

            Solver solver = new Solver(input);


            PrintResult(day, "");
        }
    }
}
