using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day09Content
{
    class Point
    {
        public int x;
        public int y;
        public int value;

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

        public override string ToString()
        {
            return value+"(" + x + "|" + y + ")";
        }
    }

    class Basin
    {
        public Point centre;
        public List<Point> locations;

        public Basin(Point p)
        {
            centre = p;
            locations = new List<Point>();
        }

        public bool AddPoint(Point p)
        {
            if (!locations.Contains(p))
            {
                locations.Add(p);
                return true;
            }
            return false;
        }

        public void PrintBasin()
        {
            Console.WriteLine("");
            Console.WriteLine("---------------------");
            Console.WriteLine(GetBasinInfo());
            foreach(Point p in locations)
            {
                Console.WriteLine(p.ToString());
            }
        }

        public string GetBasinInfo()
        {
           return("Basin "+centre.ToString() + "   size: " + locations.Count);
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

        public List<Basin> basins;
        public List<Basin> largestBasins;
        public FieldSize size;

        public Field(string[] input)
        {
            size = new FieldSize(input[0].Length, input.Length);
            field = new Point[size.width, size.height];

            for(int y = 0; y<input.Length; y++)
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
                        s += field[x, y].ToString();
                    }
                    else
                    {
                        s += ".";
                    }
                }
                Console.WriteLine(s);
            }
        }

        public int GetLowPointRisk()
        {
            basins = new List<Basin>();
            int risk = 0;
            for (int y = 0; y < size.height; y++)
            {
                for (int x = 0; x < size.width; x++)
                {
                    if(IsLowPoint(field[x,y]))
                    {
                        Console.WriteLine("Found Low Point (" + field[x, y] + ") at " + x + "|" + y);
                        risk += 1 + field[x, y].value;
                        basins.Add(new Basin(field[x,y]));
                    }
                }
            }
            return risk;
        }

        public bool IsLowPoint(Point p)
        {
            List<Point> adjPoints = GetAdjacentPoints(p);
            foreach(Point adjP in adjPoints)
            {
                if (adjP != null && p.value >= adjP.value)
                    return false;
            }
            return true;
        }

        public int CalcAllBasins()
        {
            if (basins == null)
                throw new Exception("basins not init");

            largestBasins = new List<Basin>();
            Console.WriteLine("Calculating Basins. Count: "+basins.Count);
            foreach(Basin basin in basins)
            {
                AddAdjacentFieldsToBasin(basin, basin.centre);
                basin.PrintBasin();
                CheckForLargestBasin(basin);
            }

            int result = 1;
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Largest Basins:");
            foreach(Basin basin in largestBasins)
            {
                basin.PrintBasin();
                result = result * basin.locations.Count;
            }

            return result;
        }

        public void CheckForLargestBasin(Basin checkBasin)
        {
            Console.WriteLine("Checking for largest 3 Basins. Input: " + checkBasin.GetBasinInfo());
            if (largestBasins.Contains(checkBasin))
                return;
            if(largestBasins.Count<3)
            {
                largestBasins.Add(checkBasin);
                Console.WriteLine("Count < 3 --> Adding Basin to largestBasins " + checkBasin.centre.ToString());
            }
            else
            {
                Basin lowestBasin = null;
                foreach(Basin basin in largestBasins)
                {
                    if(lowestBasin==null || lowestBasin.locations.Count > basin.locations.Count)
                    {
                        lowestBasin = basin;
                    }
                }

                Console.WriteLine("Found Basin with lowest Value " + lowestBasin.GetBasinInfo());

                if (checkBasin.locations.Count > lowestBasin.locations.Count)
                {
                    Console.WriteLine("Removing Basin from largestBasins " + lowestBasin.centre.ToString());
                    Console.WriteLine("Adding Basin to largestBasins " + checkBasin.centre.ToString());
                    largestBasins.Remove(lowestBasin);
                    largestBasins.Add(checkBasin);
                }
            }
        }

        public void AddAdjacentFieldsToBasin(Basin basin, Point p)
        {
            if (p == null || p.value == 9)
                return;

            if (basin.AddPoint(p))
            {
                List<Point> nextPoints = GetAdjacentPoints(p);

                foreach (Point nP in nextPoints)
                {
                    if (nP != null)
                    {
                        AddAdjacentFieldsToBasin(basin, nP);
                    }
                }
            }
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
    }
}
