using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day05Content
{
    class Point
    {
        public int x;
        public int y;

        public Point(string input)
        {
            string[] seg = input.Split(',');
            this.x = int.Parse(seg[0]);
            this.y = int.Parse(seg[1]);
        }
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Point() { }

        public bool Equals(Point p)
        {
            return p.x == x && p.y == y;
        }

        public Point DifferenceTo(Point p)
        {
            return new Point(p.x - this.x, p.y - this.y);
        }

        public static Point Normalize(Point p)
        {
            return new Point(Utilities.NormalizeInt(p.x), Utilities.NormalizeInt(p.y));
        }

        public Point Normalize()
        {
            return new Point(Utilities.NormalizeInt(this.x), Utilities.NormalizeInt(this.y));
        }

        public void Move(Point move)
        {
            this.x += move.x;
            this.y += move.y;
        }

        public override string ToString()
        {
            return "[" + this.x + "|" + this.y + "]";
        }

    }

}
