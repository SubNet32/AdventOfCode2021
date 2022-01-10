using AdventOfCode.Basic;
using AdventOfCode.Day11Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day11 : Day
    {
        public static int day = 11;

        public static int cycles = 1000;

        public static void GetResult1()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);

            Field field = new Field(input);

            int flashSum = 0;
            for (int i = 0; i < cycles; i++)
            {
                Console.WriteLine("");
                Console.WriteLine("Cylce: " + i);
                int flashes = field.ProcessCycle();
                flashSum += flashes;
                Console.WriteLine("");
                
                if(flashes==100)
                {
                    PrintResult(day, (i+1).ToString());
                    return;
                }

                //Console.ReadKey();
                //Console.Clear();
            }

        }
    }
}
