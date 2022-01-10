using AdventOfCode.Basic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using AdventOfCode.Day05Content;

namespace AdventOfCode
{
    class Day5 : Day
    {
        public static int day = 5;

        public static void GetResult1()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);

            List<Line> lines = new List<Line>();

            int maxX=0, maxY=0;

            foreach(string s in input)
            {
                Line l = new Line(s);
                if (l.isStraight || true)
                {
                    lines.Add(l);
                    maxX = Math.Max(maxX, l.GetMaxX() + 1);
                    maxY = Math.Max(maxY, l.GetMaxY() + 1);
                }
            }

            Console.WriteLine("Loaded " + lines.Count + " lines.");

            Field field = new Field(maxX, maxY);
            foreach(Line l in lines)
            {
                field.MarkLine(l);
            }


            PrintResult(day, field.GetFieldWithCountAtLeast2().ToString());
        }
    }
}
