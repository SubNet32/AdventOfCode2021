using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day11Content
{
    class Point
    {
        public int x;
        public int y;
        public int value;
        public bool hasFlashed;

        public Point(int x, int y, int value)
        {
            this.x = x;
            this.y = y;
            this.value = value;
        }

        public Point Left(Field field)
        {
            if (field.size.Contains(x - 1, y))
                return field.field[x-1, y];
            return null;
        }
        public Point Right(Field field)
        {
            if (field.size.Contains(x + 1, y))
                return field.field[x + 1, y];
            return null;
        }
        public Point Up(Field field)
        {
            if (field.size.Contains(x, y-1))
                return field.field[x, y-1];
            return null;
        }
        public Point Down(Field field)
        {
            if (field.size.Contains(x, y + 1))
                return field.field[x, y + 1];
            return null;
        }
        public Point GetPointRelativeTo(Field field, int dx, int dy)
        {
            if(field.size.Contains(this.x+dx, this.y+dy))
            {
                return field.field[x + dx, y + dy];
            }
            return null;
        }

        public List<Point> GetAllAdjacentPoints(Field field)
        {
            List<Point> adjPoints = new List<Point>();
            for(int x = -1; x <= 1; x++)
            {
                for(int y = -1; y <= 1; y++)
                {
                    if (x != 0 || y != 0)
                    {
                        Point p = GetPointRelativeTo(field, x, y);
                        if(p !=null)
                            adjPoints.Add(p);
                    }
                }
            }
            return adjPoints;
        }

        public void Update(Field field)
        {
            //Console.WriteLine("------------>Updating Point " + this.ToString()+ "  "+value+" -> "+(value+1));
            value++;
            if(value>9)
            {
                Flash(field);
            }
        }
        public void FlashUpdate(Field field)
        {
            //Console.WriteLine("-->FlashUpdate Point " + this.ToString() + "  " + value + " -> " + (value + 1));
            value++;
            if (value > 9)
            {
                Flash(field);
            }
        }

        public void Flash(Field field)
        {
            if(!hasFlashed)
            {
                //Console.WriteLine("------>Point " + this.ToString() + " flashed!!!");
                field.AddFlashedPoint(this);
                hasFlashed = true;
                List<Point> adjPoints = GetAllAdjacentPoints(field);
                foreach(Point p in adjPoints)
                {
                    p.FlashUpdate(field);
                }
            }
        }

        public override string ToString()
        {
            return value+"(" + x + "|" + y + ")";
        }
        public string GetValueString()
        {
            if (value > 9)
            {
                return "#";
            }
            return value.ToString();
        }

        public void ResetFlash()
        {
            if (!hasFlashed)
                throw new Exception("Tried to reset a non flashed point");
            hasFlashed = false;
            value = 0;
        }
    }

     class FieldSize
    {
        public int width;
        public int height;

        public FieldSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public bool Contains(Point p)
        {
            return (p.x >= 0 && p.x < width && p.y >= 0 && p.y < height);
        }
        public bool Contains(int x, int y)
        {
            return (x >= 0 && x < width && y >= 0 && y < height);
        }
    }

    class Field
    {
        public Point[,] field;

        public FieldSize size;

        public List<Point> flashedPoints;
        public Field(string[] input)
        {
            size = new FieldSize(input[0].Length, input.Length);
            field = new Point[size.width, size.height];
            flashedPoints = new List<Point>();

            for (int y = 0; y<input.Length; y++)
            {
                for(int x = 0; x < input[y].Length; x++)
                {
                    field[x, y] = new Point(x,y,int.Parse(input[y][x].ToString()));
                }
            }

            PrintField();
        }
        public void PrintField()
        {
            string s = "";
            for (int y = 0; y < size.height; y++)
            {
                s = "";
                for (int x = 0; x < size.width; x++)
                {
                    if (field[x, y].value > 0)
                    {
                        s += field[x, y].GetValueString();
                    }
                    else
                    {
                        s += ".";
                    }
                }
                Console.WriteLine(s);
            }
        }

        public void AddFlashedPoint(Point p)
        {
            flashedPoints.Add(p);
        }

        public List<Point> GetAdjacentPoints(Point p)
        {
            if (p == null)
                return null;
            List<Point> adjPoints = new List<Point>();
            adjPoints.Add(p.Left(this));
            adjPoints.Add(p.Right(this));
            adjPoints.Add(p.Up(this));
            adjPoints.Add(p.Down(this));
            return adjPoints;
        }

        public int ProcessCycle()
        {
            flashedPoints.Clear();
            PrintField();

            for (int y = 0; y < size.height; y++)
            {
                for (int x = 0; x < size.width; x++)
                {
                    field[x, y].Update(this);
                }
            }


            PrintField();


            foreach(Point p in flashedPoints)
            {
                p.ResetFlash();
            }
            Console.WriteLine("FlashCounter: " + flashedPoints.Count);
            return flashedPoints.Count;
        }
    }
}
