using AdventOfCode.Basic;
using AdventOfCode.Day04Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
  

    class Day4 : Day
    {
        public static int day = 4;


        public static void GetResult1()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);

            List<Field> fieldList = new List<Field>();
            List<string> ss = new List<string>();

            string[] numbers = input[0].Split(',');

            for (int i = 2; i < input.Length; i++)
            {
                if(input[i] != "")
                {
                    ss.Add(Utilities.FormatNumberString(input[i]));
                    if(i == input.Length - 1)
                    {
                        fieldList.Add(new Field(ss.ToArray()));
                    }
                }
                else
                {
                    fieldList.Add(new Field(ss.ToArray()));
                    ss.Clear();
                }
            }

            Console.WriteLine(fieldList.Count + " Fields created");
            Console.WriteLine("Checking for " + numbers.Length + " numbers");

            foreach(string number in numbers)
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Number " + number);
                foreach(Field field in fieldList)
                {
                    int result = field.MarkNumber(int.Parse(number));
                    field.PrintField();
                    if(result>0)
                    {
                        Console.WriteLine("Winner BoardSum: " + result);
                        PrintResult(day, Convert.ToString(result * int.Parse(number)));
                        return;
                    }
                }
            }

            //PrintResult(day, "");
        }

        public static void GetResult2()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);

            List<Field> fieldList = new List<Field>();
            List<string> ss = new List<string>();

            string[] numbers = input[0].Split(',');

            for (int i = 2; i < input.Length; i++)
            {
                if (input[i] != "")
                {
                    ss.Add(Utilities.FormatNumberString(input[i]));
                    if (i == input.Length - 1)
                    {
                        fieldList.Add(new Field(ss.ToArray()));
                    }
                }
                else
                {
                    fieldList.Add(new Field(ss.ToArray()));
                    ss.Clear();
                }
            }

            Console.WriteLine(fieldList.Count + " Fields created");
            Console.WriteLine("Checking for " + numbers.Length + " numbers");

            List<Field> removeList = new List<Field>();

            foreach (string number in numbers)
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Number " + number);
                foreach (Field field in fieldList)
                {
                    int result = field.MarkNumber(int.Parse(number));
                    field.PrintField();
                    if (result > 0)
                    {
                        if (fieldList.Count - removeList.Count == 1)
                        {
                            int r = result * int.Parse(number);
                            PrintResult(day, r.ToString());
                            return;
                        }
                        else
                        {
                            removeList.Add(field);
                        }
                    }
                }
                foreach(Field field in removeList)
                {
                    fieldList.Remove(field);
                    Console.WriteLine("Removed field");
                }
                removeList.Clear();

            }

            //PrintResult(day, "");
        }

    }
}
