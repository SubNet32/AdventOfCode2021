using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day24Content
{
    class Alu
    {
        List<Operation> operations;
        List<Variable> var;
        Memory memory;

        public Alu(string[] input)
        {
            operations = new List<Operation>();
            var = new List<Variable>();
            foreach (string s in input)
            {
                operations.Add(new Operation(s));
            }
            memory = Memory.GetInstance();
        }

        public long Validate(long input)
        {
            if (input.ToString().Length != 14)
                throw new Exception("Wrong input");
            Utilities.Log("");
            Utilities.Log("------------------------------------");
            Console.WriteLine("Checking for input " + input);
            memory.Clear();
            input = SetNextInput(input);
            foreach (Operation op in operations)
            {
                if (memory.inputLoaded)
                {
                    input = SetNextInput(input);
                }
                op.Execute();
                
            }

            Utilities.Log("Printing Variables");
            List<string> variables = memory.GetMemory();
            foreach(String s in variables)
            {
                Utilities.Log(s);
            }

            long z = memory.Load("z").value;
            bool valid = z == 0;
            Utilities.Log("Input was valid: " + valid.ToString());

            return z;
        }

        private long SetNextInput(long input)
        {
            string s = input.ToString();
            memory.SetInput(long.Parse(s[0].ToString()));
            Utilities.Log("Setting input " + s[0]);
            s = s.Remove(0, 1);
            Utilities.Log("Remaining input " + s);
            if (s.Length > 0)
                return long.Parse(s);
            else
                return 0;

        }


    }
}
