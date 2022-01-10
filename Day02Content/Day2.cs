using AdventOfCode.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day2 : Day
    {
        public static int day = 2;

        public static void GetResult1()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);

            int posX = 0;
            int depth = 0;

            foreach(string s in input)
            {
                string[] ss = s.Split(' ');
                string cmd = ss[0];
                int value = int.Parse(ss[1]);
                if (cmd == "forward")
                {
                    posX += value;
                }
                else if (cmd == "down")
                {
                    depth += value;
                }
                else if (cmd == "up")
                {
                    depth = Math.Max(depth-value,0);
                }
                else
                {
                    Console.WriteLine("cmd '" + cmd + "' is unknown");
                    return;
                }
            }

            Console.WriteLine("posX: " + posX);
            Console.WriteLine("depth: " + depth);
            PrintResult(day, Convert.ToString(posX * depth));
        }

        public static void GetResult2()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);

            int posX = 0;
            int depth = 0;
            int aim = 0;

            foreach (string s in input)
            {
                string[] ss = s.Split(' ');
                string cmd = ss[0];
                int value = int.Parse(ss[1]);
                if (cmd == "forward")
                {
                    posX += value;
                    depth = Math.Max(depth + (aim * value),0);
                }
                else if (cmd == "down")
                {
                    aim += value;
                }
                else if (cmd == "up")
                {
                    aim -= value;
                }
                else
                {
                    Console.WriteLine("cmd '" + cmd + "' is unknown");
                    return;
                }
            }

            Console.WriteLine("posX: " + posX);
            Console.WriteLine("depth: " + depth);
            PrintResult(day, Convert.ToString(posX * depth));
        }
    }
}
