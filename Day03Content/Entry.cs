using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day03Content
{
    class Entry
    {
        public int[] bits;
        public string bitString;

        public Entry(string s)
        {
            bitString = s;
            bits = new int[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                bits[i] = int.Parse(s[i].ToString());
            }
        }

        public int GetBit(int pos)
        {
            return bits[pos];
        }


    }
}
