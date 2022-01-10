using AdventOfCode.Basic;
using AdventOfCode.Day18Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day18 : Day
    {
        public static int day = 18;

        public static void GetResult1()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);


            int largest = 0;
            for(int i = 0; i < input.Length; i++)
            {
                for(int n = 0; n < input.Length; n++)
                {
                    if (i != n)
                    {
                        Pair pair = new Pair(input[i], 0);
                        pair.Add(new Pair(input[n], 0));
                        int result = pair.GetMagnitude();
                        if (result> largest)
                        {
                            largest = result;
                        }
                    }
                }
            }
            PrintResult(day, largest.ToString());
        }
    }
}
