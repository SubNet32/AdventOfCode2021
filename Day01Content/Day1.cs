using AdventOfCode.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day1 : Day
    {
        public static int day = 1;

        public static int GetResult1()
        {
            int[] input = InputHandler.GetInputForDayInt(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);
            int counter = 0;
            for (int i = 0; i < input.Length - 3; i++)
            {
                int sum1 = 0;
                for(int a = 0; a < 3; a++)
                {
                    sum1 += input[i + a];
                }
                int sum2 = 0;
                for (int b = 0; b < 3; b++)
                {
                    sum2 += input[i + b + 1];
                }
                if (sum2 > sum1)
                    counter++;
            }

            PrintResult(day, counter.ToString());
            return counter;
        }
    }
}
