using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Linq;

namespace AdventOfCode.Day19Content
{
    class Solver
    {

        public List<Scanner> scanner;

        public Solver()
        {
            scanner = new List<Scanner>();
        }

        public void AddInput(string s)
        {
            if(s.StartsWith("---"))
            {
                scanner.Add(new Scanner(scanner.Count));
                Utilities.Log("Adding new Scanner " + s);
            }
            else if(string.IsNullOrEmpty(s))
            {
                return;
            }
            else
            {
                scanner.Last().AddBeacon(s);
                Utilities.Log("Adding new Beacon to scanner: " + s);
            }
        }

        public int Solve()
        {
            scanner[0].foundOrientation = true;
            while(scanner.Any(s => !s.foundOrientation))
            {
                for (int i = 0; i < scanner.Count; i++)
                {
                    for (int c = 0; c < scanner.Count; c++)
                    {
                        if (i != c && (scanner[i].foundOrientation) && !scanner[c].foundOrientation)
                        {
                            scanner[i].FindOrientation(scanner[c]);
                        }
                    }
                }
            }
            List<Beacon> beacons = new List<Beacon>();
            foreach(Scanner sc in scanner)
            {
                Utilities.Log("Scanner"+sc.index+" " + sc.orientation.ToString());
                //sc.PrintBeacons();
            }



            Utilities.Log("");
            Utilities.Log("Beacons");
            foreach (Scanner sc in scanner)
            {
                List<Beacon> subList = sc.GetBeacons();
                foreach (Beacon beacon in subList)
                {
                    if (!beacons.Any(b => b.position == beacon.position))
                    {
                        beacons.Add(beacon);
                        Utilities.Log(beacon.position.ToString());
                    }
                }
            }

            foreach(Beacon b in beacons)
            {
                if(beacons.FindAll(a => a.position==b.position).Count>1)
                {
                    Utilities.Log("Error. " + b.ToString() + " was duplicate");
                }
            }

            Console.WriteLine("");
            Console.WriteLine("---------------------- END -------------------------");
            Console.WriteLine("");
            Console.WriteLine("Found " + beacons.Count + " beacons");

            int maxManhattenDist = 0;
            Vector3 biggest1 = Vector3.Zero;
            Vector3 biggest2 = Vector3.Zero;
            foreach (Scanner s1 in scanner)
            {
                foreach(Scanner s2 in scanner)
                {
                    if (s1 != s2)
                    {
                        int dist = s1.ManhattenDistTo(s2);
                        if(dist > maxManhattenDist)
                        {
                            maxManhattenDist = dist;
                            biggest1 = s1.orientation.position;
                            biggest2 = s2.orientation.position;
                        }
                    }
                }
            }
            Console.WriteLine("Biggest distance beween " + biggest1.ToString() + " and " + biggest2.ToString());
            return maxManhattenDist;

        }

    }
}
