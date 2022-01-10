using AdventOfCode.Basic;
using AdventOfCode.Day15Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day15 : Day
    {
        public static int day = 15;

        public static void GetResult1()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);

            Field field = new Field(input,5);
            

            PrintResult(day, field.FindPath().ToString());
        }
    }
}
