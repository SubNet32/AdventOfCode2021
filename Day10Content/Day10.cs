using AdventOfCode.Basic;
using AdventOfCode.Day10Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Day10 : Day
    {
        public static int day = 10;

        public static void GetResult1()
        {
            string[] input = InputHandler.GetInputForDay(day);
            Console.WriteLine("Calc Result. Elements: " + input.Length);

            SyntaxChecker syntax = new SyntaxChecker();

            List<long> autocompleteScores = new List<long>();

            foreach (string s in input)
            {
                string result = syntax.Check(s);
                if(result!="" && !result.StartsWith("Error"))
                {
                    autocompleteScores.Add(syntax.AutoComplete(result));
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Sort list of scores");
            autocompleteScores.Sort();
            for(int i=0; i < autocompleteScores.Count; i++)
            {
                Console.WriteLine(i + ": " + autocompleteScores[i]);
            }
            Console.WriteLine("");
            int middleIndex = autocompleteScores.Count / 2;
            Console.WriteLine("Taking middle: " + middleIndex + " with value: " + autocompleteScores[middleIndex]);
                
            PrintResult(day, autocompleteScores[middleIndex].ToString());
        }
    }
}
