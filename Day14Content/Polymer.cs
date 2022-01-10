using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day14Content
{
    class Polymer
    {
        public string value;
        public long count;

        public Polymer(string value)
        {
            this.value = value;
            this.count = 0;
        }

        public void AddCount(long addCount)
        {
            count += addCount;
        }

    }

    class Pair
    {
        public Polymer p1;
        public Polymer p2;
        public string result1String;
        public Pair result1;
        public string result2String;
        public Pair result2;
        public long count;

        public bool marked;
        public long savedCount;

        public Pair(Polymer p1, Polymer p2, string result1, string result2)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.result1String = result1;
            this.result2String = result2;
            this.count = 0;
            this.marked = false;
            this.savedCount = 0;
        }

        public void AddResults(Pair result1, Pair result2)
        {
            this.result1 = result1;
            this.result2 = result2;
            PrintPair();
        }

        public string GetPairString()
        {
            return p1.value + p2.value;
        }

        public void PrintPair()
        {
            Console.WriteLine("Pair " + GetPairString() + " --> " + result1.GetPairString() + " & " + result2.GetPairString());
        }

        public void IncCount()
        {
            count++;
            Console.WriteLine("-->Inc count of " + GetPairString() + " to " + count);
        }

        public void IncCountDelayed(long amount)
        {
            this.marked = true;
            this.savedCount += amount;
            Console.WriteLine("----->to +" + amount + " " + GetPairString());
        }

        public void Evolve()
        {
            if(count>0)
            {
                Console.WriteLine("Evolving " + count + " " + GetPairString());
                result1.IncCountDelayed(count);
                result2.IncCountDelayed(count);
                count = 0;
            }
        }

        public void CopyNewCount()
        {
            if(marked)
            {
                marked = false;
                count = savedCount;
                savedCount = 0;
            }
        }

        public void ApplyCountToPolymores()
        {
            this.p1.AddCount(count);
            this.p2.AddCount(count);
        }
    }

    class PolymerSolver
    {
        List<Polymer> polymers;
        List<Pair> pairs;

        public PolymerSolver(string[] input)
        {
            polymers = new List<Polymer>();

            foreach(string s in input)
            {
                if (s.Contains(" -> "))
                {
                    string polymores = s.Replace(" -> ", "");
                    foreach (char c in polymores)
                    {
                        if (polymers.Find(p => p.value == c.ToString()) == null)
                        {
                            polymers.Add(new Polymer(c.ToString()));
                        }
                    }
                }
            }

            Console.WriteLine("Found " + polymers.Count + " possible polymores");

            pairs = new List<Pair>();
            
            foreach (string s in input)
            {
                if (s.Contains(" -> "))
                {
                    string parts = s.Replace(" -> ", "");
                    pairs.Add(new Pair(FindPoylmer(parts[0]), FindPoylmer(parts[1]), string.Concat(parts[0], parts[2]), string.Concat(parts[2], parts[1])));
                }
            }
            foreach(Pair p in pairs)
            {
                p.AddResults(pairs.Find(i => i.GetPairString() == p.result1String), pairs.Find(i => i.GetPairString() == p.result2String));
            }

        }

        public Polymer FindPoylmer(string value)
        {
            return polymers.Find(p => p.value == value);
        }
        public Polymer FindPoylmer(char value)
        {
            return FindPoylmer(value.ToString());
        }

        public void SetStartInput(string s)
        {
            Console.WriteLine("Setting start string: " + s);
            for(int i = 0; i < s.Length-1; i++)
            {
                pairs.Find(p => p.GetPairString() == s.Substring(i, 2)).IncCount();
            }
            PrintPairs();
        }

        public void DevelopePolymores()
        {
            foreach(Pair p in pairs)
            {
                p.Evolve();
            }

            foreach (Pair p in pairs)
            {
                p.CopyNewCount();
            }

            PrintPairs();
        }

        public long CalcPolyDifference()
        {

            Console.WriteLine("");
            Console.WriteLine("Counting polymore occurence ");

            Polymer mostCommonPolymore = null;
            Polymer leastCommonPolymore = null;

            foreach (Polymer p in polymers)
            {
                p.count = 0;
            }

            foreach (Pair p in pairs)
            {
                p.ApplyCountToPolymores();
            }
            foreach (Polymer p in polymers)
            {
                p.count = Convert.ToInt64(Math.Round(p.count / 2.0));
                Console.WriteLine("-->Polymore " + p.value + "  count: " + p.count);
                if (mostCommonPolymore == null || p.count > mostCommonPolymore.count)
                {
                    mostCommonPolymore = p;
                }
                if (leastCommonPolymore == null || p.count < leastCommonPolymore.count)
                {
                    leastCommonPolymore = p;
                }
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("-->Most ommon " + mostCommonPolymore.value + "  count: " + mostCommonPolymore.count);
            Console.WriteLine("-->Least ommon " + leastCommonPolymore.value + "  count: " + leastCommonPolymore.count);
            Console.WriteLine("-->Diff: " + (mostCommonPolymore.count - leastCommonPolymore.count));

            return mostCommonPolymore.count - leastCommonPolymore.count;
        }

        public void PrintPairs()
        {
            Console.WriteLine("");
            Console.WriteLine("Printing Pairs");

            string s = "-->";
            foreach(Pair p in pairs)
            {
                if(p.count>0)
                    Console.WriteLine(p.GetPairString() + " (" + p.count + ")");
            }
            Console.WriteLine("");
            Console.WriteLine("");
        }

    }
}
