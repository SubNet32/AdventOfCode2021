using AdventOfCode.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class FishCounter
    {
        public Int64[] count; 
        public static int newFishStartPoint = 8;
        public static int fishRepCylce = 6;

        public FishCounter()
        {
            count = new Int64[newFishStartPoint + 1];
        }

        public Int64 ExcecuteLifeCycle()
        {
            Int64 pos0Fish = count[0];
            for (int i = 0; i < count.Length - 1; i++)
            {
                count[i] = count[i + 1];
                count[i + 1] = 0;
            }
            count[fishRepCylce] += pos0Fish;
            count[newFishStartPoint] = pos0Fish;

            Int64 sum = 0;
            for(int i = 0; i < count.Length; i++)
            {
                sum += count[i];
            }

            PrintFish();
            Console.WriteLine("Current Sum: " + sum);

            return sum;
        }

        public void PrintFish()
        {
            for (int i = count.Length - 1; i >= 0; i--)
            {
                Console.WriteLine("Fish that will reproduce in " + i + " days: " + count[i]);
            }
            Console.WriteLine("");
        }
    }

    class Day6 : Day
    {
        public static int day = 6;
       
        public static int cycles = 256;

        public static void GetResult1()
        {
            string[] input = InputHandler.GetInputForDay(day)[0].Split(',');
            Console.WriteLine("Calc Result. Elements: " + input.Length);


            FishCounter fishCounter = new FishCounter();

            foreach (string s in input)
            {
                fishCounter.count[int.Parse(s)]++;
            }


            Int64 result = 0;
            for (int i = 0; i < cycles; i++)
            {
                result = fishCounter.ExcecuteLifeCycle();
            }


            PrintResult(day, result.ToString());
        }

     
      
    }
}
