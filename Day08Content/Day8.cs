using AdventOfCode.Basic;
using AdventOfCode.Day08Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
  

    class Day8 : Day
    {
        public static int day = 8;

        public static void GetResult1()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);

            List<Entry> entries = new List<Entry>();

            long sum = 0;
            foreach(string s in input)
            {
                Entry e = new Entry(s);
                entries.Add(e);
                sum += e.value;
            }

            PrintResult(day, sum.ToString());
        }
    }
}
