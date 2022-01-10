using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day24Content
{
    public class Variable
    {
        public string name;
        public long value;

        public Variable(string name, long value)
        {
            this.name = name;
            this.value = value;
        }

        public override string ToString()
        {
            return name + "=" + value;
        }


    }
}
