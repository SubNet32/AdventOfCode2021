using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day05Content
{
    class Line
    {
        public Point start;
        public Point end;
        public bool isStraight;

        public Line(string input)
        {
            string[] seg = input.Split(" -> ");
            start = new Point(seg[0]);
            end = new Point(seg[1]);

            isStraight = (start.x == end.x || start.y == end.y);
        }

        public int GetMaxX()
        {
            return Math.Max(start.x, end.x);
        }
        public int GetMaxY()
        {
            return Math.Max(start.y, end.y);
        }

    }


    class Field
    {
        public int[,] counterField;
        public int width;
        public int height;

        public Field(int width, int height)
        {
            counterField = new int[width, height];
            this.width = width;
            this.height = height;
        }

        public void MarkLine(Line line)
        {
            Point currentPoint = line.start;
            MarkPointOnField(currentPoint);
            Console.WriteLine("Moving from " + line.start.ToString() + " --> " + line.end.ToString());
            while (!currentPoint.Equals(line.end))
            {
                currentPoint.Move(currentPoint.DifferenceTo(line.end).Normalize());
                Console.WriteLine(currentPoint.ToString());
                MarkPointOnField(currentPoint);
            }
        }

        public int MarkPointOnField(Point p)
        {
            return (counterField[p.x, p.y] += 1);
        }

        public void PrintField()
        {
            string s = "";
            for (int y = 0; y < height; y++)
            {
                s = "";
                for (int x = 0; x < width; x++)
                {
                    if (counterField[x, y] > 0)
                    {
                        s += counterField[x, y].ToString();
                    }
                    else
                    {
                        s += ".";
                    }
                }
                Console.WriteLine(s);
            }
        }

        public int GetFieldWithCountAtLeast2()
        {
            int result = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (counterField[x, y] > 1)
                    {
                        result++;
                    }
                }
            }
            return result;
        }
    }
}
