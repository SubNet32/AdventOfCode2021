using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day15Content
{
    class Point
    {
        public int x;
        public int y;
        public int value;
        public int distTarget;
        public Point prevPoint;
        public bool isStartPoint;
        public bool isEndPoint;
        public bool isPartOfPath;
        public bool isClosed;

        public Point(int x, int y, int value)
        {
            this.x = x;
            this.y = y;
            this.value = value;
            this.prevPoint = null;
            this.isPartOfPath = false;
            this.isClosed = false;
        }

        public void CalcDistTarget(Point endpoint)
        {
            this.distTarget = Distance(endpoint);
        }

        public int Distance(Point p)
        {
            return Math.Abs(p.x - this.x) + Math.Abs(p.y - this.y);
        }

        public int GetMoveToValue()
        {
            if (isStartPoint)
                return 0;
            else
            {
                return GetPathRisk() +distTarget;
            }
        }

        public int GetPathRisk()
        {
            if (prevPoint == null)
            {
                return 0;
            }
            else
            {
                return value + prevPoint.GetPathRisk();
            }
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

        public List<Point> GetAdjacentPointsInclDiagonally(Field field)
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

        public List<Point> GetAdjacentPoints(Point p, Field field)
        {
            if (p == null)
                return null;
            List<Point> adjPoints = new List<Point>();
            adjPoints.Add(p.Left(field));
            adjPoints.Add(p.Right(field));
            adjPoints.Add(p.Up(field));
            adjPoints.Add(p.Down(field));
            return adjPoints;
        }

        public override string ToString()
        {
            return "(" + x + "|" + y + ") " +value +" risk: "+ GetMoveToValue();
        }

        public string GetValueString()
        {
            return value.ToString();
        }

        public bool Compare(Point p)
        {
            return p.x == this.x && p.y == this.y;
        }

    }

    class Path
    {
        public List<Point> openPoints;
        public Point currentPoint;
        public Field field;
        public bool complete;
        public Point endPoint;

        public Path(Point startPoint, Point endPoint, Field field)
        {
            openPoints = new List<Point>();
            startPoint.isStartPoint = true;
            SetCurrentPoint(startPoint);
            this.field = field;
            this.complete = false;
            this.endPoint = endPoint;
            endPoint.isEndPoint = true;
        }

        public int GetPathRisk()
        {
            int risk = 0;
            Point p = currentPoint;
            while(true)
            {
                if (p.isStartPoint)
                {
                    return risk;
                }
                else
                {
                    risk += p.value;
                    p = p.prevPoint;
                }
            }
        }

        public void PrintPath(bool debug)
        {
            List<Point> pathList = GetPathList();
            if(debug)
                field.PrintField(pathList, openPoints, null, currentPoint);
            else
                field.PrintField(pathList, null, null, null);

        }

        public List<Point> GetPathList()
        {
            List<Point> pathList = new List<Point>();
            Point p = currentPoint;
            while(true)
            {
                pathList.Add(p);
                if (p.prevPoint != null)
                {
                    p = p.prevPoint;
                }
                else
                {
                    return pathList;
                }
            }
        }

        public void SetCurrentPoint(Point p)
        {
            openPoints.Remove(p);
            p.isClosed = true;
            currentPoint = p;
            if (currentPoint.isEndPoint)
                complete = true;
        }

        public void CheckNextPoint()
        {
            if (complete)
                return;
            Utilities.Log("---------------------------------");
            Utilities.Log("Checking for next options");
            Utilities.Log("Current position: "+currentPoint.ToString());
            List<Point> nextPoints = currentPoint.GetAdjacentPoints(currentPoint, field);
            foreach (Point p in nextPoints)
            {
                if(p != null && !p.isClosed && !openPoints.Contains(p))
                {
                    p.prevPoint = currentPoint;
                    p.CalcDistTarget(endPoint);
                    openPoints.Add(p);
                }
            }
            Point bestOptionPoint = null;
            foreach(Point p in openPoints)
            {
                //Utilities.Log("Possible next points: " + p.ToString());
                if (p.isEndPoint)
                {
                    SetCurrentPoint(p);
                    return;
                }
                if(bestOptionPoint== null || bestOptionPoint.GetMoveToValue() > p.GetMoveToValue())
                {
                    bestOptionPoint = p;
                }
            }
            //Utilities.Log("Best option is " + bestOptionPoint.ToString());
            SetCurrentPoint(bestOptionPoint);
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

        public Field(string[] input)
        {
            Utilities.Log("");
            Utilities.Log("----------------------------------------------------");
            Utilities.Log("Create new Field");
            size = new FieldSize(input[0].Length, input.Length);
            field = new Point[size.width, size.height];

            for (int y = 0; y<input.Length; y++)
            {
                for(int x = 0; x < input[y].Length; x++)
                {
                    field[x, y] = new Point(x,y,int.Parse(input[y][x].ToString()));
                }
            }

            field[size.width - 1, size.height - 1].isEndPoint = true;

            PrintField();
        }

        public Field(string[] input, int expandInput)
        {
            Utilities.Log("");
            Utilities.Log("----------------------------------------------------");
            Utilities.Log("Create new Field");
            int baseWidth = input[0].Length;
            int baseHeight = input.Length;
            size = new FieldSize(baseWidth * expandInput, baseHeight * expandInput);
            field = new Point[size.width, size.height];

            for (int y = 0; y < baseHeight; y++)
            {
                for (int x = 0; x < baseWidth; x++)
                {
                    field[x, y] = new Point(x, y, int.Parse(input[y][x].ToString()));
                }
            }

            for(int y = 0; y < size.height; y++)
            {
                for (int x = 0; x < size.width; x++)
                {
                    if(y < baseHeight && x>= baseWidth)
                    {
                        int newValue = field[x % baseWidth, y].value + Convert.ToInt32(Math.Floor(Convert.ToSingle(x) / Convert.ToSingle(baseWidth)));
                        if (newValue > 9)
                            newValue -= 9;
                        field[x, y] = new Point(x, y, newValue);
                    }
                    else if(y >= baseHeight)
                    {
                        int newValue = field[x, y - baseHeight].value +1;
                        if (newValue > 9)
                            newValue -= 9;
                        field[x, y] = new Point(x, y, newValue);
                    }
                }
            }

            field[size.width - 1, size.height - 1].isEndPoint = true;

            PrintField(baseWidth, baseHeight);
        }

        public void PrintField()
        {
            PrintField(null,null,null, null);
        }
        public void PrintField(int xSep, int ySep)
        {
            string s = "";
            for (int y = 0; y < size.height; y++)
            {
                s = "";
                for (int x = 0; x < size.width; x++)
                {
                    if (x % xSep == 0 && x > 0)
                        s += " ";
                    s += "" + field[x, y].GetValueString() + "";
                }
                if (y % xSep == 0 && y > 0)
                    Utilities.Log("");
                Utilities.Log(s);
            }


        }

        public void PrintField(List<Point> pathList, List<Point> openPoints, List<Point> closedPoints, Point currentPoint)
        {
            string s = "";
            for (int y = 0; y < size.height; y++)
            {
                s = "";
                for (int x = 0; x < size.width; x++)
                {
                    if(currentPoint!=null && field[x,y]==currentPoint)
                    {
                        s += "<" + field[x, y].GetValueString() + ">";
                    }
                    else if(pathList!=null && pathList.Contains(field[x,y]))
                    { 
                        s += "[" + field[x, y].GetValueString() + "]";
                    }
                    else if(closedPoints != null && closedPoints.Contains(field[x, y]))
                    {
                        s += "   ";
                    }
                    else if (openPoints != null && openPoints.Contains(field[x, y]))
                    {
                        s += "(" + field[x, y].GetValueString() + ")";
                    }
                    else
                    {
                        s += " " + field[x, y].GetValueString() + " ";
                    }
                }
                Utilities.Log(s);
            }

            
        }

        public void SetPartOfPath(Point p)
        {
            this.field[p.x, p.y].isPartOfPath = true;
        }

        public int FindPath()
        {
            Path path = new Path(field[0, 0], field[size.width - 1, size.height - 1], this);
            bool searching = true;
            while(searching)
            {
                path.CheckNextPoint();
                if(path.complete)
                {
                    searching = false;
                    int risk = path.GetPathRisk();
                    path.PrintPath(false);
                    return risk;
                }

                //path.PrintPath(true);
                //Utilities.Log("");
                //Utilities.Log("");
                //Console.ReadKey();
            }
            return 0;
        }

    
    }
}
