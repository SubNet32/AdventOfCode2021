using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AdventOfCode.Day17Content
{
    class Probe
    {
        public Point startPos;
        public Point startVel;
        public Point pos;
        public Point vel;
        public Point acc = new Point(-1,-1);
        public Target target;
        public List<Point> trajectoryPoints;
        public int maxY;

        public bool targetHit;
        public bool targetMissed;

        public Probe(Point velocity, Target target)
        {
            this.startVel = velocity;
            this.startPos = Point.Empty;
            this.target = target;
            Init();
        }

        public void Init()
        {
            this.pos = startPos;
            this.vel = startVel;
            this.trajectoryPoints = new List<Point>();
            this.maxY = 0;
        }

        private void Move()
        {
            pos.Offset(vel);
            vel.Offset(acc);
            if (vel.X < 0)
                vel.X = 0;

            if(target.Contains(pos))
            {
                targetHit = true;
            }
            else if(pos.X > target.end.X || pos.Y < target.end.Y || pos.X < 0)
            {
                targetMissed = true;
            }
        }

        public bool SimulateTrajectory(bool log)
        {
            trajectoryPoints = new List<Point>();
            if (log) 
            {
                Console.WriteLine("Simulating Trajectory for Probe with StartVel " + vel.ToString());
            }
            while (!targetHit && !targetMissed)
            {
                if(trajectoryPoints.Count==0)
                {
                    maxY = pos.Y;
                }
                trajectoryPoints.Add(pos);
                Move();
                maxY = Math.Max(pos.Y, maxY);
                if (log)
                {
                    Console.WriteLine("-->" + pos.ToString());
                }
                if(targetHit)
                {
                    if (log)
                    {
                        Console.WriteLine("Hit target at " + pos.ToString());
                    }
                    trajectoryPoints.Add(pos);
                }
                else if(targetMissed)
                {
                    if (log)
                    {
                        Console.WriteLine("Missed target at " + pos.ToString());
                    }
                    trajectoryPoints.Add(pos);
                }
            }
            return targetHit;
        }

        public List<Point> GetAllTrajectoryPoints()
        {
            return trajectoryPoints;
        }

        public bool TrajectoryContainsPoint(Point p)
        {
            if(targetHit || targetMissed)
            {
                foreach(Point tPoint in trajectoryPoints)
                {
                    if(tPoint.X == p.X && tPoint.Y == p.Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }

    class Target
    {
        public Point start;
        public Point end;

        public Target(int x1, int y1, int x2, int y2)
        {
            this.start = new Point(Math.Min(x1,x2), Math.Max(y1,y2));
            this.end = new Point(Math.Max(x1, x2), Math.Min(y1, y2));
        }

        public bool Contains(Point p)
        {
            return Contains(p.X, p.Y);
        }
        public bool Contains(int x, int y)
        {
            return (x >= start.X && x <= end.X) && (y >= end.Y && y <= start.Y);
        }

        public override string ToString()
        {
            return "{x1: " + start.X + " y1: " + start.Y + " x2: " + end.X + " y2: " + end.Y + "}";
        }
    }

    class ProbeSolver
    {
        public static bool debug=true;
        Target target;
        List<int> possibleX;

        public ProbeSolver(string input)
        {
            string[] s = input.Replace("target area: ", "").Replace("x=","").Replace("y=","").Split(", ");
            string[] xs = s[0].Split("..");
            string[] ys = s[1].Split("..");
            int x1 = int.Parse(xs[0]);
            int x2 = int.Parse(xs[1]);
            int y1 = int.Parse(ys[0]);
            int y2 = int.Parse(ys[1]);

            target = new Target(x1, y1, x2, y2);
            Log("Created target " + target.ToString());

            possibleX = new List<int>();
        }

        public static void Log(string s)
        {
            if (debug)
                Console.WriteLine(s);
        }

        public void FindPossibleX()
        {
            Log("Finding possible X Start Velocities");
            Log("Test " + target.Contains(21, -6));
            int start = target.end.X;
            while(start>0)
            {
                int x = 0;
                for(int i = 0; i <= start; i++)
                {
                    x += (start - i);
                    if (target.Contains(x, target.start.Y))
                    {
                        Log("-->Found startVel: " + start+" hits after "+(i+1)+" steps at posX: "+x);
                        possibleX.Add(start);
                        break;
                    }
                }
                start--;
            }
            possibleX.Reverse();
            Log("done");
            Log("");

        }

        public int FindHighestTrajectory()
        {
            FindPossibleX();
            Probe bestProbe=null;
            int count = 0;
            foreach(int x in possibleX)
            {
                Log("----------------------------------------------");
                Log("Trying for x "+x);
                int y = target.end.Y;
                int missedCount = 0;
                while (true)
                {
                    Probe p = new Probe(new Point(x, y), target);
                    p.SimulateTrajectory(false);
                    if(p.targetMissed)
                    {
                        //Log("Probe " + p.startVel.ToString() + " missed");
                        missedCount++;
                        if(missedCount>1000)
                            break;
                    }
                    else
                    {
                        if (missedCount > 0)
                            Log("");
                        missedCount = 0;
                        Log("Probe " + p.startVel.ToString() + " hit. With MaxY: "+p.maxY);
                        count++;
                        if (bestProbe==null || bestProbe.maxY < p.maxY)
                        {
                            bestProbe = p;
                        }
                    }
                    y++;
                }
            }

            Log("");
            //PrintTrajectrory(bestProbe);

            return count;
        }

        public void PrintTrajectrory(Probe probe)
        {
            Console.WriteLine("");
            Console.WriteLine("Printing Probe Trajectory. StartVel " + probe.startVel.X + ":" + probe.startVel.Y);
            Console.WriteLine("MaxY: " + probe.maxY+"  EndX: "+target.end.X+"  EndY:" +target.end.Y);
            string s;
            for (int y = probe.maxY; y >= target.end.Y; y--)
            {
                s = "";
                for(int x = 0; x <= target.end.X; x++)
                {
                    Point p = new Point(x, y);
                    if(x==0 && y==0)
                    {
                        s += "S";
                    }
                    else if(probe.TrajectoryContainsPoint(p))
                    {
                        s += "#";
                    }
                    else if(target.Contains(p))
                    {
                        s += "T";
                    }
                    else
                    {
                        s += ".";
                    }
                }
                Console.WriteLine(s);
            }
            Console.WriteLine("Probe: " + probe.startVel.ToString()+  " Target " + (probe.targetHit ? "hit!" : "missed"));
            Console.WriteLine("");

        }


    }
}
