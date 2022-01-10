using AdventOfCode.Day13Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day13 : Day
    {
        public static int day = 13;

        public static void GetResult1()
        {
            string[] input = AdventOfCode.Basic.InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);

            List<Point> points = new List<Point>();
            List<string> instructions = new List<string>();

            foreach(string s in input)
            {
                if(s.Length>0)
                {
                    if(s.StartsWith("fold along"))
                    {
                        instructions.Add(s.Replace("fold along ",""));
                    }
                    else
                    {
                        string[] coordinates = s.Split(',');
                        points.Add(new Point(int.Parse(coordinates[0]), int.Parse(coordinates[1]),"#"));
                    }
                }
            }

            Field field = new Field(points);
            field.PrintField();
            Field newField = field; ;
            foreach (string s in instructions)
            {
                string[] iSplit = s.Split("=");
                if(iSplit[0]=="x")
                {
                    newField = newField.FoldX(int.Parse(iSplit[1]));
                    newField.PrintField();
                }
                else if (iSplit[0] == "y")
                {
                    newField = newField.FoldY(int.Parse(iSplit[1]));
                    newField.PrintField();
                }
            }


            PrintResult(day, newField.GetPointCount().ToString());
        }
    }
}
