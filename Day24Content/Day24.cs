using AdventOfCode.Basic;
using AdventOfCode.Day24Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day24 : Day
    {
        public static int day = 24;

        public static void GetResult1()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);

            Alu alu = new Alu(input);

            long model = (long)10e13 - 1;

            string ms = model.ToString();
            List<Result> results = new List<Result>();
            for(int i = 0; i < ms.Length; i++)
            {
                Result r = new Result(i);
                for (int n = 1; n <= 9; n++)
                {
                    ms = ChangeCharAt(ms, n.ToString(), i);
                    long res = alu.Validate(long.Parse(ms));
                    r.AddResult(long.Parse(ms), res);
                }
                results.Add(r);
            }

            Console.WriteLine("Results");
            string mask = "";
            foreach (Result r in results)
            {
                r.CheckResults();
                Console.WriteLine("");
                if(r.noChange)
                {
                    mask += "0";
                }
                else
                {
                    mask += "1";
                }

            }

            Console.WriteLine("Mask: " + mask);


            while (alu.Validate(model)!=0)
            {
                model = SubtractWithMask(model, mask);
            }

            PrintResult(day, "");
        }


        public static string ChangeCharAt(string s, string c, int index)
        {
            string res = s.Substring(0, index);
            res += c;
            res += s.Substring(index + 1);
            return res;
        }

        public static long SubtractWithMask(long value, string mask)
        {
            string s = value.ToString();
            int index = 0;
            for(int i = s.Length - 1; i >= 0; i--)
            {
                if(mask[i]=='1')
                {
                    index = i;
                    break;
                }
            }
            //Console.WriteLine("Subtracting");
            while (true)
            {
                int v = int.Parse(s[index].ToString());
                //Console.WriteLine("index: "+index);
                if (v>1)
                {
                    value -= Convert.ToInt64(Math.Pow(10, (double)(s.Length - (index + 1))));
                    return value;
                }
                else
                {
                    if (index == 0)
                        return 0;

                    value += 8 * Convert.ToInt64(Math.Pow(10, (double)(s.Length - (index + 1))));

                    for (int i = index-1; i >= 0; i--)
                    {
                        if (mask[i] == '1')
                        {
                            index = i;
                            break;
                        }
                    }
                }

            }
        }
    }
}
