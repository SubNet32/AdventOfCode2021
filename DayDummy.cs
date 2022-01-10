using AdventOfCode.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class DayDummy : Day
    {
        public static int day = 0;

        public static void GetResult1()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);

          


            PrintResult(day, "");
        }
    }
}
