using AdventOfCode.Basic;
using AdventOfCode.Day17Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day17 : Day
    {
        public static int day = 17;

        public static void GetResult1()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);

            ProbeSolver solver = new ProbeSolver(input[0]);


            PrintResult(day, solver.FindHighestTrajectory().ToString());
        }

    }
}
