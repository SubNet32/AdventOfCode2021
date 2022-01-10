using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day18Content
{
    class Explosion
    {
        public int x;
        public int y;
        public bool explosion;

        public Explosion()
        {
            x = -1;
            y = -1;
            explosion = false;
        }
    }

    class Value
    {
        public bool isValue;
        public int value;
        public Pair pair;

        public Value(Value value)
        {
            this.isValue = value.isValue;
            this.value = value.value;
            if (value.pair != null)
            {
                this.pair = new Pair(value.pair.x, value.pair.y, value.pair.depth);
            }
        }

        public Value(int value)
        {
            this.isValue = true;
            this.value = value;
            this.pair = null;
        }

        public Value(Pair pair)
        {
            this.isValue = false;
            this.value = 0;
            this.pair = new Pair(pair.x, pair.y,pair.depth);
        }

        public void IncreaseDepth()
        {
            if (!isValue)
                pair.IncreaseDepth();
        }

        public Pair SplitToPair(int newDepth)
        {
            if (isValue)
            {
                return new Pair(Utilities.DivideByAndRoundDown(value,2), Utilities.DivideByAndRoundUp(value, 2), newDepth);
            }
            return null;
        }

        public int GetMagnitude(int multiplyer)
        {
            if(isValue)
            {
                return multiplyer * value;
            }
            else
            {
                return multiplyer * pair.GetMagnitude();
            }
        }

        public override string ToString()
        {
            if (this.isValue)
                return value.ToString();
            else
                return pair.ToString();
        }
    }

    class Pair
    {
        public Value x;
        public Value y;
        public int depth;
        public bool delete;

        public enum Direction
        {
            left,
            right
        }

        public Pair(Value x, Value y, int depth)
        {
            this.x = x;
            this.y = y;
            this.depth = depth;
        }

        public Pair(int x, int y, int depth)
        {
            this.x = new Value(x);
            this.y = new Value(y);
            this.depth = depth;
        }

        public Pair(string s, int depth)
        {
            string logModifyer = String.Concat(Enumerable.Repeat("--", depth)) + "("+depth+")>";
            Utilities.Log(logModifyer + "New Pair input:" + s);
            this.depth = depth;
            s = s.Substring(1, s.Length - 2); //remove start "[" and end "]"
            if(s[0] == '[')
            {
                string sx = GetPairSubString(s);
                x = new Value(new Pair(sx, depth+1));
                s = s.Remove(0, sx.Length+1);
                if (s[0] == '[')
                {
                    string sy = GetPairSubString(s);
                    y = new Value(new Pair(sy, depth + 1));
                }
                else
                {
                    y = new Value(int.Parse(s));
                }
            }
            else
            {
                string sx = s.Substring(0, s.IndexOf(","));
                x = new Value(int.Parse(sx));
                s = s.Substring(sx.Length+1);
                if(s[0] == '[')
                {
                    string sy = GetPairSubString(s);
                    y = new Value(new Pair(sy, depth + 1));
                }
                else
                {
                    y = new Value(int.Parse(s));
                }
            }
        }

        private string GetPairSubString(string s)
        {
            int countOpen = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '[')
                {
                    countOpen++;
                }
                else if (s[i] == ']')
                {
                    countOpen--;
                }
                if (countOpen == 0)
                {
                    return s.Substring(0, i+1);
                }
            }

            throw new Exception("FinPairSubString Error");
        }

        public void Add(Pair add)
        {
            Utilities.Log("Adding pair " + add.ToString());
            Value nx = new Value(this);
            Value ny = new Value(add);
            nx.IncreaseDepth();
            ny.IncreaseDepth();
            this.x = nx;
            this.y = ny;
            Utilities.Log("Result of Adding: " + this.ToString());

            bool checking = true;
            while(checking)
            {
                Explosion expRes = CheckExplosion();
                if (expRes != null && expRes.explosion)
                {
                    Utilities.Log("Result after Explosion: " + this.ToString());
                }
                else
                {
                    bool checkRes = CheckSplit();
                    if (checkRes)
                    {
                        Utilities.Log("Result after Split: " + this.ToString());
                    }
                    else
                    {
                        checking = false;
                    }
                }

            }
            
           
        }
        public void IncreaseDepth()
        {
            depth++;
            x.IncreaseDepth();
            y.IncreaseDepth();
           
        }

        public Explosion CheckExplosion()
        {
            if (x.isValue && y.isValue)
            {
                Explosion c = new Explosion();
                if (depth>=4)
                {
                    Utilities.Log("Pair " + ToString() + " is exploding");
                    c.x = x.value;
                    c.y = y.value;
                    c.explosion = true;
                    delete = true;
                }
                return c;
            }

            if (!x.isValue)
            {
                Explosion exp = x.pair.CheckExplosion();
                if(x.pair.delete)
                {
                    x = new Value(0);
                }
                if (exp != null && exp.explosion)
                {
                    if (exp.y > -1)
                    {
                        AddValueFromY(exp.y, true);
                        exp.y = -1;
                    }
                    return exp;
                }
            }

            if (!y.isValue)
            {
                Explosion exp = y.pair.CheckExplosion();
                if (y.pair.delete )
                {
                    y = new Value(0);
                }
                if (exp != null && exp.explosion)
                {
                    if (exp.x > -1)
                    {
                        AddValueFromX(exp.x, true);
                        exp.x = -1;
                    }
                    return exp;
                }
            }
            return null;
        }

        public int AddValueFromY(int value, bool first)
        {
            if (first)
            {
                if (y.isValue)
                {
                    y.value += value;
                    Utilities.Log("Value of y(" + value + ") added to y of " + ToString());
                    return 0;
                }
                else
                {
                    return y.pair.AddValueFromY(value, false);
                }
            }
            else
            {
                if(x.isValue)
                {
                    x.value += value;
                    Utilities.Log("Value of y(" + value + ") added to x of " + ToString());
                    return 0;
                }
                else
                {
                    return x.pair.AddValueFromY(value, false);
                }
            }
        }

        public int AddValueFromX(int value, bool first)
        {
            if (first)
            {
                if (x.isValue)
                {
                    x.value += value;
                    Utilities.Log("Value of x(" + value + ") added to x of " + ToString());
                    return 0;
                }
                else
                {
                    return x.pair.AddValueFromX(value, false);
                }
            }
            else
            {
                if (y.isValue)
                {
                    y.value += value;
                    Utilities.Log("Value of x(" + value + ") added to y of " + ToString());
                    return 0;
                }
                else
                {
                    return y.pair.AddValueFromX(value, false);
                }
            }
        }

        public bool CheckSplit()
        {
            if(x.isValue)
            {
                if(x.value>=10)
                {
                    Utilities.Log("Splitting x of " + ToString());
                    x = new Value(x.SplitToPair(this.depth+1));
                    return true;
                }
            }
            else
            {
                if (x.pair.CheckSplit())
                    return true;
            }
            if (y.isValue)
            {
                if (y.value >= 10)
                {
                    Utilities.Log("Splitting y of " + ToString());
                    y = new Value(y.SplitToPair(this.depth + 1));
                    return true;
                }
            }
            else
            {
                if (y.pair.CheckSplit())
                    return true;
            }
            return false;
        }

        public int GetMagnitude()
        {
            return x.GetMagnitude(3) + y.GetMagnitude(2);
        }

        public override string ToString()
        {
            //return "[<" + depth + ">" + x.ToString() + "," + y.ToString() + "]";
            return "["+x.ToString() + "," + y.ToString() + "]";
        }

    }
}
