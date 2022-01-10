using AdventOfCode.Basic;
using AdventOfCode.Day19Content;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AdventOfCode
{
    class Day19 : Day
    {
        public static int day = 19;

        public static void GetResult1()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);

            Solver solver = new Solver();
            foreach(string s in input)
            {
                solver.AddInput(s);
            }


            //88	-113	-1104

            //Vector3 test = Orientation.RotateVector(new Vector3(88, 113, - 1104), new Vector3(0, 0 , 180));
            //Console.WriteLine("");
            //Console.WriteLine("Test:" +test.ToString());

            PrintResult(day, solver.Solve().ToString());
        }
    }
}
