using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day10Content
{

    class Legal
    {
        public string open;
        public string close;
        public int errorValue;
        public int correctionValue;

        public Legal(string syntax, int errorValue, int correctionValue)
        {
            this.open = syntax[0].ToString();
            this.close = syntax[1].ToString();
            this.errorValue = errorValue;
            this.correctionValue = correctionValue;
        }

        public string GetCompleteString()
        {
            return open + close;
        }

        public string GetCounterPart(string part)
        {
            if (part == open)
                return close;
            else if (part == close)
                return open;
            else
                throw new Exception("Invalid part");
        }

        public bool ContainsPart(string part)
        {
            return part == open || part == close;
        }
    }

    class SyntaxChecker
    {
        public List<Legal> legals;

        public SyntaxChecker()
        {
            legals = new List<Legal>();
            legals.Add(new Legal("()", 3,1));
            legals.Add(new Legal("[]", 57,2));
            legals.Add(new Legal("{}", 1197,3));
            legals.Add(new Legal("<>", 25137,4));
        }

        public string Check(string input)  //0: result ok; >0 == error;   <0 == inclomete
        {
            Console.WriteLine("");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Checking syntax: " + input);
            bool checking = true;
            int findings;
            while(checking)
            {
                findings = 0;
                foreach (Legal l in legals)
                {
                    string complete = l.GetCompleteString();
                    if (input.Contains(complete))
                    {
                        input = input.Replace(complete, "");
                        findings++;
                        Console.WriteLine("Remove "+complete+ " -->    " + input);
                    }
                }
                if(findings==0 || input.Length==0)
                {
                    checking = false;
                }
            }

            if(input.Length==0)
            {
                return "";
            }
            
            Console.WriteLine("Input not ok. Checking for error: " + input);

            for(int i = 1; i < input.Length; i++)
            {
                foreach(Legal l in legals)
                {
                    if(input[i].ToString() == l.close)
                    {

                        Console.WriteLine("Found Error: " + input[i-1] + " expected, but " + input[i - 1] + " found");
                        return "Error: "+l.errorValue;
                    }
                }
            }

            Console.WriteLine("Line is just incomplete");
            return input;
        }

        public long AutoComplete(string input)
        {
            Console.WriteLine("");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Starting autocompletion for: " + input);

            string autocomp = "";
            long result = 0;
            for(int i = input.Length-1; i >= 0; i--)
            {
                foreach (Legal l in legals)
                {
                    if (l.ContainsPart(input[i].ToString()))
                    {
                        autocomp += l.GetCounterPart(input[i].ToString());
                        result = (result * 5) + l.correctionValue;
                        break;
                    }
                }
            }
            Console.WriteLine("Autocorrection complete (" +result+")  :" + input+ "  -  " + autocomp);
            return result;
        }

        public string GetLegalCounterPart(string part)
        {
          
            throw new Exception("No counterpart found");
        }
    }
}
