using AdventOfCode.Basic;
using AdventOfCode.Day09Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day9 : Day
    {
        public static int day = 9;

        public static void GetResult1()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);

            Field field = new Field(input);

            field.GetLowPointRisk();
            int result = field.CalcAllBasins();

            PrintResult(day, result.ToString());
        }
    }
}
