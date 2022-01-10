using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day13Content
{
    class Point
    {
        public int x;
        public int y;
        public string value;

        public Point(int x, int y, string value)
        {
            this.x = x;
            this.y = y;
            this.value = value;
        }

        public override string ToString()
        {
            return value+"(" + x + "|" + y + ")";
        }
        public string GetValueString()
        {
            return value;
        }

        public void Shift(int dx, int dy)
        {
            x += dx;
            y += dy;
        }

    }

     class FieldSize
    {
        public int startX;
        public int startY;
        public int width;
        public int height;

        public FieldSize(int startX, int startY, int width, int height)
        {
            this.startX = startX;
            this.startY = startY;
            this.width = width;
            this.height = height;
        }

        public void Normalize()
        {
            width += -(startX);
            startX = 0;
            height += -(startY);
            startY = 0;
        }

        public int GetRangeX()
        {
            return width - startX;
        }

        public int GetRangeY()
        {
            return height - startY;
        }

        public bool Contains(Point p)
        {
            return Contains(p.x, p.y);
        }
        public bool Contains(int x, int y)
        {
            return (x >= startX && x < width && y >= startY && y < height);
        }
    }

    class Field
    {
        public Point[,] field;

        public FieldSize size;

        public Field(List<Point> points)
        {
            size = new FieldSize(0,0,0,0);
            foreach (Point p in points)
            {
                size.startX = Math.Min(size.startX, p.x);
                size.startY = Math.Min(size.startY, p.y);
                size.width = Math.Max(size.width, p.x+1);
                size.height = Math.Max(size.height, p.y+1);
            }
            field = new Point[size.GetRangeX(),size.GetRangeY()];
            foreach (Point p in points)
            {
                p.Shift(-size.startX, -size.startY);
            }
            size.Normalize();
            for (int y = 0; y<size.height; y++)
            {
                for(int x = 0; x < size.width; x++)
                {
                    field[x, y] = new Point((x-size.startX),(y-size.startY),".");
                }
            }
            foreach (Point p in points)
            {
                field[p.x, p.y] = p;
            }
        }
        public void PrintField()
        {
            string s = "";
            for (int y = size.startY; y < size.height; y++)
            {
                s = "";
                for (int x = size.startX; x < size.width; x++)
                {
                    s += field[x, y].GetValueString();
                }
                Console.WriteLine(s);
            }
            Console.WriteLine("");
        }


        public Field FoldY(int foldY)
        {
            List<Point> newPoints = new List<Point>();
            for(int y = 0; y < size.height; y++)
            {
                for (int x = 0; x < size.width; x++)
                {
                    if (field[x, y].value == "#")
                    {
                        if (y < foldY)
                        {
                            newPoints.Add(field[x, y]);
                        }
                        else if (y > foldY)
                        {
                            int newY = foldY - (y - foldY);
                            newPoints.Add(new Point(x, newY, "#"));
                        }
                    }
                }
            }
            return new Field(newPoints);
        }
        public Field FoldX(int foldX)
        {
            List<Point> newPoints = new List<Point>();
            for (int y = 0; y < size.height; y++)
            {
                for (int x = 0; x < size.width; x++)
                {
                    if (field[x, y].value == "#")
                    {
                        if (x < foldX)
                        {
                            newPoints.Add(field[x, y]);
                        }
                        else if (x > foldX)
                        {
                            int newX = foldX - (x - foldX);
                            newPoints.Add(new Point(newX, y, "#"));
                        }
                    }
                }
            }
            return new Field(newPoints);
        }

        public int GetPointCount()
        {
            int result = 0;
            for (int y = 0; y < size.height; y++)
            {
                for (int x = 0; x < size.width; x++)
                {
                    if (field[x, y].value == "#")
                    {
                        result++;
                    }
                }
            }
            return result;
        }


    }
}
