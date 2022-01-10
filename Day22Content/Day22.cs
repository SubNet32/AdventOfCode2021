using AdventOfCode.Basic;
using AdventOfCode.Day22Content;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AdventOfCode
{
    class Day22 : Day
    {
        public static int day = 22;

        public static void GetResult1()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);

            Solver solver = new Solver(input);

            PrintResult(day, solver.Solve().ToString());
        }
    }
}
