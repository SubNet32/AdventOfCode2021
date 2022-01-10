using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day20Content
{
    class EnhancementMask
    {
        public string enhString;

        public EnhancementMask(string input)
        {
            enhString = input;
            if (enhString.Length != 512)
                throw new Exception("EnhString is not correct. len:" + enhString.Length);
        }

        public bool GetEnhancement(int value)
        {
            if (value >= 512 || value < 0)
                throw new Exception("Wrong value in GetEnhancement(): " + value);

            return (enhString[value] == '#');
        }

        public bool GetEnhancement(string value)
        {
            return GetEnhancement((int)Utilities.ConvertBinaryStingToInt(value));
        }

    }
}
